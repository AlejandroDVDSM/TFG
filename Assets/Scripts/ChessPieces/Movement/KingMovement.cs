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

        availableMoves = CheckOneInFront(availableMoves);
        availableMoves = CheckOneInTopRight(availableMoves);
        availableMoves = CheckOneInRight(availableMoves);
        availableMoves = CheckOneInBottomRight(availableMoves);
        availableMoves = CheckOneInBottom(availableMoves);
        availableMoves = CheckOneInBottomLeft(availableMoves);
        availableMoves = CheckOneInLeft(availableMoves);
        availableMoves = CheckOneInTopLeft(availableMoves);

        return availableMoves;
    }

    private List<Vector2Int> CheckOneInFront(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 0) return availableMoves;
        
        Vector2Int coordinates = new Vector2Int(_currentRow - 1, _currentColumn);
        if (_myTile.IsThereAPieceAboveMe())
        {
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece"))
                availableMoves.Add(coordinates);
        }
        else
            availableMoves.Add(coordinates);
        
        return availableMoves;
    }

    private List<Vector2Int> CheckOneInTopRight(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 0 || _currentColumn == 5) return availableMoves;
        
        Vector2Int coordinates = new Vector2Int(_currentRow - 1, _currentColumn + 1);
        if (_myTile.IsThereAPieceAt(coordinates))
        {
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece"))
                availableMoves.Add(coordinates);
        }
        else
            availableMoves.Add(coordinates);
        
        return availableMoves;
    }
    
    private List<Vector2Int> CheckOneInRight(List<Vector2Int> availableMoves)
    {
        if (_currentColumn == 5) return availableMoves;
        
        Vector2Int coordinates = new Vector2Int(_currentRow, _currentColumn + 1);
        if (_myTile.IsThereAPieceAt(coordinates))
        {
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece"))
                availableMoves.Add(coordinates);
        }
        else
            availableMoves.Add(coordinates);
        
        return availableMoves;
    }
    
    private List<Vector2Int> CheckOneInBottomRight(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 5 || _currentColumn == 5) return availableMoves;
        
        Vector2Int coordinates = new Vector2Int(_currentRow + 1, _currentColumn + 1);
        if (_myTile.IsThereAPieceAt(coordinates))
        {
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece"))
                availableMoves.Add(coordinates);
        }
        else
            availableMoves.Add(coordinates);
        
        return availableMoves;
    }
    
    private List<Vector2Int> CheckOneInBottom(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 5) return availableMoves;
        
        Vector2Int coordinates = new Vector2Int(_currentRow + 1, _currentColumn);
        if (_myTile.IsThereAPieceAt(coordinates))
        {
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece"))
                availableMoves.Add(coordinates);
        }
        else
            availableMoves.Add(coordinates);
        
        return availableMoves;
    }
    
    private List<Vector2Int> CheckOneInBottomLeft(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 5 || _currentColumn == 0) return availableMoves;
        
        Vector2Int coordinates = new Vector2Int(_currentRow + 1, _currentColumn - 1);
        if (_myTile.IsThereAPieceAt(coordinates))
        {
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece"))
                availableMoves.Add(coordinates);
        }
        else
            availableMoves.Add(coordinates);
        
        return availableMoves;
    }
    
    private List<Vector2Int> CheckOneInLeft(List<Vector2Int> availableMoves)
    {
        if (_currentColumn == 0) return availableMoves;
        
        Vector2Int coordinates = new Vector2Int(_currentRow, _currentColumn - 1);
        if (_myTile.IsThereAPieceAt(coordinates))
        {
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece"))
                availableMoves.Add(coordinates);
        }
        else
            availableMoves.Add(coordinates);
        
        return availableMoves;
    }
    
    private List<Vector2Int> CheckOneInTopLeft(List<Vector2Int> availableMoves)
    {
        if (_currentRow == 0 || _currentColumn == 0) return availableMoves;
        
        Vector2Int coordinates = new Vector2Int(_currentRow - 1, _currentColumn - 1);
        if (_myTile.IsThereAPieceAt(coordinates))
        {
            if (_chessboardManager.GetTileAtPosition(coordinates).transform.GetChild(0).CompareTag("EnemyPiece"))
                availableMoves.Add(coordinates);
        }
        else
            availableMoves.Add(coordinates);
        
        return availableMoves;
    }
}
