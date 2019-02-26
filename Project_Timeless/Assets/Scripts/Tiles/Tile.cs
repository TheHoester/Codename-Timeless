using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour, ITile
{
    [SerializeField]
    private RelativePosition tilemapPosition;

    [SerializeField]
    private TileType type;
    [SerializeField]
    private bool isWalkable;
    [SerializeField]
    private List<GameObject> layers;

    public RelativePosition TilemapPosition { get { return tilemapPosition; } }
    public TileType Type { get { return type; } }
    public bool IsWalkable {  get { return isWalkable; } }

    public void InitializeTile(RelativePosition position)
    {
        tilemapPosition = position;
        transform.position = new Vector3(position.x * Constants.TILESIZE, position.y * Constants.TILESIZE, 0.0f);

        layers = new List<GameObject>();
    }

    public void AddLayer(GameObject obj)
    {
        if (layers == null)
            layers = new List<GameObject>();

        layers.Add(obj);
        obj.transform.SetParent(transform);
        obj.transform.position = new Vector3(transform.position.x, transform.position.y, -0.01f * layers.Count);
    }
}