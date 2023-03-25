using System.Collections.Generic;
using Unity.VisualScripting;
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
        
        availableMoves = CheckOneInFront(availableMoves);
        availableMoves = CheckDiagonalRight(availableMoves);
        availableMoves = CheckDiagonalLeft(availableMoves);
        
        return availableMoves;
    }

    private List<Vector2Int> CheckOneInFront(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 0) return availableMoves;
        
        if (!_myTile.IsThereAPieceAboveMe())
        {
            Vector2Int coordinatesAbove = new Vector2Int(_currentRow - 1, _currentColumn); 
            availableMoves.Add(coordinatesAbove);
        }
        
        return availableMoves;
    }

    private List<Vector2Int> CheckDiagonalRight(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 0 || _currentColumn == 5) return availableMoves;
        
        Vector2Int coordinates = new Vector2Int(_currentRow - 1, _currentColumn + 1);
        if (_myTile.IsThereAPieceAt(coordinates))
        {
            // If is an enemy piece
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece"))
                availableMoves.Add(coordinates);
        }
        
        return availableMoves;
    }

    private List<Vector2Int> CheckDiagonalLeft(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 0 || _currentColumn == 0) return availableMoves;

        Vector2Int coordinates = new Vector2Int(_currentRow - 1, _currentColumn - 1);
        if (_myTile.IsThereAPieceAt(coordinates)) {
            // If is an enemy piece
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece"))
                availableMoves.Add(coordinates);
        }

        return availableMoves;
    }
}
