using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PawnMovement : MonoBehaviour, IMovement
{
    public List<Vector2Int> GetAllAvailableMoves()
    {
        List<Vector2Int> availableMoves = new List<Vector2Int>();

        Tile myTile = GetComponentInParent<Tile>();    
        
        // One in front
        if (!myTile.IsThereAPieceAboveMe())
        {
            Vector2Int coordinatesAbove = new Vector2Int(myTile.Coordinates.x - 1, myTile.Coordinates.y); 
            availableMoves.Add(coordinatesAbove);
        }
        
        // Diagonal Right - Kill
        ChessboardManager chessboardManager = FindObjectOfType<ChessboardManager>();
        Vector2Int coordinatesDiagonalRight = new Vector2Int(myTile.Coordinates.x - 1, myTile.Coordinates.y + 1);
        if (myTile.IsThereAPieceAt(coordinatesDiagonalRight)) {
            // If is an enemy piece
            if (chessboardManager.GetTileAtPosition(coordinatesDiagonalRight).GetComponentInChildren<Transform>().CompareTag("EnemyPiece"))
                availableMoves.Add(coordinatesDiagonalRight);
        }

        // Diagonal Left - Kill
        Vector2Int coordinatesDiagonalLeft = new Vector2Int(myTile.Coordinates.x - 1, myTile.Coordinates.y - 1);
        if (myTile.IsThereAPieceAt(coordinatesDiagonalLeft)) {
            // If is an enemy piece
            if (chessboardManager.GetTileAtPosition(coordinatesDiagonalLeft).GetComponentInChildren<Transform>().CompareTag("EnemyPiece"))
                availableMoves.Add(coordinatesDiagonalLeft);
        }

        GetComponent<ChessPieceMovement>().HighlightAvailableTiles(availableMoves);
        return availableMoves;
    }    
}
