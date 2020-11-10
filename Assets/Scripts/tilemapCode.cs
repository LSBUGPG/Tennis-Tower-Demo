using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tilemapCode : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] allTiles;

    List<Vector3Int> white = new List<Vector3Int>();
    // Start is called before the first frame update
    void Start()
    {
        BoundsInt bounds = tilemap.cellBounds;
        allTiles = tilemap.GetTilesBlock(bounds);

        foreach(Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if(tile != null)
            {
                if (tile.name == "Pillar Tile Map Placeholder")
                {
                    white.Add(pos);
                }
            }
        }
        Debug.LogFormat("there are {0} white tiles", white.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
