using System;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public const float TILESIZE = 1.6f;
    public const float TILECENTREOFFSET = TILESIZE / 2;
    public const float TILESCALE = 10.0f;
    public const float FAIL = 0.0001f;
}

public class HelperFunctions
{
    #region Direction Conversion Methods

    /// <summary>
    /// Takes a direction enum and converts it to the vector3 of the direction in world space.
    /// </summary>
    /// <param name="direction">Direction enum to be converted.</param>
    /// <returns>Vector3 direction in the direction of the enum.</returns>
    public static Vector3 DirectionToVector3(Direction direction)
    {
        switch (direction)
        {
            case Direction.None:
                return new Vector3(0.0f, 0.0f, 0.0f);
            case Direction.North:
                return new Vector3(0.0f, 1.0f, 0.0f);
            case Direction.East:
                return new Vector3(1.0f, 0.0f, 0.0f);
            case Direction.South:
                return new Vector3(0.0f, -1.0f, 0.0f);
            case Direction.West:
                return new Vector3(-1.0f, 0.0f, 0.0f);
            default:
                return new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    /// <summary>
    /// Takes a direction enum and converts it to a colour that is unique to each direction.
    /// </summary>
    /// <param name="direction">Direction enum to be converted.</param>
    /// <returns>Colour that is unique to that direction.</returns>
    public static Color DirectionToColour(Direction direction)
    {
        switch (direction)
        {
            case Direction.None:
                return Color.black;
            case Direction.North:
                return Color.red;
            case Direction.East:
                return Color.blue;
            case Direction.South:
                return Color.yellow;
            case Direction.West:
                return Color.green;
            default:
                return Color.black;
        }
    }

    /// <summary>
    /// Converts a world position to the Relative position of the tile.
    /// </summary>
    /// <param name="position">World position.</param>
    /// <returns>Relative position of the world position.</returns>
    public static RelativePosition Vector3ToRelativePosition(Vector3 position)
    {
        return new RelativePosition(Mathf.RoundToInt(position.x / Constants.TILESIZE), Mathf.RoundToInt(position.y / Constants.TILESIZE));
    }

    public static RelativePosition Vector2ToRelativePosition(Vector2 position)
    {
        return new RelativePosition(Mathf.RoundToInt(position.x / Constants.TILESIZE), Mathf.RoundToInt(position.y / Constants.TILESIZE));
    }

    public static Vector2 RelativePositionToVector2(RelativePosition position)
    {
        return new Vector2(position.x * Constants.TILESIZE, position.y * Constants.TILESIZE);
    }

    public static Direction OppositeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                return Direction.South;
            case Direction.East:
                return Direction.West;
            case Direction.South:
                return Direction.North;
            case Direction.West:
                return Direction.East;
            default:
                return Direction.None;
        }
    }

    public static Direction ClockwiseDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                return Direction.East;
            case Direction.East:
                return Direction.South;
            case Direction.South:
                return Direction.West;
            case Direction.West:
                return Direction.North;
            default:
                return Direction.None;
        }
    }

    public static Direction AntiClockwiseDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                return Direction.West;
            case Direction.East:
                return Direction.North;
            case Direction.South:
                return Direction.East;
            case Direction.West:
                return Direction.South;
            default:
                return Direction.None;
        }
    }

    public static List<T> FlipList<T>(List<T> list)
    {
        List<T> newList = new List<T>();

        for (int i = 0; i < list.Count; i++)
            newList.Add(list[list.Count - i - 1]);

        return newList;
    }

    #endregion
}

public enum Direction
{
    None, North, East, South, West
}

public enum TileType
{
    Road, PavementFront, PavementBack, InteriorFloor, InteriorWall, InteriorEdge
}

[Serializable]
public enum InteriorTileBrush
{
    Floor, Wall, Erase
}

public struct HandleData
{
    public int id;
    public Vector3 position;
    public Direction direction;

    public HandleData(int id, Vector3 pos, Direction dir)
    {
        this.id = id;
        position = pos;
        direction = dir;
    }
}

public struct TileInfo
{
    public RelativePosition tileMapPosition;
    public GameObject prefabType;

    public TileInfo(RelativePosition position, GameObject prefab)
    {
        tileMapPosition = position;
        prefabType = prefab;
    }
}

[Serializable]
public struct RelativePosition
{
    public int x;
    public int y;

    public RelativePosition(int xPos, int yPos)
    {
        x = xPos;
        y = yPos;
    }

    public RelativePosition(float xPos, float yPos)
    {
        x = (int)(Mathf.Round(xPos));
        y = (int)(Mathf.Round(yPos));
    }
}