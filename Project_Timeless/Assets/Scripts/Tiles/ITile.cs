
public interface ITile
{
    RelativePosition TilemapPosition { get; }
    TileType Type { get; }
    bool IsWalkable { get; }
    
    void InitializeTile(RelativePosition position);
}