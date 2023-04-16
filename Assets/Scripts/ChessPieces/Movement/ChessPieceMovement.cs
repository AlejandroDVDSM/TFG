using System.Collections.Generic;
using UnityEngine;

public class ChessPieceMovement: MonoBehaviour
{
    private List<Vector2Int> availableMoves = new List<Vector2Int>();

    private ChessboardManager _chessboardManager;
    
    public bool IsMoving = false;

    private void Start()
    {
        _chessboardManager = FindObjectOfType<ChessboardManager>();
    }

    public void SetAllAvailableMoves()
    {
        availableMoves = GetComponent<IMovement>().GetAllAvailableMoves();

        if (availableMoves.Count > 0)
            HighlightAvailableTiles();
        else
            IsMoving = false;
    }
    
    public void Move(Tile targetTile)
    {
        GetComponent<ChessPieceConnections>().RemoveConnections();
        GetComponentInParent<Tile>().IsFree = true; // Old parent

        transform.SetParent(targetTile.transform);
        transform.localPosition = new Vector2(.05f, .3f);;
        GetComponentInParent<Tile>().IsFree = false; // New parent
        
        targetTile.CheckNearbyTiles(); // Check if this chess piece can make new connections after moving it
        
        IsMoving = false;
        GetBackToNormal();
        availableMoves.Clear();
        //GameStateManager.instance.UpdateGameState(GameState.EnemyTurn);
    }

    public void MoveAndEat(Tile targetTile)
    {
        Destroy(targetTile.transform.GetChild(0).gameObject);
        Move(targetTile);
    }
    
    private void HighlightAvailableTiles()
    {
        foreach (var availableMove in availableMoves)
        {
            Tile tileAt = _chessboardManager.GetTileAtPosition(availableMove);
            tileAt.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.78f);
            tileAt.tag = "TileToMove";
        }
    }

    public void GetBackToNormal()
    {
        foreach (var availableMove in availableMoves)
        {
            Tile tileAt = _chessboardManager.GetTileAtPosition(availableMove);
            tileAt.GetComponent<SpriteRenderer>().color = Color.white;
            tileAt.tag = "Tile";
        }
    }
}
