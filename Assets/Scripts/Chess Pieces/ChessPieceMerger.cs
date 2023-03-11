using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChessPieceMerger : MonoBehaviour
{
    [SerializeField] private GameObject _newChessPiecePrefab; // In a future this will have to be removed
    
    private void Start()
    {
        Tile myTile = GetInWhichTileIAm();
        myTile.CheckNearbyTiles();
    }
    
    private Tile GetInWhichTileIAm()
    {
        return GetComponentInParent<Tile>();
    }
    
    // Called from the event "OnChessPieceConnected" in the editor
    public void MergePieces()
    {
        ChessPiece thisChessPiece = GetComponent<ChessPiece>();

        foreach (var chessPiece in thisChessPiece.connections)
        {
            chessPiece.GetComponentInParent<Tile>().IsFree = true;
            Destroy(chessPiece.gameObject);
        }
        
        Instantiate(_newChessPiecePrefab, GetInWhichTileIAm().transform);
        Destroy(gameObject);
    }    
    
}
