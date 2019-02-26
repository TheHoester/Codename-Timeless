using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[ExecuteInEditMode]
public abstract class Tilemap : MonoBehaviour
{
    [Serializable]
    public class TilemapDictionary : SerializableDictionary<RelativePosition, Tile> { }

    #region Variables

    [SerializeField, Header("Tilemap Variables")]
    protected int minXPosition;
    [SerializeField]
    protected int maxXPosition, minYPosition, maxYPosition;

    [SerializeField]
    protected TilemapDictionary tilemapStorage;
    public TilemapDictionary TilemapStorage
    {
        get
        {
            if (tilemapStorage == null || tilemapStorage.Count == 0)
                GetTiles();
            return tilemapStorage;
        }
    }

    [SerializeField, Header("Navigation Variables")]
    protected List<Vector2> navigationNodes;

    [SerializeField]
    protected Vector2 startPosition, endPosition;
    [SerializeField]
    protected List<Vector2> path;

    /// <summary>
    /// Used by the TilemapStorage property to repopulate the tilemapStorage variable with all tiles
    /// in the tilemap if it is return null.
    /// </summary>
    private void GetTiles()
    {
        tilemapStorage = new TilemapDictionary();

        Tile[] tiles = GetComponentsInChildren<Tile>();
        foreach (Tile tile in tiles)
            tilemapStorage.Add(tile.TilemapPosition, tile);
    }

    #endregion

    #region Accessor Methods

    /// <summary>
    /// Retrieves the tile at a given position in the map.
    /// </summary>
    /// <param name="mapPosition">The relative position of the tile in the map.</param>
    /// <returns>The tile at the position.</returns>
    protected Tile GetTile(RelativePosition mapPosition)
    {
        Tile temp = null;
        TilemapStorage.TryGetValue(mapPosition, out temp);
        return temp;
    }

    /// <summary>
    /// Retrieves the tile at a given position in the map.
    /// </summary>
    /// <param name="mapPosition">The relative position of the tile in the map.</param>
    /// <returns>The tile at the position.</returns>
    protected Tile GetTile(int x, int y)
    {
        return GetTile(new RelativePosition(x, y));
    }

    /// <summary>
    /// Gets the tile north of the tile in the position given in the map.
    /// </summary>
    /// <param name="mapPosition">The relative position of the original tile in the map.</param>
    /// <returns>The tile at the position.</returns>
    protected Tile GetNorthTile(RelativePosition mapPosition)
    {
        Tile temp = null;
        TilemapStorage.TryGetValue(new RelativePosition(mapPosition.x, mapPosition.y + 1), out temp);
        return temp;
    }

    /// <summary>
    /// Gets the tile east of the tile in the position given in the map.
    /// </summary>
    /// <param name="mapPosition">The relative position of the original tile in the map.</param>
    /// <returns>The tile at the position.</returns>
    protected Tile GetEastTile(RelativePosition mapPosition)
    {
        Tile temp = null;
        TilemapStorage.TryGetValue(new RelativePosition(mapPosition.x + 1, mapPosition.y), out temp);
        return temp;
    }

    /// <summary>
    /// Gets the tile south of the tile in the position given in the map.
    /// </summary>
    /// <param name="mapPosition">The relative position of the original tile in the map.</param>
    /// <returns>The tile at the position.</returns>
    protected Tile GetSouthTile(RelativePosition mapPosition)
    {
        Tile temp = null;
        TilemapStorage.TryGetValue(new RelativePosition(mapPosition.x, mapPosition.y - 1), out temp);
        return temp;
    }

    /// <summary>
    /// Gets the tile west of the tile in the position given in the map.
    /// </summary>
    /// <param name="mapPosition">The relative position of the original tile in the map.</param>
    /// <returns>The tile at the position.</returns>
    protected Tile GetWestTile(RelativePosition mapPosition)
    {
        Tile temp = null;
        TilemapStorage.TryGetValue(new RelativePosition(mapPosition.x - 1, mapPosition.y), out temp);
        return temp;
    }

    #endregion

    #region Tilemap Editor Methods

    /// <summary>
    /// Adds a new tile to the tilemap. If a tile already exists in the position, it is replaced.
    /// </summary>
    /// <param name="prefab">Prefab of the new tile.</param>
    /// <param name="position">Relative position of the new tile.</param>
    /// <returns>The new tile.</returns>
    protected Tile InstantiateTile(GameObject prefab, RelativePosition position)
    {
        Tile tile = ((GameObject)PrefabUtility.InstantiatePrefab(prefab)).GetComponent<Tile>();
        tile.transform.parent = transform;
        tile.InitializeTile(position);

        if (TilemapStorage.ContainsKey(position))
        {
            Tile oldTile = null;
            if (TilemapStorage.TryGetValue(position, out oldTile))
            {
                if (oldTile.GetInstanceID() != tile.GetInstanceID())
                {
                    TilemapStorage[position] = tile;
                    DestroyImmediate(oldTile.gameObject);
                }
            }
            else
                TilemapStorage[position] = tile;
        }
        else
            TilemapStorage.Add(position, tile);

        UpdateMinMax();

        return tile;
    }

    /// <summary>
    /// Adds a new sprite layer to a tile.
    /// </summary>
    /// <param name="prefab">Prefab gameobject of the layer to be added to the tile.</param>
    /// <param name="position">Relative map position of the tile to add the layer to.</param>
    /// <returns>The tile at the position given.</returns>
    protected Tile AddLayerToTile(GameObject prefab, RelativePosition position)
    {
        Tile tile = null;
        if (TilemapStorage.TryGetValue(position, out tile))
            if (tile != null)
            {
                GameObject layer = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                layer.transform.localScale = new Vector3(Constants.TILESCALE, Constants.TILESCALE, Constants.TILESCALE);
                tile.AddLayer(layer);
            }

        return tile;
    }

    /// <summary>
    /// Adds a new sprite layer to a tile.
    /// </summary>
    /// <param name="sprite">The spite of the new layer to be added.</param>
    /// <param name="position">Relative map position of the tile to add the layer to.</param>
    /// <returns>The tile at the position given.</returns>
    protected Tile AddLayerToTile(Sprite sprite, RelativePosition position)
    {
        Tile tile = null;
        if (TilemapStorage.TryGetValue(position, out tile))
        {
            if (tile != null)
            {
                GameObject layer = new GameObject();
                layer.name = sprite.name;
                layer.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
                SpriteRenderer renderer = layer.AddComponent<SpriteRenderer>();
                renderer.sprite = sprite;
                tile.AddLayer(layer);
            }
        }

        return tile;
    }

    /// <summary>
    /// Destroys the tile in the position given and removes its entry from the tilemap.
    /// </summary>
    /// <param name="position">The relative position of the tile.</param>
    protected void RemoveTile(RelativePosition position)
    {
        if (TilemapStorage.ContainsKey(position))
        {
            Tile tile = null;
            if (TilemapStorage.TryGetValue(position, out tile))
            {
                if (tile != null)
                    DestroyImmediate(tile.gameObject);

                TilemapStorage.Remove(position);
            }
        }
        else
            Debug.LogError("Tile can't be removed: Tile doesn't exist");
    }

    /// <summary>
    /// Destroys all tile objects within this tilemap and resets the dictionary storing the map.
    /// </summary>
    protected void DestroyTilemap()
    {
        Tile[] allRoadTiles = GetComponentsInChildren<Tile>();
        foreach (Tile tile in allRoadTiles)
            DestroyImmediate(tile.gameObject);

        tilemapStorage = new TilemapDictionary();

        UpdateMinMax();
    }

    #endregion

    #region Util Methods

    /// <summary>
    /// Checks if a tile exists in the position given.
    /// </summary>
    /// <param name="position">The position of interest.</param>
    /// <returns></returns>
    protected bool IsTileValid(RelativePosition position)
    {
        return TilemapStorage.ContainsKey(position);
    }

    /// <summary>
    /// Takes a vector3 world position and converts it to its relative position in the tilemap.
    /// </summary>
    /// <param name="position">World position of the tile.</param>
    /// <returns></returns>
    protected RelativePosition ConvertWorldToMapPosition(Vector3 position)
    {
        return new RelativePosition(position.x / Constants.TILESIZE, position.y / Constants.TILESIZE);
    }

    /// <summary>
    /// Takes a vector2 world position and converts it to its relative position in the tilemap.
    /// </summary>
    /// <param name="position">World position of the tile.</param>
    /// <returns></returns>
    protected RelativePosition ConvertWorldToMapPosition(Vector2 position)
    {
        return new RelativePosition(position.x / Constants.TILESIZE, position.y / Constants.TILESIZE);
    }

    /// <summary>
    /// Updates the new min and max position values in both the x and y direction.
    /// </summary>
    private void UpdateMinMax()
    {
        Tile[] tiles = GetComponentsInChildren<Tile>();
        if (tiles.Length > 0)
        {
            minXPosition = maxXPosition = tiles[0].TilemapPosition.x;
            minYPosition = maxYPosition = tiles[0].TilemapPosition.y;

            foreach (Tile tile in tiles)
            {
                if (minXPosition > tile.TilemapPosition.x)
                    minXPosition = tile.TilemapPosition.x;
                if (maxXPosition < tile.TilemapPosition.x)
                    maxXPosition = tile.TilemapPosition.x;
                if (minYPosition > tile.TilemapPosition.y)
                    minYPosition = tile.TilemapPosition.y;
                if (maxYPosition < tile.TilemapPosition.y)
                    maxYPosition = tile.TilemapPosition.y;
            }
        }
        else
            minXPosition = maxXPosition = minYPosition = maxYPosition = 0;
    }

    #endregion

    #region Navigation Methods

    public void GenerateNavigationNodes()
    {
        navigationNodes = new List<Vector2>();

        Tile currentTile = null;
        for (int x = minXPosition; x <= maxXPosition; x++)
        {
            for (int y = minYPosition; y <= maxYPosition; y++)
            {
                if (TilemapStorage.TryGetValue(new RelativePosition(x, y), out currentTile))
                {
                    if (currentTile == null || !currentTile.IsWalkable)
                        continue;

                    if (x == -4 && y == -2)
                    {
                        int i = 1;
                    }

                    Vector2 startPosition = HelperFunctions.RelativePositionToVector2(new RelativePosition(x, y));

                    startPosition.x -= Constants.TILECENTREOFFSET;
                    startPosition.y -= Constants.TILECENTREOFFSET;

                    Vector2 currentPosition = startPosition;

                    for (int yNode = 0; yNode < 5; yNode++)
                    {
                        for (int xNode = 0; xNode < 3; xNode++)
                        {
                            currentPosition.x = startPosition.x + (xNode * Constants.TILECENTREOFFSET);
                            currentPosition.y = startPosition.y + (yNode * (Constants.TILECENTREOFFSET / 2));

                            if (yNode % 2 == 1)
                            {
                                if (xNode == 2)
                                    continue;

                                currentPosition.x += (Constants.TILECENTREOFFSET / 2);
                            }

                            if (CheckNodeValid(currentPosition) && !CheckNodeExists(currentPosition))
                                navigationNodes.Add(currentPosition);
                        }
                    }
                }
            }
        }
    }

    private bool CheckNodeValid(Vector2 position)
    {
        Vector2 positionCheck = new Vector2(position.x - Constants.TILECENTREOFFSET + Constants.FAIL, position.y);
        Tile tileCheck = null;

        if (TilemapStorage.TryGetValue(HelperFunctions.Vector2ToRelativePosition(positionCheck), out tileCheck))
            if (tileCheck == null || !tileCheck.IsWalkable)
                return false;

        positionCheck = new Vector2(position.x + Constants.TILECENTREOFFSET - Constants.FAIL, position.y);
        tileCheck = null;

        if (TilemapStorage.TryGetValue(HelperFunctions.Vector2ToRelativePosition(positionCheck), out tileCheck))
            if (tileCheck == null || !tileCheck.IsWalkable)
                return false;

        return true;
    }

    private bool CheckNodeExists(Vector2 position)
    {
        foreach (Vector2 vector in navigationNodes)
        {
            if (vector.x == 13.2f)
            {
                int i = 0;
            }

            if (Mathf.Approximately(vector.x, position.x) && Mathf.Approximately(vector.y, position.y))
                return true;
        }

        return false;
    }

    private bool IsConnectingNode(Vector2 startNode, Vector2 endNode)
    {
        List<Vector2> connectingNodes = GetConnectingNodes(startNode);

        foreach (Vector2 node in connectingNodes)
            if (node == endNode)
                return true;

        return false;
    }

    private List<Vector2> GetConnectingNodes(Vector2 position)
    {
        List<Vector2> connectingNodes = new List<Vector2>();

        // North
        Vector2 currentTile = new Vector2(position.x, position.y + Constants.TILECENTREOFFSET);
        if (CheckNodeExists(currentTile))
            connectingNodes.Add(currentTile);

        // East
        currentTile = new Vector2(position.x + Constants.TILECENTREOFFSET, position.y);
        if (CheckNodeExists(currentTile))
            connectingNodes.Add(currentTile);

        // South
        currentTile = new Vector2(position.x, position.y - Constants.TILECENTREOFFSET);
        if (CheckNodeExists(currentTile))
            connectingNodes.Add(currentTile);

        // West
        currentTile = new Vector2(position.x - Constants.TILECENTREOFFSET, position.y);
        if (CheckNodeExists(currentTile))
            connectingNodes.Add(currentTile);

        // NorthEast
        currentTile = new Vector2(position.x + (Constants.TILECENTREOFFSET / 2), position.y + (Constants.TILECENTREOFFSET / 2));
        if (CheckNodeExists(currentTile))
            connectingNodes.Add(currentTile);

        // NorthWest
        currentTile = new Vector2(position.x - (Constants.TILECENTREOFFSET / 2), position.y + (Constants.TILECENTREOFFSET / 2));
        if (CheckNodeExists(currentTile))
            connectingNodes.Add(currentTile);

        // SouthEast
        currentTile = new Vector2(position.x + (Constants.TILECENTREOFFSET / 2), position.y - (Constants.TILECENTREOFFSET / 2));
        if (CheckNodeExists(currentTile))
            connectingNodes.Add(currentTile);

        // SouthWest
        currentTile = new Vector2(position.x - (Constants.TILECENTREOFFSET / 2), position.y - (Constants.TILECENTREOFFSET / 2));
        if (CheckNodeExists(currentTile))
            connectingNodes.Add(currentTile);

        return connectingNodes;
    }

    private Vector2 GetClosestNode(Vector2 position)
    {
        float closestDistance = 10000.0f;
        Vector2 closestNode = new Vector2();

        float distance = 0.0f;
        foreach (Vector2 node in navigationNodes)
        {
            distance = Vector2.Distance(position, node);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNode = node;
            }
        }

        return closestNode;
    }

    public List<Vector2> FindPath(Vector2 startPos, Vector2 endPos)
    {
        double startTime = EditorApplication.timeSinceStartup;

        // Makes sure the locations are travellable and gets the closest nodes to the positions.
        Tile startTile = null;
        Tile endTile = null;
        if (TilemapStorage.TryGetValue(ConvertWorldToMapPosition(startPos), out startTile) &&
            TilemapStorage.TryGetValue(ConvertWorldToMapPosition(endPos), out endTile))
        {
            if (startTile != null && startTile.IsWalkable && endTile != null && endTile.IsWalkable)
            {
                startPos = GetClosestNode(startPos);
                endPos = GetClosestNode(endPos);
            }
            else
                return null;
        }
        else
            return null;

        // Start of A* pathfinding
        List<NodeNavInfo> openList = new List<NodeNavInfo>();
        List<NodeNavInfo> closedList = new List<NodeNavInfo>();

        // Setup closed and open lists
        closedList.Add(new NodeNavInfo(startPos, CalculateNodeScore(startPos, startPos, endPos), 0));

        List<Vector2> connectingNodes = GetConnectingNodes(startPos);
        foreach (Vector2 node in connectingNodes)
            openList.Add(new NodeNavInfo(node, CalculateNodeScore(node, startPos, endPos), 1));

        int iterations = 0;
        while (closedList[closedList.Count - 1].nodePos != endPos)
        {
            if (openList.Count == 0)
            {
                List<Vector2> errorList = new List<Vector2>();
                Debug.LogError("Pathfinding Error: No more open nodes");
                foreach (NodeNavInfo node in closedList)
                    errorList.Add(node.nodePos);
                return errorList;
            }

            // Gets closest node
            NodeNavInfo closestNode = openList[0];
            foreach (NodeNavInfo node in openList)
                if (node.score <= closestNode.score)
                    closestNode = node;

            // Moves closest node to closed list
            closedList.Add(closestNode);
            openList.Remove(closestNode);

            // Adds any connecting nodes to the open list that aren't already in it
            connectingNodes = GetConnectingNodes(closestNode.nodePos);
            foreach (Vector2 node in connectingNodes)
            {
                bool exists = false;
                foreach (NodeNavInfo closedNode in closedList)
                {
                    if (closedNode.nodePos == node)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    foreach (NodeNavInfo openNode in openList)
                    {
                        if (openNode.nodePos == node)
                        {
                            exists = true;
                            break;
                        }
                    }
                }

                if (!exists)
                    openList.Add(new NodeNavInfo(node, CalculateNodeScore(node, startPos, endPos), closestNode.numFromStart + 1));
            }

            // Failsafe incase infinite loop is created
            iterations++;
            if (iterations > 1000)
            {
                Debug.LogError("Pathfinding failed: Infinite loop");
                break;
            }
        }

        List<Vector2> nodeList = new List<Vector2>();

        closedList = HelperFunctions.FlipList(closedList);
        nodeList.Add(closedList[0].nodePos);
        int lastIndexAdded = 0;

        for (int i = 0; i < closedList.Count; i++)
        {
            if (IsConnectingNode(closedList[i].nodePos, closedList[lastIndexAdded].nodePos) &&
                closedList[i].numFromStart == closedList[lastIndexAdded].numFromStart - 1)
            {
                nodeList.Add(closedList[i].nodePos);
                lastIndexAdded = i;
            }
        }

        nodeList = HelperFunctions.FlipList(nodeList);

        Debug.Log("Time: " + (EditorApplication.timeSinceStartup - startTime));

        return nodeList;
    }

    private float CalculateNodeScore(Vector2 node, Vector2 start, Vector2 end)
    {


        /*
        dx = abs(node.x - goal.x)
        dy = abs(node.y - goal.y)
        return D * (dx * dx + dy * dy)

        int xToStart = Mathf.RoundToInt((node.x - start.x) / (Constants.TILECENTREOFFSET / 2));
        int yToStart = Mathf.RoundToInt((node.y - start.y) / (Constants.TILECENTREOFFSET / 2));
        int xToEnd = Mathf.RoundToInt((node.x - end.x) / (Constants.TILECENTREOFFSET / 2));
        int yToEnd = Mathf.RoundToInt((node.y - end.y) / (Constants.TILECENTREOFFSET / 2));

        return Mathf.Abs(xToStart) + Mathf.Abs(yToStart) + Mathf.Abs(xToEnd) + Mathf.Abs(yToEnd);
        */
        //float xToStart = Mathf.Abs(node.x - start.x);
        //float yToStart = Mathf.Abs(node.y - start.y);
        float xToEnd = Mathf.Abs(node.x - end.x);
        float yToEnd = Mathf.Abs(node.y - end.y);

        return (Constants.TILECENTREOFFSET / 2) * (((xToEnd * xToEnd) + (yToEnd * yToEnd)));
    }

    public void TestNavigation()
    {
        path = FindPath(startPosition, endPosition);
    }

    #endregion

    #region Abtract Methods

    public abstract void ResetTilemap();

    #endregion

    private void OnDrawGizmos()
    {
        if (navigationNodes != null)
        {
            /*
            Vector2 north;
            Vector2 east;
            Vector2 south;
            Vector2 west;
            Vector2 northEast;
            Vector2 northWest;
            Vector2 southEast;
            Vector2 southWest;
            */

            for (int i = 0; i < navigationNodes.Count; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(navigationNodes[i], 0.1f);

                /*
                if (i >= 0)
                {
                    GetConnectingNodes(position, out north, out east, out south, out west, out northEast, out northWest, out southEast, out southWest);

                    Gizmos.color = Color.green;

                    if (north.x != Constants.FAIL)
                        Gizmos.DrawLine(position, north);
                    if (east.x != Constants.FAIL)
                        Gizmos.DrawLine(position, east);
                    if (south.x != Constants.FAIL)
                        Gizmos.DrawLine(position, south);
                    if (west.x != Constants.FAIL)
                        Gizmos.DrawLine(position, west);
                    if (northEast.x != Constants.FAIL)
                        Gizmos.DrawLine(position, northEast);
                    if (northWest.x != Constants.FAIL)
                        Gizmos.DrawLine(position, northWest);
                    if (southEast.x != Constants.FAIL)
                        Gizmos.DrawLine(position, southEast);
                    if (southWest.x != Constants.FAIL)
                        Gizmos.DrawLine(position, southWest);
                }
                */
            }

            Gizmos.color = Color.blue;
            if (path != null)
                for (int i = 0; i < path.Count - 1; i++)
                    Gizmos.DrawLine(path[i], path[i + 1]);
        }
    }
}

struct NodeNavInfo
{
    public Vector2 nodePos;
    public float score;
    public int numFromStart;

    public NodeNavInfo(Vector2 pos, float sco, int start)
    {
        nodePos = pos;
        score = sco;
        numFromStart = start;
    }
}