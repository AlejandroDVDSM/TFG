using System;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : MonoBehaviour, IMovement
{
    private Tile _myTile;
    private ChessboardManager _chessboardManager;
    private int _currentRow, _currentColumn;

    private void Start()
    {
        _chessboardManager = FindObjectOfType<ChessboardManager>();
    }

    public List<Vector2Int> GetAllAvailableMoves()
    {
        List<Vector2Int> availableMoves = new List<Vector2Int>();

        _myTile = GetComponentInParent<Tile>();
        _currentRow = _myTile.Coordinates.x;
        _currentColumn = _myTile.Coordinates.y;

        availableMoves = CheckFarRightTop(availableMoves);
        availableMoves = CheckCloseRightTop(availableMoves);
        
        availableMoves = CheckFarRightBottom(availableMoves);
        availableMoves = CheckCloseRightBottom(availableMoves);
        
        availableMoves = CheckFarLeftTop(availableMoves);
        availableMoves = CheckCloseLeftTop(availableMoves);
        
        availableMoves = CheckFarLeftBottom(availableMoves);
        availableMoves = CheckCloseLeftBottom(availableMoves);
        
        return availableMoves;
    }

    private List<Vector2Int> CheckFarRightTop(List<Vector2Int> availableMoves)
    {
        if (!(_currentRow - 2 >= 0 && _currentColumn + 1 <= 5)) return availableMoves;
        
        Vector2Int coordinates = new Vector2Int(_currentRow - 2, _currentColumn + 1);

        if (!_myTile.IsThereAPieceAt(coordinates))
        {
            availableMoves.Add(coordinates);
        } else {
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece")) {
                availableMoves.Add(coordinates);
            }
        }
        
        return availableMoves;
    }

    private List<Vector2Int> CheckCloseRightTop(List<Vector2Int> availableMoves)
    {
        if (!(_currentRow - 1 >= 0 && _currentColumn + 2 <= 5)) return availableMoves;
        
        Vector2Int coordinates = new Vector2Int(_currentRow - 1, _currentColumn + 2);

        if (!_myTile.IsThereAPieceAt(coordinates))
        {
            availableMoves.Add(coordinates);
        } else {
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece")) {
                availableMoves.Add(coordinates);
            }
        }

        return availableMoves;
    }
    
    private List<Vector2Int> CheckFarRightBottom(List<Vector2Int> availableMoves)
    {
        if (!(_currentRow + 2 <= 5 && _currentColumn + 1 <= 5)) return availableMoves;
        
        Vector2Int coordinates = new Vector2Int(_currentRow + 2, _currentColumn + 1);
        
        if (!_myTile.IsThereAPieceAt(coordinates)) {
            availableMoves.Add(coordinates);
        } else {
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece")) {
                availableMoves.Add(coordinates);
            }
        }

        return availableMoves;
    }
    
    private List<Vector2Int> CheckCloseRightBottom(List<Vector2Int> availableMoves)
    {
        if (!(_currentRow + 1 <= 5 && _currentColumn + 2 <= 5)) return availableMoves;
        
        Vector2Int coordinates = new Vector2Int(_currentRow + 1, _currentColumn + 2);
        
        if (!_myTile.IsThereAPieceAt(coordinates)) {
            availableMoves.Add(coordinates);
        } else {
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece")) {
                availableMoves.Add(coordinates);
            }
        }
        
        return availableMoves;
    }
    
    private List<Vector2Int> CheckFarLeftTop(List<Vector2Int> availableMoves)
    {
        if (!(_currentRow - 2 >= 0 && _currentColumn - 1 >= 0)) return availableMoves;
        
        Vector2Int coordinates = new Vector2Int(_currentRow - 2, _currentColumn - 1);
        
        if (!_myTile.IsThereAPieceAt(coordinates)) {
            availableMoves.Add(coordinates);
        } else {
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece")) {
                availableMoves.Add(coordinates);
            }
        }
        
        return availableMoves;
    }
    
    private List<Vector2Int> CheckCloseLeftTop(List<Vector2Int> availableMoves)
    {
        if (!(_currentRow - 1 >= 0 && _currentColumn - 2 >= 0)) return availableMoves;
        
        Vector2Int coordinates = new Vector2Int(_currentRow - 1, _currentColumn - 2);
        
        if (!_myTile.IsThereAPieceAt(coordinates)) {
            availableMoves.Add(coordinates);
        } else {
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece")) {
                availableMoves.Add(coordinates);
            }
        }
        
        return availableMoves;
    }
    
    private List<Vector2Int> CheckFarLeftBottom(List<Vector2Int> availableMoves)
    {
        if (!(_currentRow + 2 <= 5 && _currentColumn - 1 >= 0)) return availableMoves;
        
        Vector2Int coordinates = new Vector2Int(_currentRow + 2, _currentColumn - 1);
        
        if (!_myTile.IsThereAPieceAt(coordinates)) {
            availableMoves.Add(coordinates);
        } else {
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece")) {
                availableMoves.Add(coordinates);
            }
        }

        
        return availableMoves;
    }
    
    private List<Vector2Int> CheckCloseLeftBottom(List<Vector2Int> availableMoves)
    {
        if (!(_currentRow + 1 <= 5 && _currentColumn - 2 >= 0)) return availableMoves;
        
        Vector2Int coordinates = new Vector2Int(_currentRow + 1, _currentColumn - 2);
        
        if (!_myTile.IsThereAPieceAt(coordinates)) {
            availableMoves.Add(coordinates);
        } else {
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece")) {
                availableMoves.Add(coordinates);
            }
        }
        
        return availableMoves;
    }
}
