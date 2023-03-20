using System.Collections.Generic;
using UnityEngine;

public class ChessPieceMovement: MonoBehaviour
{
    private List<Vector2Int> availableMoves = new List<Vector2Int>();

    private ChessboardManager _chessboardManager;
    
    public bool IsMoving = false;
    private Vector2 _defaultLocalPosition = new Vector2(.05f, .3f); // Position relative to the parent

    private void Start()
    {
        _chessboardManager = FindObjectOfType<ChessboardManager>();
    }

    public void Move(Tile targetTile)
    {
        GetComponent<ChessPiece>().RemoveConnections();
        GetComponentInParent<Tile>().IsFree = true; // Old parent
        // Need to kill children if it has a piece in it
        
        transform.SetParent(targetTile.transform);
        transform.localPosition = _defaultLocalPosition;
        GetComponentInParent<Tile>().IsFree = false; // New parent
        
        targetTile.CheckNearbyTiles(); // Check if this chess piece can make new connections after moving it
        
        IsMoving = false;
        GetBackToNormal();
        availableMoves.Clear();
        GameStateManager.instance.UpdateGameState(GameState.EnemyTurn);
    }
    
    private void HighlightAvailableTiles()
    {
        foreach (var availableMove in availableMoves)
        {
            Debug.Log("ROW: " + availableMove.x + "COLUMN: " + availableMove.y);
            Tile tileAt = _chessboardManager.GetTileAtPosition(availableMove);
            tileAt.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.78f);
            tileAt.tag = "TileToMove";
        }
    }

    private void GetBackToNormal()
    {
        foreach (var availableMove in availableMoves)
        {
            Tile tileAt = _chessboardManager.GetTileAtPosition(availableMove);
            tileAt.GetComponent<SpriteRenderer>().color = Color.white;
            tileAt.tag = "Tile";

        }
    }

    public void SetAllAvailableMoves()
    {
        availableMoves = GetComponent<IMovement>().GetAllAvailableMoves();
        HighlightAvailableTiles();
    }
}
