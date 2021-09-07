using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu(menuName = "Level4/Custom Tiles/Advanced Rule Tile")]
public class AdvancedRuleTile : RuleTile<AdvancedRuleTile.Neighbor>
{
    public bool alwaysConnect;
    public bool checkSelf;
    public TileBase[] tilesToConnect;

    public class Neighbor : RuleTile.TilingRule.Neighbor
    {
        public const int Any = 3;
        public const int Specified = 4;
        public const int Nothing = 5;
    }


    public override bool RuleMatch(int neighbor, TileBase other)
    {
        switch (neighbor)
        {
            case Neighbor.This: return CheckThis(other);
            case Neighbor.NotThis: return CheckNotThis(other);
            case Neighbor.Any: return CheckAny(other);
            case Neighbor.Specified: return CheckSpecified(other);
            case Neighbor.Nothing: return CheckNothing(other);
        }
        return base.RuleMatch(neighbor, other);
    }
    bool CheckThis(TileBase tile) 
    {
        if (!alwaysConnect) return tile == this;
        else return tilesToConnect.Contains(tile) || tile == this;
    }
    bool CheckNotThis(TileBase tile)
    {
        return tile != this;
    }
    bool CheckAny(TileBase tile)
    {
        if (checkSelf) return tile != null;
        else return tile != null && tile != this;
    }
    bool CheckSpecified(TileBase tile)
    {
        return (tilesToConnect != null) && tilesToConnect.Contains(tile);
    }
    bool CheckNothing(TileBase tile)
    {
        return tile == null;
    }
}
