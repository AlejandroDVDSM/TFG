using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceMerger : MonoBehaviour
{
    private void Start()
    {
        Tile myTile = GetInWhichTileIAm();
        myTile.CheckNearbyTiles();
    }
    
    private Tile GetInWhichTileIAm()
    {
        return GetComponentInParent<Tile>();
    }
    
}
