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
        
        CheckTopRight(availableMoves);
        CheckBottomRight(availableMoves);
        CheckBottomLeft(availableMoves);
        CheckTopLeft(availableMoves);

        return availableMoves;
    }

    private void CheckTopRight(List<Vector2Int> availableMoves)
    {
        for (int row = _currentRow - 1, column = _currentColumn + 1; row >= 0 && column <= 5; row--, column++)
        {
            Vector2Int move = new Vector2Int(row, column);
            if (AddMovesUntilBlocked(move, availableMoves)) break;
        }
    }
    
    private void CheckBottomRight(List<Vector2Int> availableMoves)
    {
        for (int row = _currentRow + 1, column = _currentColumn + 1; row <= 5 && column <= 5; row++, column++)
        {
            Vector2Int move = new Vector2Int(row, column);
            if (AddMovesUntilBlocked(move, availableMoves)) break;

        }
    }
    
    private void CheckBottomLeft(List<Vector2Int> availableMoves)
    {
        for (int row = _currentRow + 1, column = _currentColumn - 1; row <= 5 && column >= 0; row++, column--)
        {
            Vector2Int move = new Vector2Int(row, column);
            if (AddMovesUntilBlocked(move, availableMoves)) break;

        }
    }
    
    private void CheckTopLeft(List<Vector2Int> availableMoves)
    {
        for (int row = _currentRow - 1, column = _currentColumn - 1; row >= 0 && column >= 0; row--, column--)
        {
            Vector2Int move = new Vector2Int(row, column);
            if (AddMovesUntilBlocked(move, availableMoves)) break;

        }
    }

    // Returns true if its path is blocked by a piece.
    private bool AddMovesUntilBlocked(Vector2Int move, List<Vector2Int> availableMoves)
    {
        if (_myTile.IsThereAPieceAt(move))
        {
            // If there is an enemy piece
            var piece = _chessboardManager.GetTileAtPosition(move).transform.GetChild(0);
            if (piece.CompareTag("EnemyPiece"))
            {
                availableMoves.Add(move);
                return true;
            }

            // There is a player piece
            return true;
        }

        availableMoves.Add(move);
        return false;
    }
}
