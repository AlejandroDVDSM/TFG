using System.Collections.Generic;
using UnityEngine;

public class BishopMovement : MonoBehaviour, IMovement
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

        _myTile = GetComponent<ChessPieceData>().GetInWhichTileIAm();
        _currentRow = _myTile.Coordinates.x;
        _currentColumn = _myTile.Coordinates.y;

        availableMoves = CheckRightTop(availableMoves);
        availableMoves = CheckLeftTop(availableMoves);
        availableMoves = CheckRightBottom(availableMoves);
        availableMoves = CheckLeftBottom(availableMoves);

        return availableMoves;
    }

    private List<Vector2Int> CheckRightTop(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 0 || _currentColumn == 5) return availableMoves;

        for (int row = _currentRow - 1, column = _currentColumn + 1; row >= 0 && column <= 5; row--, column++)
        {
            Vector2Int coordinates = new Vector2Int(row, column);

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
    
    private List<Vector2Int> CheckLeftTop(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 0 || _currentColumn == 0) return availableMoves;

        for (int row = _currentRow - 1, column = _currentColumn - 1; row >= 0 && column >= 0; row--, column--)
        {
            Vector2Int coordinates = new Vector2Int(row, column);

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
    
    private List<Vector2Int> CheckRightBottom(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 5 || _currentColumn == 5) return availableMoves;

        for (int row = _currentRow + 1, column = _currentColumn + 1; row <= 5 && column <= 5; row++, column++)
        {
            Vector2Int coordinates = new Vector2Int(row, column);

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
    
    private List<Vector2Int> CheckLeftBottom(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 5 || _currentColumn == 0) return availableMoves;

        for (int row = _currentRow + 1, column = _currentColumn - 1; row <= 5 && column >= 0; row++, column--)
        {
            Vector2Int coordinates = new Vector2Int(row, column);

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
