using System.Collections.Generic;
using UnityEngine;

public class ChessPieceMovement: MonoBehaviour
{
    private List<Vector2Int> availableMoves = new List<Vector2Int>();

    private ChessboardManager _chessboardManager;
    
    public bool IsMoving = false;
    private ChessPieceSpawner _chessPieceGenerator;

    private void Start()
    {
        _chessboardManager = FindObjectOfType<ChessboardManager>();
        _chessPieceGenerator = FindObjectOfType<ChessPieceSpawner>();
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
        transform.localPosition = new Vector2(.05f, .3f);
        GetComponentInParent<Tile>().IsFree = false; // New parent
        AudioManager.Instance.Play("ChesspieceMove");
        targetTile.CheckNearbyTiles(); // Check if this chess piece can make new connections after moving it

        _chessPieceGenerator.SetNextRandomChessPiece();
        IsMoving = false;
        GetBackToNormal();
        availableMoves.Clear();
        
    }

    public void MoveAndEat(Tile targetTile)
    {
        Destroy(targetTile.transform.GetChild(0).gameObject);
        AudioManager.Instance.Play("ChesspieceEat");
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
