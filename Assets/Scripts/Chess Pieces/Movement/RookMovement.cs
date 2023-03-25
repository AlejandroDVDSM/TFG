using System.Collections.Generic;
using UnityEngine;

public class RookMovement : MonoBehaviour, IMovement
{
    private ChessboardManager _chessboardManager;

    private Tile _myTile;

    private int _currentRow, _currentColumn;
    
    private void Start()
    {
        _chessboardManager = FindObjectOfType<ChessboardManager>();
    }

    public List<Vector2Int> GetAllAvailableMoves()
    {
        List<Vector2Int> availableMoves = new List<Vector2Int>();

        _myTile = GetComponent<ChessPiece>().GetInWhichTileIAm();
        _currentRow = _myTile.Coordinates.x;
        _currentColumn = _myTile.Coordinates.y;

        availableMoves = CheckBelow(availableMoves);
        availableMoves = CheckAbove(availableMoves);
        availableMoves = CheckRight(availableMoves);
        availableMoves = CheckLeft(availableMoves);
        
        return availableMoves;
    }

    private List<Vector2Int> CheckBelow(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 5) return availableMoves;

        for (int i = _currentRow + 1; i <= 5; i++)
        {
            Vector2Int coordinates = new Vector2Int(i, _currentColumn);
            // If there is no piece
            if (!_myTile.IsThereAPieceAt(coordinates))
            {
                availableMoves.Add(coordinates);
            }
            else
            {
                var piece = _chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0);
                
                if (piece.CompareTag("EnemyPiece"))
                {
                    availableMoves.Add(coordinates);
                    break;
                    
                }

                if (piece.CompareTag("PlayerPiece")) break;
            }
        }

        return availableMoves;
    }

    private List<Vector2Int> CheckAbove(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 0) return availableMoves;
        
        for (int i = _currentRow - 1; i >= 0; i--)
        {
            Vector2Int coordinates = new Vector2Int(i, _currentColumn);
            // If there is no piece
            if (!_myTile.IsThereAPieceAt(coordinates))
            {
                availableMoves.Add(coordinates);
            }
            else
            {
                var piece = _chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0);
                
                if (piece.CompareTag("EnemyPiece"))
                {
                    availableMoves.Add(coordinates);
                    break;
                    
                }

                if (piece.CompareTag("PlayerPiece")) break;
            }
        }

        return availableMoves;
    }
    
    private List<Vector2Int> CheckRight(List<Vector2Int> availableMoves) {
        if (_currentColumn == 5) return availableMoves;
        
        for (int i = _currentColumn + 1; i <= 5; i++)
        {
            Vector2Int coordinates = new Vector2Int(_currentRow, i);
            // If there is no piece
            if (!_myTile.IsThereAPieceAt(coordinates))
            {
                availableMoves.Add(coordinates);
            }
            else
            {
                var piece = _chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0);
                
                if (piece.CompareTag("EnemyPiece"))
                {
                    availableMoves.Add(coordinates);
                    break;
                    
                }

                if (piece.CompareTag("PlayerPiece")) break;
            }
        }
    
        return availableMoves;
    }
    
    private List<Vector2Int> CheckLeft(List<Vector2Int> availableMoves) {
        if (_currentColumn == 0) return availableMoves;
        
        for (int i = _currentColumn - 1; i >= 0; i--)
        {
            Vector2Int coordinates = new Vector2Int(_currentRow, i);
            // If there is no piece
            if (!_myTile.IsThereAPieceAt(coordinates))
            {
                availableMoves.Add(coordinates);
            }
            else
            {
                var piece = _chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0);
                
                if (piece.CompareTag("EnemyPiece"))
                {
                    availableMoves.Add(coordinates);
                    break;
                    
                }

                if (piece.CompareTag("PlayerPiece")) break;
            }
        }
        
        return availableMoves;
    }
}
