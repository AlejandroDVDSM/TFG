using System.Collections.Generic;
using UnityEngine;

public class PawnMovement : MonoBehaviour, IMovement
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
        
        CheckOneInFront(availableMoves);
        CheckTopRight(availableMoves);
        CheckTopLeft(availableMoves);
        
        return availableMoves;
    }

    private void CheckOneInFront(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 0) return;
        
        if (!_myTile.IsThereAPieceAboveMe())
        {
            Vector2Int coordinates = new Vector2Int(_currentRow - 1, _currentColumn); 
            availableMoves.Add(coordinates);
        }
    }
    
    private void CheckTopRight(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 0 || _currentColumn == 5) return;
        
        Vector2Int coordinates = new Vector2Int(_currentRow - 1, _currentColumn + 1);
        AddMoves(coordinates, availableMoves);
    }

    private void CheckTopLeft(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 0 || _currentColumn == 0) return;

        Vector2Int coordinates = new Vector2Int(_currentRow - 1, _currentColumn - 1);
        AddMoves(coordinates, availableMoves);
    }

    private void AddMoves(Vector2Int move, List<Vector2Int> availableMoves)
    {
        if (_myTile.IsThereAPieceAt(move)) {
            // If it is an enemy piece
            var piece = _chessboardManager.GetTileAtPosition(move).transform.GetChild(0);
            if (piece.CompareTag("EnemyPiece"))
            {
                availableMoves.Add(move);
            }
        }
    }
}
