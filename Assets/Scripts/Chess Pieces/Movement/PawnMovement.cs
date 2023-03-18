using System.Collections.Generic;
using UnityEngine;

public class PawnMovement : MonoBehaviour, IMovement
{
    public bool isAboutToMove = false;
    
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
        Vector2Int coordinatesDiagonalRight = new Vector2Int(myTile.Coordinates.x - 1, myTile.Coordinates.y + 1);
        if (myTile.IsThereAPieceAt(coordinatesDiagonalRight)) {
            // SHOULD CHECK IF IT IS AN ENEMY PIECE
            
            availableMoves.Add(coordinatesDiagonalRight);
        }

        // Diagonal Left - Kill
        Vector2Int coordinatesDiagonalLeft = new Vector2Int(myTile.Coordinates.x - 1, myTile.Coordinates.y - 1);
        if (myTile.IsThereAPieceAt(coordinatesDiagonalLeft)) {
            // SHOULD CHECK IF IT IS AN ENEMY PIECE
            
            availableMoves.Add(coordinatesDiagonalLeft);
        }

        HighlightAvailableTiles(availableMoves);
        return availableMoves;
    }

    private void HighlightAvailableTiles(List<Vector2Int> availableMoves)
    {
        var chessboardManager = FindObjectOfType<ChessboardManager>();
        foreach (var availableMove in availableMoves)
        {
            Tile tileAt = chessboardManager.GetTileAtPosition(availableMove);
            tileAt.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.78f);
        }
    }

    private void GetBackToNormal()
    {
        GetComponentInParent<SpriteRenderer>().color = Color.white;
    }
}
