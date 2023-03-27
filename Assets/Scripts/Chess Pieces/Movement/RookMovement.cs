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

        _myTile = GetComponent<ChessPieceData>().GetInWhichTileIAm();
        _currentRow = _myTile.Coordinates.x;
        _currentColumn = _myTile.Coordinates.y;

        // Check above
        for (int row = _currentRow - 1; row >= 0; row--)
        {
            Vector2Int move = new Vector2Int(row, _currentColumn);
            if (AddMovesUntilBlocked(move, availableMoves)) break;
        }
        
        // Check right
        for (int column = _currentColumn + 1; column <= 5; column++)
        {
            Vector2Int move = new Vector2Int(_currentRow, column);
            if (AddMovesUntilBlocked(move, availableMoves)) break;
        }
        
        // Check below
        for (int row = _currentRow + 1; row <= 5; row++)
        {
            Vector2Int move = new Vector2Int(row, _currentColumn);
            if (AddMovesUntilBlocked(move, availableMoves)) break;
        }
        
        // Check left
        for (int column = _currentColumn - 1; column >= 0; column--)
        {
            Vector2Int move = new Vector2Int(_currentRow, column);
            if (AddMovesUntilBlocked(move, availableMoves)) break;
        }
        
        return availableMoves;
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
