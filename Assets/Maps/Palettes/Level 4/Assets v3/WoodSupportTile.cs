using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

[CreateAssetMenu(menuName ="Level4/Custom Tiles/Wood Support")]
public class WoodSupportTile : TileBase
{
    public Sprite[] diags = new Sprite[4];

    int Mod(int a, int b)
    {
        return (a % b + b) % b;
    }
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = diags[(Mod(position.y, 2) * 2) + Mod(position.x,2) ];
    }


}
