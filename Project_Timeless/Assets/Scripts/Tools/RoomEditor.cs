using UnityEngine;

[ExecuteInEditMode]
public class RoomEditor : Tilemap
{
    #region Variables and Properties

    [SerializeField, Header("RoomEditor Variables")]
    private bool isPainting = false;

    [SerializeField]
    private GameObject floorPrefab, wallTopPrefab, wallBottomPrefab;

    [SerializeField]
    private GameObject edgeBasePrefab;
    [SerializeField]
    private Sprite edgeTopSprite, edgeBottomSprite, edgeLeftSprite, edgeRightSprite;
    [SerializeField]
    private Sprite edgeCornerTopLeftSprite, edgeCornerTopRightSprite, edgeCornerBottomLeftSprite, edgeCornerBottomRightSprite;
    [SerializeField]
    private Sprite edgeCornerInvertedTopLeftSprite, edgeCornerInvertedTopRightSprite, edgeCornerInvertedBottomLeftSprite, edgeCornerInvertedBottomRightSprite;
    [SerializeField]
    private Sprite edgeHorizontalTopLeftSprite, edgeHorizontalTopRightSprite, edgeHorizontalBottomLeftSprite, edgeHorizontalBottomRightSprite;
    [SerializeField]
    private Sprite edgeVerticalLeftTopSprite, edgeVerticalLeftBottomSprite, edgeVerticalRightTopSprite, edgeVerticalRightBottomSprite;

    [SerializeField]
    private InteriorTileBrush tileBrush;

    public bool IsPainting { get { return isPainting; } }

    #endregion

    public override void ResetTilemap()
    {
        DestroyTilemap();
    }

    public void SwitchIsPainting()
    {
        if (isPainting)
            isPainting = false;
        else
            isPainting = true;
    }

    public void CreateRoomTile(Vector3 position)
    {
        RelativePosition relPosition = ConvertWorldToMapPosition(ConvertPositionToTilePosition(position));

        // Removes the tile that was selected if it exists (will remove the joining wall tile if a wall is to be removed).
        Tile oldTile = GetTile(relPosition);
        if (oldTile != null)
        {
            if (oldTile.Type == TileType.InteriorFloor)
                RemoveTile(relPosition);
            else if (oldTile.Type == TileType.InteriorWall)
                RemoveWall(relPosition);
        }

        // If the tile to be placed is a wall then the northern tile needs removing too. If the northern tile is a wall then the full
        // wall must be removed as to not leave the top half behind.
        if (tileBrush == InteriorTileBrush.Wall)
        {
            oldTile = null;
            oldTile = GetNorthTile(relPosition);
            if (oldTile != null && oldTile.Type == TileType.InteriorWall)
                RemoveWall(oldTile.TilemapPosition);
        }

        switch(tileBrush)
        {
            case InteriorTileBrush.Floor:
                InstantiateTile(floorPrefab, relPosition);
                break;
            case InteriorTileBrush.Wall:
                InstantiateTile(wallBottomPrefab, relPosition);
                InstantiateTile(wallTopPrefab, new RelativePosition(relPosition.x, relPosition.y + 1));
                break;
        }
    }

    private bool RemoveWall(RelativePosition position)
    {
        Tile currentWall = GetTile(position);

        if (currentWall != null && currentWall.Type == TileType.InteriorWall)
        {
            Tile otherWall = GetNorthTile(position);
            if (otherWall == null || otherWall.Type != TileType.InteriorWall)
                otherWall = GetSouthTile(position);

            if (otherWall == null || otherWall.Type != TileType.InteriorWall)
            {
                Debug.LogError("Impossible Tilemap Error: Only one wall tile found.");
                return false;
            }

            RemoveTile(position);
            RemoveTile(otherWall.TilemapPosition);
            return true;
        }
        else
            Debug.LogError("Tile being removed is not a wall.");

        return false;
    }

    public void CreateRoomBorder()
    {
        RemoveRoomBorder();

        int maxX = maxXPosition;
        int maxY = maxYPosition;
        RelativePosition currentPosition = new RelativePosition();

        for (int x = minXPosition; x <= maxX; x++)
        {
            for (int y = minYPosition; y <= maxY; y++)
            {
                currentPosition = new RelativePosition(x, y);

                if (!IsTileValid(currentPosition))
                    continue;
                
                BorderCheck(GetTile(currentPosition));
            }
        }
    }

    private void BorderCheck(Tile currentTile)
    {
        RelativePosition currentPosition = new RelativePosition();
        if (currentTile.Type != TileType.InteriorEdge)
        {
            if (GetNorthTile(currentTile.TilemapPosition) == null)
                BorderAdjacentCheck(new RelativePosition(currentTile.TilemapPosition.x, currentTile.TilemapPosition.y + 1));
            if (GetEastTile(currentTile.TilemapPosition) == null)
                BorderAdjacentCheck(new RelativePosition(currentTile.TilemapPosition.x + 1, currentTile.TilemapPosition.y));
            if (GetSouthTile(currentTile.TilemapPosition) == null)
                BorderAdjacentCheck(new RelativePosition(currentTile.TilemapPosition.x, currentTile.TilemapPosition.y - 1));
            if (GetWestTile(currentTile.TilemapPosition) == null)
                BorderAdjacentCheck(new RelativePosition(currentTile.TilemapPosition.x - 1, currentTile.TilemapPosition.y));

            currentPosition = new RelativePosition(currentTile.TilemapPosition.x - 1, currentTile.TilemapPosition.y + 1); // North West
            if (GetTile(currentPosition) == null)
                BorderAdjacentCheck(currentPosition);
            currentPosition = new RelativePosition(currentTile.TilemapPosition.x + 1, currentTile.TilemapPosition.y + 1); // North East
            if (GetTile(currentPosition) == null)
                BorderAdjacentCheck(currentPosition);
            currentPosition = new RelativePosition(currentTile.TilemapPosition.x - 1, currentTile.TilemapPosition.y - 1); // South West
            if (GetTile(currentPosition) == null)
                BorderAdjacentCheck(currentPosition);
            currentPosition = new RelativePosition(currentTile.TilemapPosition.x + 1, currentTile.TilemapPosition.y - 1); // South East
            if (GetTile(currentPosition) == null)
                BorderAdjacentCheck(currentPosition);
        }
    }

    private bool BorderAdjacentCheck(RelativePosition position)
    {
        Tile currentTile = InstantiateTile(edgeBasePrefab, position);

        Tile north = GetNorthTile(position);
        Tile east = GetEastTile(position);
        Tile south = GetSouthTile(position);
        Tile west = GetWestTile(position);

        if (north != null && north.Type != TileType.InteriorEdge)
            AddLayerToTile(edgeBottomSprite, position);
        if (east != null && east.Type != TileType.InteriorEdge)
            AddLayerToTile(edgeLeftSprite, position);
        if (south != null && south.Type != TileType.InteriorEdge)
            AddLayerToTile(edgeTopSprite, position);
        if (west != null && west.Type != TileType.InteriorEdge)
            AddLayerToTile(edgeRightSprite, position);

        return BorderCornerCheck(position, north, east, south, west);
    }

    private bool BorderCornerCheck(RelativePosition position, Tile north, Tile east, Tile south, Tile west)
    {
        Tile northWest = GetTile(new RelativePosition(position.x - 1, position.y + 1));
        Tile northEast = GetTile(new RelativePosition(position.x + 1, position.y + 1));
        Tile southWest = GetTile(new RelativePosition(position.x - 1, position.y - 1));
        Tile southEast = GetTile(new RelativePosition(position.x + 1, position.y - 1));

        // North West
        if (north != null && north.Type != TileType.InteriorEdge)
        {
            if (west != null && west.Type != TileType.InteriorEdge)
                AddLayerToTile(edgeCornerInvertedBottomRightSprite, position);
            else
                AddLayerToTile(edgeHorizontalBottomLeftSprite, position);
        }
        else if (west != null && west.Type != TileType.InteriorEdge)
            AddLayerToTile(edgeVerticalRightTopSprite, position);
        else if (northWest != null && northWest.Type != TileType.InteriorEdge)
            AddLayerToTile(edgeCornerBottomRightSprite, position);
            
        // North East
        if (north != null && north.Type != TileType.InteriorEdge)
        {
            if (east != null && east.Type != TileType.InteriorEdge)
                AddLayerToTile(edgeCornerInvertedBottomLeftSprite, position);
            else
                AddLayerToTile(edgeHorizontalBottomRightSprite, position);
        }
        else if (east != null && east.Type != TileType.InteriorEdge)
            AddLayerToTile(edgeVerticalLeftTopSprite, position);
        else if (northEast != null && northEast.Type != TileType.InteriorEdge)
            AddLayerToTile(edgeCornerBottomLeftSprite, position);
            
        // South West
        if (south != null && south.Type != TileType.InteriorEdge)
        {
            if (west != null && west.Type != TileType.InteriorEdge)
                AddLayerToTile(edgeCornerInvertedTopRightSprite, position);
            else
                AddLayerToTile(edgeHorizontalTopLeftSprite, position);
        }
        else if (west != null && west.Type != TileType.InteriorEdge)
            AddLayerToTile(edgeVerticalRightBottomSprite, position);
        else if (southWest != null && southWest.Type != TileType.InteriorEdge)
            AddLayerToTile(edgeCornerTopRightSprite, position);

        // South East
        if (south != null && south.Type != TileType.InteriorEdge)
        {
            if (east != null && east.Type != TileType.InteriorEdge)
                AddLayerToTile(edgeCornerInvertedTopLeftSprite, position);
            else
                AddLayerToTile(edgeHorizontalTopRightSprite, position);
        }
        else if (east != null && east.Type != TileType.InteriorEdge)
            AddLayerToTile(edgeVerticalLeftBottomSprite, position);
        else if (southEast != null && southEast.Type != TileType.InteriorEdge)
            AddLayerToTile(edgeCornerTopLeftSprite, position);

        return true;
    }

    private void RemoveRoomBorder()
    {
        int maxX = maxXPosition;
        int maxY = maxYPosition;
        Tile currentTile = null;
        RelativePosition currentPosition = new RelativePosition();

        for (int x = minXPosition; x <= maxX; x++)
        {
            for (int y = minYPosition; y <= maxY; y++)
            {
                currentPosition = new RelativePosition(x, y);

                if (!IsTileValid(currentPosition))
                    continue;

                currentTile = GetTile(currentPosition);
                if (currentTile.Type == TileType.InteriorEdge)
                    RemoveTile(currentPosition);
            }
        }
    }

    #region Util Methods

    /// <summary>
    /// Converts a world position into the world position for the closest tile.
    /// </summary>
    /// <param name="position">World position (e.g. the mouse position)</param>
    /// <returns>The position of the closest tile.</returns>
    private Vector3 ConvertPositionToTilePosition(Vector3 position)
    {
        if (position.x > 0.0f)
            position.x += Constants.TILECENTREOFFSET;
        else
            position.x -= Constants.TILECENTREOFFSET;

        if (position.y > 0.0f)
            position.y += Constants.TILECENTREOFFSET;
        else
            position.y -= Constants.TILECENTREOFFSET;

        int x = (int)(position.x * 10);
        int y = (int)(position.y * 10);

        x /= (int)(Constants.TILESIZE * 10);
        x *= (int)(Constants.TILESIZE * 10);
        y /= (int)(Constants.TILESIZE * 10);
        y *= (int)(Constants.TILESIZE * 10);

        position.x = ((float)x) / 10;
        position.y = ((float)y) / 10;
        position.z = 0.0f;

        return position;
    }

    #endregion
}
