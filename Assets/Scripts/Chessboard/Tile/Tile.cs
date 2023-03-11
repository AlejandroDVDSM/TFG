using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private ChessboardManager _chessboardManager;

    private Vector2 _coordinates;
    
    public bool IsFree { get; set; } = true;

    private void Start()
    {
        _chessboardManager = FindObjectOfType<ChessboardManager>();
    }
    
    public void SetCoordinates(Vector2 coordinates)
    {
        _coordinates = coordinates;
    }
    
    public void CheckNearbyTiles()
    {
        Debug.Log($"Tile where I am: {name}" );

        IsThereAPieceAboveMe();
        IsThereAPieceBelowMe();
        IsThereAPieceAtMyLeft();
        IsThereAPieceAtMyRight();
    }
    
    private bool IsThereAPieceAboveMe()
    {
        if (_coordinates.x == 0) return false; // Off the chessboard
        
        Vector2 tileAboveCoordinates = new Vector2(_coordinates.x - 1, _coordinates.y);
        Tile tileAboveMe = _chessboardManager.GetTileAtPosition(tileAboveCoordinates);

        if (tileAboveMe.GetComponentInChildren<ChessPieceMerger>() != null)
        {
            Debug.Log("ABOVE: true");
            return true;
        }

        Debug.Log("ABOVE: false");
        return false;
    }
    
    private bool IsThereAPieceBelowMe()
    {
        if (_coordinates.x == 5) return false; // Off the chessboard
        
        Vector2 tileBelowCoordinates = new Vector2(_coordinates.x + 1, _coordinates.y);
        Tile tileBelowMe = _chessboardManager.GetTileAtPosition(tileBelowCoordinates);

        if (tileBelowMe.GetComponentInChildren<ChessPieceMerger>() != null)
        {
            Debug.Log("BELOW: true");
            return true;
        }

        Debug.Log("BELOW: false");
        return false;
    }

    private bool IsThereAPieceAtMyLeft()
    {
        if (_coordinates.y == 0) return false; // Off the chessboard
        
        Vector2 tileAtMyLeftCoordinates = new Vector2(_coordinates.x, _coordinates.y - 1);
        Tile tileAtMyLeft = _chessboardManager.GetTileAtPosition(tileAtMyLeftCoordinates);

        if (tileAtMyLeft.GetComponentInChildren<ChessPieceMerger>() != null)
        {
            Debug.Log("LEFT: true");
            return true;
        }
    
        Debug.Log("LEFT: false");
        return false;
    }
    
    private bool IsThereAPieceAtMyRight()
    {
        if (_coordinates.y == 5) return false; // Off the chessboard
        
        Vector2 tileAtMyRightCoordinates = new Vector2(_coordinates.x, _coordinates.y + 1);
        Tile tileAtMyRight = _chessboardManager.GetTileAtPosition(tileAtMyRightCoordinates);

        if (tileAtMyRight.GetComponentInChildren<ChessPieceMerger>() != null)
        {
            Debug.Log("RIGHT: true");
            return true;
        }
    
        Debug.Log("RIGHT: false");
        return false;
    }
}