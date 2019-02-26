using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class RoadManager : Tilemap
{
    #region Variables
    
    [SerializeField, Header("RoadManager Variables")]
    private GameObject roadBlank;
    [SerializeField]
    private GameObject roadCentreLeft, roadCentreRight, roadCentreTop, roadCentreBottom;
    [SerializeField]
    private GameObject pavementCurbBottom, pavementCurbTop, pavementCurbLeft, pavementCurbRight;
    [SerializeField]
    private GameObject pavementBackBottom, pavementBackTop, pavementBackLeft, pavementBackRight;
    [SerializeField]
    private GameObject pavementCornerBottomLeft, pavementCornerBottomRight, pavementCornerTopLeft, pavementCornerTopRight;
    [SerializeField]
    private GameObject pavementBackCornerBottomLeft, pavementBackCornerBottomRight, pavementBackCornerTopLeft, pavementBackCornerTopRight;
    [SerializeField]
    private GameObject pavementInvertedCornerBottomLeft, pavementInvertedCornerBottomRight, pavementInvertedCornerTopLeft, pavementInvertedCornerTopRight;

    private Stack<List<TileInfo>> undoStateStorage;

    #endregion

    public override void ResetTilemap()
    {
        DestroyTilemap();

        InstantiateTile(pavementBackCornerBottomRight, new RelativePosition(0, 0));
        InstantiateTile(pavementBackBottom, new RelativePosition(1, 0));
        InstantiateTile(pavementBackBottom, new RelativePosition(2, 0));
        InstantiateTile(pavementBackBottom, new RelativePosition(3, 0));
        InstantiateTile(pavementBackBottom, new RelativePosition(4, 0));
        InstantiateTile(pavementBackBottom, new RelativePosition(5, 0));
        InstantiateTile(pavementBackBottom, new RelativePosition(6, 0));
        InstantiateTile(pavementBackBottom, new RelativePosition(7, 0));
        InstantiateTile(pavementBackBottom, new RelativePosition(8, 0));
        InstantiateTile(pavementBackCornerBottomLeft, new RelativePosition(9, 0));

        InstantiateTile(pavementBackRight, new RelativePosition(0, -1));
        InstantiateTile(pavementInvertedCornerTopLeft, new RelativePosition(1, -1));
        InstantiateTile(pavementCurbBottom, new RelativePosition(2, -1));
        InstantiateTile(pavementCurbBottom, new RelativePosition(3, -1));
        InstantiateTile(pavementCurbBottom, new RelativePosition(4, -1));
        InstantiateTile(pavementCurbBottom, new RelativePosition(5, -1));
        InstantiateTile(pavementCurbBottom, new RelativePosition(6, -1));
        InstantiateTile(pavementCurbBottom, new RelativePosition(7, -1));
        InstantiateTile(pavementInvertedCornerTopRight, new RelativePosition(8, -1));
        InstantiateTile(pavementBackLeft, new RelativePosition(9, -1));

        InstantiateTile(pavementBackRight, new RelativePosition(0, -2));
        InstantiateTile(pavementCurbRight, new RelativePosition(1, -2));
        InstantiateTile(roadBlank, new RelativePosition(2, -2));
        InstantiateTile(roadBlank, new RelativePosition(3, -2));
        InstantiateTile(roadBlank, new RelativePosition(4, -2));
        InstantiateTile(roadBlank, new RelativePosition(5, -2));
        InstantiateTile(roadBlank, new RelativePosition(6, -2));
        InstantiateTile(roadBlank, new RelativePosition(7, -2));
        InstantiateTile(pavementCurbLeft, new RelativePosition(8, -2));
        InstantiateTile(pavementBackLeft, new RelativePosition(9, -2));

        InstantiateTile(pavementBackRight, new RelativePosition(0, -3));
        InstantiateTile(pavementCurbRight, new RelativePosition(1, -3));
        InstantiateTile(roadBlank, new RelativePosition(2, -3));
        InstantiateTile(roadBlank, new RelativePosition(3, -3));
        InstantiateTile(roadBlank, new RelativePosition(4, -3));
        InstantiateTile(roadBlank, new RelativePosition(5, -3));
        InstantiateTile(roadBlank, new RelativePosition(6, -3));
        InstantiateTile(roadBlank, new RelativePosition(7, -3));
        InstantiateTile(pavementCurbLeft, new RelativePosition(8, -3));
        InstantiateTile(pavementBackLeft, new RelativePosition(9, -3));

        InstantiateTile(pavementBackRight, new RelativePosition(0, -4));
        InstantiateTile(pavementCurbRight, new RelativePosition(1, -4));
        InstantiateTile(roadBlank, new RelativePosition(2, -4));
        InstantiateTile(roadBlank, new RelativePosition(3, -4));
        InstantiateTile(roadBlank, new RelativePosition(4, -4));
        InstantiateTile(roadBlank, new RelativePosition(5, -4));
        InstantiateTile(roadBlank, new RelativePosition(6, -4));
        InstantiateTile(roadBlank, new RelativePosition(7, -4));
        InstantiateTile(pavementCurbLeft, new RelativePosition(8, -4));
        InstantiateTile(pavementBackLeft, new RelativePosition(9, -4));

        InstantiateTile(pavementBackRight, new RelativePosition(0, -5));
        InstantiateTile(pavementCurbRight, new RelativePosition(1, -5));
        InstantiateTile(roadBlank, new RelativePosition(2, -5));
        InstantiateTile(roadBlank, new RelativePosition(3, -5));
        InstantiateTile(roadBlank, new RelativePosition(4, -5));
        InstantiateTile(roadBlank, new RelativePosition(5, -5));
        InstantiateTile(roadBlank, new RelativePosition(6, -5));
        InstantiateTile(roadBlank, new RelativePosition(7, -5));
        InstantiateTile(pavementCurbLeft, new RelativePosition(8, -5));
        InstantiateTile(pavementBackLeft, new RelativePosition(9, -5));

        InstantiateTile(pavementBackRight, new RelativePosition(0, -6));
        InstantiateTile(pavementCurbRight, new RelativePosition(1, -6));
        InstantiateTile(roadBlank, new RelativePosition(2, -6));
        InstantiateTile(roadBlank, new RelativePosition(3, -6));
        InstantiateTile(roadBlank, new RelativePosition(4, -6));
        InstantiateTile(roadBlank, new RelativePosition(5, -6));
        InstantiateTile(roadBlank, new RelativePosition(6, -6));
        InstantiateTile(roadBlank, new RelativePosition(7, -6));
        InstantiateTile(pavementCurbLeft, new RelativePosition(8, -6));
        InstantiateTile(pavementBackLeft, new RelativePosition(9, -6));

        InstantiateTile(pavementBackRight, new RelativePosition(0, -7));
        InstantiateTile(pavementCurbRight, new RelativePosition(1, -7));
        InstantiateTile(roadBlank, new RelativePosition(2, -7));
        InstantiateTile(roadBlank, new RelativePosition(3, -7));
        InstantiateTile(roadBlank, new RelativePosition(4, -7));
        InstantiateTile(roadBlank, new RelativePosition(5, -7));
        InstantiateTile(roadBlank, new RelativePosition(6, -7));
        InstantiateTile(roadBlank, new RelativePosition(7, -7));
        InstantiateTile(pavementCurbLeft, new RelativePosition(8, -7));
        InstantiateTile(pavementBackLeft, new RelativePosition(9, -7));

        InstantiateTile(pavementBackRight, new RelativePosition(0, -8));
        InstantiateTile(pavementInvertedCornerBottomLeft, new RelativePosition(1, -8));
        InstantiateTile(pavementCurbTop, new RelativePosition(2, -8));
        InstantiateTile(pavementCurbTop, new RelativePosition(3, -8));
        InstantiateTile(pavementCurbTop, new RelativePosition(4, -8));
        InstantiateTile(pavementCurbTop, new RelativePosition(5, -8));
        InstantiateTile(pavementCurbTop, new RelativePosition(6, -8));
        InstantiateTile(pavementCurbTop, new RelativePosition(7, -8));
        InstantiateTile(pavementInvertedCornerBottomRight, new RelativePosition(8, -8));
        InstantiateTile(pavementBackLeft, new RelativePosition(9, -8));

        InstantiateTile(pavementBackCornerTopRight, new RelativePosition(0, -9));
        InstantiateTile(pavementBackTop, new RelativePosition(1, -9));
        InstantiateTile(pavementBackTop, new RelativePosition(2, -9));
        InstantiateTile(pavementBackTop, new RelativePosition(3, -9));
        InstantiateTile(pavementBackTop, new RelativePosition(4, -9));
        InstantiateTile(pavementBackTop, new RelativePosition(5, -9));
        InstantiateTile(pavementBackTop, new RelativePosition(6, -9));
        InstantiateTile(pavementBackTop, new RelativePosition(7, -9));
        InstantiateTile(pavementBackTop, new RelativePosition(8, -9));
        InstantiateTile(pavementBackCornerTopLeft, new RelativePosition(9, -9));
    }

    public void LoadAssets()
    {
        roadBlank = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Road/Road_Blank.prefab", typeof(GameObject));
        roadCentreLeft = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Road/Road_Left.prefab", typeof(GameObject));
        roadCentreRight = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Road/Road_Right.prefab", typeof(GameObject));
        roadCentreTop = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Road/Road_Top.prefab", typeof(GameObject));
        roadCentreBottom = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Road/Road_Bottom.prefab", typeof(GameObject));
        pavementCurbBottom = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_Bottom_Curb.prefab", typeof(GameObject));
        pavementCurbTop = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_Top_Curb.prefab", typeof(GameObject));
        pavementCurbLeft = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_Left_Curb.prefab", typeof(GameObject));
        pavementCurbRight = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_Right_Curb.prefab", typeof(GameObject));
        pavementBackBottom = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_Bottom_Back.prefab", typeof(GameObject));
        pavementBackTop = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_Top_Back.prefab", typeof(GameObject));
        pavementBackLeft = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_Left_Back.prefab", typeof(GameObject));
        pavementBackRight = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_Right_Back.prefab", typeof(GameObject));
        pavementCornerBottomLeft = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_RoadCorner_BottomLeft.prefab", typeof(GameObject));
        pavementCornerBottomRight = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_RoadCorner_BottomRight.prefab", typeof(GameObject));
        pavementCornerTopLeft = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_RoadCorner_TopLeft.prefab", typeof(GameObject));
        pavementCornerTopRight = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_RoadCorner_TopRight.prefab", typeof(GameObject));
        pavementBackCornerBottomLeft = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_BackCorner_BottomLeft.prefab", typeof(GameObject));
        pavementBackCornerBottomRight = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_BackCorner_BottomRight.prefab", typeof(GameObject));
        pavementBackCornerTopLeft = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_BackCorner_TopLeft.prefab", typeof(GameObject));
        pavementBackCornerTopRight = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_BackCorner_TopRight.prefab", typeof(GameObject));
        pavementInvertedCornerBottomLeft = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_InvertedCorner_BottomLeft.prefab", typeof(GameObject));
        pavementInvertedCornerBottomRight = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_InvertedCorner_BottomRight.prefab", typeof(GameObject));
        pavementInvertedCornerTopLeft = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_InvertedCorner_TopLeft.prefab", typeof(GameObject));
        pavementInvertedCornerTopRight = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tiles/Pavement/Pavement_InvertedCorner_TopRight.prefab", typeof(GameObject));
    }

    #region Handle Methods

    /// <summary>
    /// Called by the inspector to create new tiles when the user lets go of a handle.
    /// </summary>
    /// <param name="direction">The direction the handle is pulled.</param>
    /// <param name="numberOfTiles">The distance the handle was pulled by number of tile widths/lengths.</param>
    /// <param name="arrowPos">Tile position of the tile the arrow started from.</param>
    public void CreateNewTiles(Direction direction, int numberOfTiles, RelativePosition arrowPos)
    {
        // Uses arrow position to find the starting position of the tile strips. Generally the most top tile or most left tile, depending on direction.
        Vector3 directionVector = HelperFunctions.DirectionToVector3(direction);
        if (directionVector.x < 0.0f)
            directionVector.x *= -1.0f;
        if (directionVector.y < 0.0f)
            directionVector.y *= -1.0f;
        RelativePosition topLeft = new RelativePosition(arrowPos.x - (int)(directionVector.y * 4), arrowPos.y + (int)(directionVector.x * 5));

        // Resets directional vector
        directionVector = HelperFunctions.DirectionToVector3(direction);
        
        GameObject[] prefabsBack;
        GameObject[] prefabsFront;
        GameObject[] prefabsRoad;

        switch (direction)
        {
            case Direction.North:
                prefabsBack = new GameObject[] { pavementBackCornerBottomRight, pavementBackBottom, pavementBackBottom, pavementBackBottom, pavementBackBottom, pavementBackBottom, pavementBackBottom, pavementBackBottom, pavementBackBottom, pavementBackCornerBottomLeft };
                prefabsFront = new GameObject[] { pavementBackRight, pavementInvertedCornerTopLeft, pavementCurbBottom, pavementCurbBottom, pavementCurbBottom, pavementCurbBottom, pavementCurbBottom, pavementCurbBottom, pavementInvertedCornerTopRight, pavementBackLeft };
                prefabsRoad = new GameObject[] { pavementBackRight, pavementCurbRight, roadBlank, roadBlank, roadBlank, roadBlank, roadBlank, roadBlank, pavementCurbLeft, pavementBackLeft };
                break;
            case Direction.East:
                prefabsBack = new GameObject[] { pavementBackCornerBottomLeft, pavementBackLeft, pavementBackLeft, pavementBackLeft, pavementBackLeft, pavementBackLeft, pavementBackLeft, pavementBackLeft, pavementBackLeft, pavementBackCornerTopLeft };
                prefabsFront = new GameObject[] { pavementBackBottom, pavementInvertedCornerTopRight, pavementCurbLeft, pavementCurbLeft, pavementCurbLeft, pavementCurbLeft, pavementCurbLeft, pavementCurbLeft, pavementInvertedCornerBottomRight, pavementBackRight };
                prefabsRoad = new GameObject[] { pavementBackBottom, pavementCurbBottom, roadBlank, roadBlank, roadBlank, roadBlank, roadBlank, roadBlank, pavementCurbTop, pavementBackTop };
                break;
            case Direction.South:
                prefabsBack = new GameObject[] { pavementBackCornerTopRight, pavementBackTop, pavementBackTop, pavementBackTop, pavementBackTop, pavementBackTop, pavementBackTop, pavementBackTop, pavementBackTop, pavementBackCornerTopLeft };
                prefabsFront = new GameObject[] { pavementBackRight, pavementInvertedCornerBottomLeft, pavementCurbTop, pavementCurbTop, pavementCurbTop, pavementCurbTop, pavementCurbTop, pavementCurbTop, pavementInvertedCornerBottomRight, pavementBackLeft };
                prefabsRoad = new GameObject[] { pavementBackRight, pavementCurbRight, roadBlank, roadBlank, roadBlank, roadBlank, roadBlank, roadBlank, pavementCurbLeft, pavementBackLeft };
                break;
            case Direction.West:
                prefabsBack = new GameObject[] { pavementBackCornerBottomRight, pavementBackRight, pavementBackRight, pavementBackRight, pavementBackRight, pavementBackRight, pavementBackRight, pavementBackRight, pavementBackRight, pavementBackCornerTopRight };
                prefabsFront = new GameObject[] { pavementBackBottom, pavementInvertedCornerTopLeft, pavementCurbRight, pavementCurbRight, pavementCurbRight, pavementCurbRight, pavementCurbRight, pavementCurbRight, pavementInvertedCornerBottomLeft, pavementBackRight };
                prefabsRoad = new GameObject[] { pavementBackBottom, pavementCurbBottom, roadBlank, roadBlank, roadBlank, roadBlank, roadBlank, roadBlank, pavementCurbTop, pavementBackTop };
                break;
            default:
                return;
        }


        if (IsRoadBlocked(topLeft, direction, numberOfTiles))
        {
            if (!CanMerge(topLeft, direction, numberOfTiles))
                return;

            UpdateLastAction();
            MergeRoad(topLeft, numberOfTiles, direction, prefabsRoad);
        }
        else
        {
            UpdateLastAction();
            SpawnRoad(topLeft, numberOfTiles, direction, prefabsRoad, prefabsFront, prefabsBack);
        }
    }

    /// <summary>
    /// Used by CreateNewTiles method to spawn a new section of road if no merge is required.
    /// </summary>
    /// <param name="topLeft">The start position for the new road section.</param>
    /// <param name="numberOfTiles">Number of tiles in width/height the road will be created.</param>
    /// <param name="direction">The direction the road is being spawned.</param>
    /// <param name="prefabsRoad">Array of prefabs to depict what a road strip looks like.</param>
    /// <param name="prefabsFront">Array of prefabs to depict what a pavement front looks like.</param>
    /// <param name="prefabsBack">Aray of prefavs to depict what a pavement back looks like.</param>
    private void SpawnRoad(RelativePosition topLeft, int numberOfTiles, Direction direction, GameObject[] prefabsRoad, GameObject[] prefabsFront, GameObject[] prefabsBack)
    {
        int index = 1;
        Vector3 directionVector = HelperFunctions.DirectionToVector3(direction);

        while (index <= numberOfTiles)
        {
            if (numberOfTiles - index >= 2)
                SpawnTileStrip(new RelativePosition(topLeft.x + (index * directionVector.x),
                                                    topLeft.y + (index * directionVector.y)), prefabsRoad, direction);
            else if (numberOfTiles - index >= 1)
                SpawnTileStrip(new RelativePosition(topLeft.x + (index * directionVector.x),
                                                    topLeft.y + (index * directionVector.y)), prefabsFront, direction);
            else
                SpawnTileStrip(new RelativePosition(topLeft.x + (index * directionVector.x),
                                                    topLeft.y + (index * directionVector.y)), prefabsBack, direction);

            index++;
        }

        if (numberOfTiles == 1)
            InstantiateTileStrip(topLeft, prefabsFront, direction);
        else
            InstantiateTileStrip(topLeft, prefabsRoad, direction);

        InstantiateTileStrip(new RelativePosition(topLeft.x - directionVector.x, topLeft.y - directionVector.y), prefabsRoad, direction);

        CornerReplacer(topLeft);
        if (direction == Direction.North || direction == Direction.South)
            CornerReplacer(new RelativePosition(topLeft.x + 9, topLeft.y));
        else if (direction == Direction.East || direction == Direction.West)
            CornerReplacer(new RelativePosition(topLeft.x, topLeft.y - 9));
    }

    /// <summary>
    /// Used by CreateNewTiles method to spawn a new section of road if a merge is required.
    /// </summary>
    /// <param name="topLeft">The start position for the new road section.</param>
    /// <param name="numberOfTiles">Number of tiles in width/height the road will be created.</param>
    /// <param name="direction">The direction the road is being spawned.</param>
    /// <param name="prefabsRoad">Array of prefabs to depict what a road strip looks like.</param>
    private void MergeRoad(RelativePosition topLeft, int numberOfTiles, Direction direction, GameObject[] prefabsRoad)
    {
        int index = 1;
        int mergeStartDistance = 0;
        Vector3 directionVector = HelperFunctions.DirectionToVector3(direction);

        while (index <= numberOfTiles)
        {
            if (IsStripBlocked(new RelativePosition(topLeft.x + (index * directionVector.x),
                                                    topLeft.y + (index * directionVector.y)), direction))
            {
                mergeStartDistance = index;
                break;
            }
            else
                SpawnTileStrip(new RelativePosition(topLeft.x + (index * directionVector.x),
                                                    topLeft.y + (index * directionVector.y)), prefabsRoad, direction);
            index++;
        }

        InstantiateTileStrip(new RelativePosition(topLeft.x + (index * directionVector.x),
                                              topLeft.y + (index * directionVector.y)), prefabsRoad, direction);
        index++;
        InstantiateTileStrip(new RelativePosition(topLeft.x + (index * directionVector.x),
                                              topLeft.y + (index * directionVector.y)), prefabsRoad, direction);

        CornerReplacer(new RelativePosition(topLeft.x + (mergeStartDistance * directionVector.x),
                                            topLeft.y + (mergeStartDistance * directionVector.y)));

        if (direction == Direction.North || direction == Direction.South)
            CornerReplacer(new RelativePosition(topLeft.x + (mergeStartDistance * directionVector.x) + 9,
                                                topLeft.y + (mergeStartDistance * directionVector.y)));
        else if (direction == Direction.East || direction == Direction.West)
            CornerReplacer(new RelativePosition(topLeft.x + (mergeStartDistance * directionVector.x),
                                                topLeft.y + (mergeStartDistance * directionVector.y) - 9));

        InstantiateTileStrip(topLeft, prefabsRoad, direction);
        InstantiateTileStrip(new RelativePosition(topLeft.x - directionVector.x, topLeft.y - directionVector.y), prefabsRoad, direction);

        CornerReplacer(topLeft);
        if (direction == Direction.North || direction == Direction.South)
            CornerReplacer(new RelativePosition(topLeft.x + 9, topLeft.y));
        else if (direction == Direction.East || direction == Direction.West)
            CornerReplacer(new RelativePosition(topLeft.x, topLeft.y - 9));
    }

    public void UndoLastAction()
    {
        if (undoStateStorage == null || undoStateStorage.Count == 0)
            return;

        int tilesX = maxXPosition - minXPosition;
        int tilesY = maxYPosition - minYPosition;
        Tile currentTile = null;
        for (int x = minXPosition; x <= maxXPosition; x++)
            for (int y = minYPosition; y <= maxYPosition; y++)
                if (TilemapStorage.TryGetValue(new RelativePosition(x, y), out currentTile))
                    DestroyImmediate(currentTile.gameObject);

        currentTile = null;
        List<TileInfo> previousTiles = undoStateStorage.Pop();
        tilemapStorage = new TilemapDictionary();

        foreach (TileInfo info in previousTiles)
        {
            if (info.prefabType == null)
                continue;

            currentTile = InstantiateTile(info.prefabType, info.tileMapPosition);
        }
    }

    /// <summary>
    /// Adds the last map state to the undo stack.
    /// </summary>
    private void UpdateLastAction()
    {
        if (undoStateStorage == null)
            undoStateStorage = new Stack<List<TileInfo>>();

        List<TileInfo> undoState = new List<TileInfo>();

        int tilesX = maxXPosition - minXPosition;
        int tilesY = maxYPosition - minYPosition;

        for (int x = minXPosition; x <= maxXPosition; x++)
        {
            for (int y = minYPosition; y <= maxYPosition; y++)
            {
                Tile currentTile = null;
                RelativePosition position = new RelativePosition(x, y);
                if (TilemapStorage.TryGetValue(position, out currentTile))
                    undoState.Add(new TileInfo(position, ConvertNameToPrefab(currentTile.name)));
                else
                    undoState.Add(new TileInfo(position, null));
            }
        }

        undoStateStorage.Push(undoState);
    }

    /// <summary>
    /// Searches the road network for tiles that fit criteria for manipulation handles.
    /// </summary>
    /// <returns>A list of handle data with positions and directions the handle is facing.</returns>
    public List<HandleData> GetHandlePositions()
    {
        List<HandleData> data = new List<HandleData>();

        Tile tile = null;
        for (int x = minXPosition; x <= maxXPosition; x++)
        {
            for (int y = minYPosition; y <= maxYPosition; y++)
            {
                // Check if tile exists.
                if (TilemapStorage.ContainsKey(new RelativePosition(x, y)))
                {
                    TilemapStorage.TryGetValue(new RelativePosition(x, y), out tile);
                    if (tile != null)
                    {
                        // Checks if tile is a valid position for a handle and gets the direction of the handle
                        Direction direction = IsTileValidHandle(tile, false);
                        if (direction != Direction.None)
                            data.Add(new HandleData(0, new Vector3(tile.TilemapPosition.x * Constants.TILESIZE, tile.TilemapPosition.y * Constants.TILESIZE, 0.0f), direction));
                    }
                }
            }
        }

        return data;
    }

    /// <summary>
    /// Checks if a given tile is valid for a manipulation handle.
    /// </summary>
    /// <param name="tile">The tile to be checked.</param>
    /// <returns>The direction the arrow will face, Direction. None if invalid for a handle.</returns>
    private Direction IsTileValidHandle(Tile tile, bool recursion)
    {
        if (tile.Type != TileType.PavementBack)
            return Direction.None;

        Tile north = GetNorthTile(tile.TilemapPosition);
        Tile east = GetEastTile(tile.TilemapPosition);
        Tile south = GetSouthTile(tile.TilemapPosition);
        Tile west = GetWestTile(tile.TilemapPosition);

        Tile tempTile = null;
        if (east != null && east.Type == TileType.PavementBack)
        {
            tempTile = GetEastTile(east.TilemapPosition);
            int count = 1;
            while (count < 5)
            {
                if (tempTile != null && tempTile.Type == TileType.PavementBack)
                {
                    tempTile = GetEastTile(tempTile.TilemapPosition);
                    count++;
                }
                else
                    return Direction.None;
            }

            if (west != null && west.Type == TileType.PavementBack)
            {
                tempTile = GetWestTile(west.TilemapPosition);
                count = 1;
                while (count < 4)
                {
                    if (tempTile != null && tempTile.Type == TileType.PavementBack)
                    {
                        tempTile = GetWestTile(tempTile.TilemapPosition);
                        count++;
                    }
                    else
                        return Direction.None;
                }

                if (north == null || north.Type != TileType.PavementFront)
                {
                    if (recursion)
                        return Direction.North;

                    Tile opposingTile = null;
                    if (TilemapStorage.ContainsKey(new RelativePosition(tile.TilemapPosition.x, tile.TilemapPosition.y + 1)))
                    {
                        TilemapStorage.TryGetValue(new RelativePosition(tile.TilemapPosition.x, tile.TilemapPosition.y + 1), out opposingTile);
                        Direction opposingDirection = IsTileValidHandle(opposingTile, true);
                        if (opposingDirection != Direction.None)
                            return Direction.North;
                        else
                            return Direction.None;
                    }
                    else if (IsStripBlocked(new RelativePosition(tile.TilemapPosition.x - 4, tile.TilemapPosition.y + 1), Direction.North))
                        return Direction.None;
                    else
                        return Direction.North;
                }
                else if (south == null || south.Type != TileType.PavementFront)
                {
                    if (recursion)
                        return Direction.South;

                    Tile opposingTile = null;
                    if (TilemapStorage.ContainsKey(new RelativePosition(tile.TilemapPosition.x, tile.TilemapPosition.y - 1)))
                    {
                        TilemapStorage.TryGetValue(new RelativePosition(tile.TilemapPosition.x, tile.TilemapPosition.y - 1), out opposingTile);
                        Direction opposingDirection = IsTileValidHandle(opposingTile, true);
                        if (opposingDirection != Direction.None)
                            return Direction.South;
                        else
                            return Direction.None;
                    }
                    else if (IsStripBlocked(new RelativePosition(tile.TilemapPosition.x - 4, tile.TilemapPosition.y - 1), Direction.South))
                        return Direction.None;
                    else
                        return Direction.South;
                }
            }
        }
        else if (north != null && north.Type == TileType.PavementBack)
        {
            tempTile = GetNorthTile(north.TilemapPosition);
            int count = 1;
            while (count < 5)
            {
                if (tempTile != null && tempTile.Type == TileType.PavementBack)
                {
                    tempTile = GetNorthTile(tempTile.TilemapPosition);
                    count++;
                }
                else
                    return Direction.None;
            }

            if (south != null && south.Type == TileType.PavementBack)
            {
                tempTile = GetSouthTile(south.TilemapPosition);
                count = 1;
                while (count < 4)
                {
                    if (tempTile != null && tempTile.Type == TileType.PavementBack)
                    {
                        tempTile = GetSouthTile(tempTile.TilemapPosition);
                        count++;
                    }
                    else
                        return Direction.None;
                }

                if (east == null || east.Type != TileType.PavementFront)
                {
                    if (recursion)
                        return Direction.East;

                    Tile opposingTile = null;
                    if (TilemapStorage.ContainsKey(new RelativePosition(tile.TilemapPosition.x + 1, tile.TilemapPosition.y)))
                    {
                        TilemapStorage.TryGetValue(new RelativePosition(tile.TilemapPosition.x + 1, tile.TilemapPosition.y), out opposingTile);
                        Direction opposingDirection = IsTileValidHandle(opposingTile, true);
                        if (opposingDirection != Direction.None)
                            return Direction.East;
                        else
                            return Direction.None;
                    }
                    else if (IsStripBlocked(new RelativePosition(tile.TilemapPosition.x + 1, tile.TilemapPosition.y + 5), Direction.East))
                        return Direction.None;
                    else
                        return Direction.East;
                }
                else if (west == null || west.Type != TileType.PavementFront)
                {
                    if (recursion)
                        return Direction.West;

                    Tile opposingTile = null;
                    if (TilemapStorage.ContainsKey(new RelativePosition(tile.TilemapPosition.x - 1, tile.TilemapPosition.y)))
                    {
                        TilemapStorage.TryGetValue(new RelativePosition(tile.TilemapPosition.x - 1, tile.TilemapPosition.y), out opposingTile);
                        Direction opposingDirection = IsTileValidHandle(opposingTile, true);
                        if (opposingDirection != Direction.None)
                            return Direction.West;
                        else
                            return Direction.None;
                    }
                    else if (IsStripBlocked(new RelativePosition(tile.TilemapPosition.x - 1, tile.TilemapPosition.y + 5), Direction.West))
                        return Direction.None;
                    else
                        return Direction.West;
                }
            }
        }

        return Direction.None;
    }

    /// <summary>
    /// Spawns a new tile strip one over from the position given in the direction given using the array of prefabs to reference 
    /// which tiles to spawn.
    /// </summary>
    /// <param name="leftPos">Starting position of spawn.</param>
    /// <param name="prefabs">Tiles to be spawned.</param>
    /// <param name="direction">Direction to spawn the tiles in.</param>
    private void SpawnTileStrip(RelativePosition leftPos, GameObject[] prefabs, Direction direction)
    {
        Vector3 directionVector = HelperFunctions.DirectionToVector3(direction);

        InstantiateTile(prefabs[0], NewTilePosition(leftPos, 0, direction));
        InstantiateTile(prefabs[1], NewTilePosition(leftPos, 1, direction));
        InstantiateTile(prefabs[2], NewTilePosition(leftPos, 2, direction));
        InstantiateTile(prefabs[3], NewTilePosition(leftPos, 3, direction));
        InstantiateTile(prefabs[4], NewTilePosition(leftPos, 4, direction));
        InstantiateTile(prefabs[5], NewTilePosition(leftPos, 5, direction));
        InstantiateTile(prefabs[6], NewTilePosition(leftPos, 6, direction));
        InstantiateTile(prefabs[7], NewTilePosition(leftPos, 7, direction));
        InstantiateTile(prefabs[8], NewTilePosition(leftPos, 8, direction));
        InstantiateTile(prefabs[9], NewTilePosition(leftPos, 9, direction));
    }

    private void InstantiateTileStrip(RelativePosition leftPos, GameObject[] prefabs, Direction direction)
    {
        if (direction == Direction.North || direction == Direction.South)
            for (int i = 0; i < prefabs.Length; i++)
                InstantiateTile(prefabs[i], new RelativePosition(leftPos.x + i, leftPos.y));

        if (direction == Direction.East || direction == Direction.West)
            for (int i = 0; i < prefabs.Length; i++)
                InstantiateTile(prefabs[i], new RelativePosition(leftPos.x, leftPos.y - i));
    }

    private RelativePosition NewTilePosition(RelativePosition topLeftPosition, int number, Direction direction)
    {
        switch(direction)
        {
            case Direction.North:
            case Direction.South:
                    return new RelativePosition(topLeftPosition.x + number, topLeftPosition.y);
            case Direction.East:
            case Direction.West:
                return new RelativePosition(topLeftPosition.x, topLeftPosition.y - number);
            default:
                return topLeftPosition;
        }
    }

    /// <summary>
    /// Using the back corner tile piece, will determine and update to the current type of corner.
    /// </summary>
    /// <param name="backCornerPos">Relative position of the back corner tile that is to be updated</param>
    private void CornerReplacer(RelativePosition backCornerPos)
    {
        Vector3 direction = Vector3.zero;

        Tile currentTile = GetTile(backCornerPos.x - 2, backCornerPos.y);
        if (currentTile != null && currentTile.Type == TileType.Road)
                direction.x = -1.0f;
        else
        { 
            currentTile = GetTile(backCornerPos.x + 2, backCornerPos.y);
            if (currentTile != null && currentTile.Type == TileType.Road)
                direction.x = 1.0f;
        }
        
        currentTile = GetTile(backCornerPos.x, backCornerPos.y + 2);
        if (currentTile != null && currentTile.Type == TileType.Road)
            direction.y = 1.0f;
        else
        {
            currentTile = GetTile(backCornerPos.x, backCornerPos.y - 2);
            if (currentTile != null && currentTile.Type == TileType.Road)
                direction.y = -1.0f;
        }

        // Not a corner
        if (direction.x == 0.0f || direction.y == 0.0f)
            return;

        // TopLeft Corner
        if (direction.x == -1.0f && direction.y == 1.0f)
        {
            InstantiateTile(pavementCurbLeft, new RelativePosition(backCornerPos.x + direction.x, backCornerPos.y));
            InstantiateTile(pavementCurbTop, new RelativePosition(backCornerPos.x, backCornerPos.y + direction.y));
            InstantiateTile(pavementCornerTopLeft, new RelativePosition(backCornerPos.x + direction.x, backCornerPos.y + direction.y));
            InstantiateTile(pavementBackCornerTopLeft, backCornerPos);
        }

        // TopRight Corner
        if (direction.x == 1.0f && direction.y == 1.0f)
        {
            InstantiateTile(pavementCurbRight, new RelativePosition(backCornerPos.x + direction.x, backCornerPos.y));
            InstantiateTile(pavementCurbTop, new RelativePosition(backCornerPos.x, backCornerPos.y + direction.y));
            InstantiateTile(pavementCornerTopRight, new RelativePosition(backCornerPos.x + direction.x, backCornerPos.y + direction.y));
            InstantiateTile(pavementBackCornerTopRight, backCornerPos);
        }

        // BottomLeft Corner
        if (direction.x == -1.0f && direction.y == -1.0f)
        {
            InstantiateTile(pavementCurbLeft, new RelativePosition(backCornerPos.x + direction.x, backCornerPos.y));
            InstantiateTile(pavementCurbBottom, new RelativePosition(backCornerPos.x, backCornerPos.y + direction.y));
            InstantiateTile(pavementCornerBottomLeft, new RelativePosition(backCornerPos.x + direction.x, backCornerPos.y + direction.y));
            InstantiateTile(pavementBackCornerBottomLeft, backCornerPos);
        }

        // BottomRight Corner
        if (direction.x == 1.0f && direction.y == -1.0f)
        {
            InstantiateTile(pavementCurbRight, new RelativePosition(backCornerPos.x + direction.x, backCornerPos.y));
            InstantiateTile(pavementCurbBottom, new RelativePosition(backCornerPos.x, backCornerPos.y + direction.y));
            InstantiateTile(pavementCornerBottomRight, new RelativePosition(backCornerPos.x + direction.x, backCornerPos.y + direction.y));
            InstantiateTile(pavementBackCornerBottomRight, backCornerPos);
        }
    }
    
    private bool IsRoadBlocked(RelativePosition topLeft, Direction direction, int numberOfTiles)
    {
        Vector3 vectorDirection = HelperFunctions.DirectionToVector3(direction);
        Vector3 vectorSliceDirection = Vector3.zero;

        if (vectorDirection.x != 0.0f)
            vectorSliceDirection.y = -1.0f;
        if (vectorDirection.y != 0.0f)
            vectorSliceDirection.x = 1.0f;

        for (int y = 1; y <= numberOfTiles; y++)
            for (int x = 0; x < 10; x++)
                if (IsTileValid(new RelativePosition(topLeft.x + (vectorDirection.x * y) + (vectorSliceDirection.x * x), topLeft.y + (vectorDirection.y * y) + (vectorSliceDirection.y * x))))
                    return true;

        return false;
    }

    private bool IsStripBlocked(RelativePosition topLeft, Direction direction)
    {
        Vector3 vectorDirection = HelperFunctions.DirectionToVector3(direction);
        Vector3 vectorSliceDirection = Vector3.zero;

        if (vectorDirection.x != 0.0f)
            vectorSliceDirection.y = -1.0f;
        if (vectorDirection.y != 0.0f)
            vectorSliceDirection.x = 1.0f;
        
        for (int x = 0; x < 10; x++)
            if (IsTileValid(new RelativePosition(topLeft.x + (vectorSliceDirection.x * x), topLeft.y + (vectorSliceDirection.y * x))))
                return true;

        return false;
    }

    private bool CanMerge(RelativePosition topLeft, Direction direction, int numberOfTiles)
    {
        int tileDistance = 0;

        Vector3 vectorDirection = HelperFunctions.DirectionToVector3(direction);
        Vector3 vectorSliceDirection = Vector3.zero;

        if (vectorDirection.x != 0.0f)
            vectorSliceDirection.y = -1.0f;
        if (vectorDirection.y != 0.0f)
            vectorSliceDirection.x = 1.0f;

        for (int y = 1; y <= numberOfTiles; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                if (IsTileValid(new RelativePosition(topLeft.x + (vectorDirection.x * y) + (vectorSliceDirection.x * x), topLeft.y + (vectorDirection.y * y) + (vectorSliceDirection.y * x))))
                {
                    tileDistance = y;
                    break;
                }
            }
        }

        for (int x = 0; x < 10; x++)
            if (!IsTileValid(new RelativePosition(topLeft.x + (vectorDirection.x * tileDistance) + (vectorSliceDirection.x * x), topLeft.y + (vectorDirection.y * tileDistance) + (vectorSliceDirection.y * x))))
                return false;

        return true;
    }

    #endregion

    /// <summary>
    /// Takes the filename of the tile prefab and converts it to the prefabs gameobject.
    /// </summary>
    /// <param name="name">Filename of the tile prefab to be converted.</param>
    /// <returns></returns>
    private GameObject ConvertNameToPrefab(string name)
    {
        switch(name)
        {
            case "Road_Blank":
                return roadBlank;
            case "Road_Bottom":
                return roadCentreBottom;
            case "Road_Left":
                return roadCentreLeft;
            case "Road_Right":
                return roadCentreRight;
            case "Road_Top":
                return roadCentreTop;
            case "Pavement_BackCorner_BottomLeft":
                return pavementBackCornerBottomLeft;
            case "Pavement_BackCorner_BottomRight":
                return pavementBackCornerBottomRight;
            case "Pavement_BackCorner_TopLeft":
                return pavementBackCornerTopLeft;
            case "Pavement_BackCorner_TopRight":
                return pavementBackCornerTopRight;
            case "Pavement_Bottom_Back":
                return pavementBackBottom;
            case "Pavement_Bottom_Curb":
                return pavementCurbBottom;
            case "Pavement_InvertedCorner_BottomLeft":
                return pavementInvertedCornerBottomLeft;
            case "Pavement_InvertedCorner_BottomRight":
                return pavementInvertedCornerBottomRight;
            case "Pavement_InvertedCorner_TopLeft":
                return pavementInvertedCornerTopLeft;
            case "Pavement_InvertedCorner_TopRight":
                return pavementInvertedCornerTopRight;
            case "Pavement_Left_Back":
                return pavementBackLeft;
            case "Pavement_Left_Curb":
                return pavementCurbLeft;
            case "Pavement_Right_Back":
                return pavementBackRight;
            case "Pavement_Right_Curb":
                return pavementCurbRight;
            case "Pavement_RoadCorner_BottomLeft":
                return pavementCornerBottomLeft;
            case "Pavement_RoadCorner_BottomRight":
                return pavementCornerBottomRight;
            case "Pavement_RoadCorner_TopLeft":
                return pavementCornerTopLeft;
            case "Pavement_RoadCorner_TopRight":
                return pavementCornerTopRight;
            case "Pavement_Top_Back":
                return pavementBackTop;
            case "Pavement_Top_Curb":
                return pavementCurbTop;
            default:
                return null;
        }
    }
}