using System.Collections.Generic;
using UnityEngine;

public class KingMovement : MonoBehaviour, IMovement
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

        _myTile = GetComponentInParent<Tile>();
        _currentRow = _myTile.Coordinates.x;
        _currentColumn = _myTile.Coordinates.y;
        
        CheckAbove(availableMoves);
        CheckTopRight(availableMoves);
        CheckRight(availableMoves);
        CheckBottomRight(availableMoves);
        CheckBelow(availableMoves);
        CheckBottomLeft(availableMoves);
        CheckLeft(availableMoves);
        CheckTopLeft(availableMoves);

        return availableMoves;
    }

    private void CheckAbove(List<Vector2Int> availableMoves)
    {
        // Off the chessboard
        if (_currentRow == 0) return;

        Vector2Int move = new Vector2Int(_currentRow - 1, _currentColumn);
        AddMoves(move, availableMoves);        
    }
    
    private void CheckTopRight(List<Vector2Int> availableMoves)
    {
        // Off the chessboard
        if (_currentRow == 0 || _currentColumn == 5) return;

        Vector2Int move = new Vector2Int(_currentRow - 1, _currentColumn + 1);
        AddMoves(move, availableMoves);        
    }
    
    private void CheckRight(List<Vector2Int> availableMoves)
    {
        // Off the chessboard
        if (_currentColumn == 5) return;

        Vector2Int move = new Vector2Int(_currentRow, _currentColumn + 1);
        AddMoves(move, availableMoves);        
    }
    
    private void CheckBottomRight(List<Vector2Int> availableMoves)
    {
        // Off the chessboard
        if (_currentRow == 5 || _currentColumn == 5) return;

        Vector2Int move = new Vector2Int(_currentRow + 1, _currentColumn + 1);
        AddMoves(move, availableMoves);        
    }
    
    private void CheckBelow(List<Vector2Int> availableMoves)
    {
        // Off the chessboard
        if (_currentRow == 5) return;

        Vector2Int move = new Vector2Int(_currentRow + 1, _currentColumn);
        AddMoves(move, availableMoves);        
    }
    
    private void CheckBottomLeft(List<Vector2Int> availableMoves)
    {
        // Off the chessboard
        if (_currentRow == 5 || _currentColumn == 0) return;

        Vector2Int move = new Vector2Int(_currentRow + 1, _currentColumn - 1);
        AddMoves(move, availableMoves);        
    }
    
    private void CheckLeft(List<Vector2Int> availableMoves)
    {
        // Off the chessboard
        if (_currentColumn == 0) return;

        Vector2Int move = new Vector2Int(_currentRow, _currentColumn - 1);
        AddMoves(move, availableMoves);        
    }
    
    private void CheckTopLeft(List<Vector2Int> availableMoves)
    {
        // Off the chessboard
        if (_currentRow == 0 || _currentColumn == 0) return;

        Vector2Int move = new Vector2Int(_currentRow - 1, _currentColumn - 1);
        AddMoves(move, availableMoves);        
    }

    private void AddMoves(Vector2Int move, List<Vector2Int> availableMoves)
    {
        if (_myTile.IsThereAPieceAt(move))
        {
            var piece = _chessboardManager.GetTileAtPosition(move).transform.GetChild(0);
            if (piece.CompareTag("EnemyPiece"))
                availableMoves.Add(move);
        }
        else
            availableMoves.Add(move);
    }
}
