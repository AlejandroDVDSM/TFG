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
        
        CheckFarTopRight(availableMoves);
        CheckCloseTopRight(availableMoves);
        
        CheckCloseBottomRight(availableMoves);
        CheckFarBottomRight(availableMoves);
        
        CheckFarBottomLeft(availableMoves);
        CheckCloseBottomLeft(availableMoves);
        
        CheckCloseTopLeft(availableMoves);
        CheckFarTopLeft(availableMoves);
        
        return availableMoves;
    }

    private void CheckFarTopRight(List<Vector2Int> availableMoves)
    {
        // Off the chessboard
        if (_currentRow - 2 < 0 || _currentColumn + 1 > 5) return;
        
        Vector2Int move = new Vector2Int(_currentRow - 2, _currentColumn + 1);
        AddMoves(move, availableMoves);
    }

    private void CheckCloseTopRight(List<Vector2Int> availableMoves)
    {
        // Off the chessboard
        if (_currentRow - 1 < 0 || _currentColumn + 2 > 5) return;
        
        Vector2Int move = new Vector2Int(_currentRow - 1, _currentColumn + 2);
        AddMoves(move, availableMoves);
    }

    private void CheckCloseBottomRight(List<Vector2Int> availableMoves)
    {
        // Off the chessboard
        if (_currentRow + 1 > 5 || _currentColumn + 2 > 5) return;
        
        Vector2Int move = new Vector2Int(_currentRow + 1, _currentColumn + 2);
        AddMoves(move, availableMoves);
    }

    private void CheckFarBottomRight(List<Vector2Int> availableMoves)
    {
        // Off the chessboard
        if (_currentRow + 2 > 5 || _currentColumn + 1 > 5) return;
        
        Vector2Int move = new Vector2Int(_currentRow + 2, _currentColumn + 1);
        AddMoves(move, availableMoves);
    }

    private void CheckFarBottomLeft(List<Vector2Int> availableMoves)
    {
        // Off the chessboard
        if (_currentRow + 2 > 5 || _currentColumn - 1 < 0) return;
        
        Vector2Int move = new Vector2Int(_currentRow + 2, _currentColumn - 1);
        AddMoves(move, availableMoves);
    }

    private void CheckCloseBottomLeft(List<Vector2Int> availableMoves)
    {
        // Off the chessboard
        if (_currentRow + 1 > 5 || _currentColumn - 2 < 0) return;
        
        Vector2Int move = new Vector2Int(_currentRow + 1, _currentColumn - 2);
        AddMoves(move, availableMoves);
    }

    private void CheckCloseTopLeft(List<Vector2Int> availableMoves)
    {
        // Off the chessboard
        if (_currentRow - 1 < 0 || _currentColumn - 2 < 0) return;
        
        Vector2Int move = new Vector2Int(_currentRow - 1, _currentColumn - 2);
        AddMoves(move, availableMoves);
    }

    private void CheckFarTopLeft(List<Vector2Int> availableMoves)
    {
        // Off the chessboard
        if (_currentRow - 2 < 0 || _currentColumn - 1 < 0) return;
        
        Vector2Int move = new Vector2Int(_currentRow - 2, _currentColumn - 1);
        AddMoves(move, availableMoves);
    }
    
    private void AddMoves(Vector2Int move, List<Vector2Int> availableMoves)
    {
        if (_myTile.IsThereAPieceAt(move))
        {
            // If there is an enemy piece
            var piece = _chessboardManager.GetTileAtPosition(move).transform.GetChild(0);
            if (piece.CompareTag("EnemyPiece"))
            {
                availableMoves.Add(move);
                return;
            }

            // There is a player piece
            return;
        }

        availableMoves.Add(move);
    }
}
