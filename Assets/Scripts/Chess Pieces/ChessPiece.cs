using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ChessPiece : MonoBehaviour
{
    public Type type;
    public List<ChessPiece> connections;

    public UnityEvent onChessPieceConnected = new UnityEvent();

    private void Start()
    {
        Tile myTile = GetInWhichTileIAm();
        
        name = $"{type} {myTile.Coordinates.x} {myTile.Coordinates.y}";
        
        myTile.CheckNearbyTiles();
    }
    
    public void ConnectPieces(ChessPiece chessPieceToConnect)
    {
        if (!SameTypes(chessPieceToConnect.type)) return;
        
        AddConnections(chessPieceToConnect);
        chessPieceToConnect.AddConnections(this);
        
        foreach (var piece in chessPieceToConnect.connections.Where(piece => piece != this))
        {
            piece.AddConnections(this);
        }
        
        /*if (connections.Count >= 2) */onChessPieceConnected.Invoke();
    }

    private void AddConnections(ChessPiece chessPiece)
    {
        if (!connections.Contains(chessPiece)) connections.Add(chessPiece);
        connections = connections.Union(chessPiece.connections.Where(piece => piece != this)).ToList(); // Union() excludes duplicated elements.
    }

    private bool SameTypes(Type chessPieceType)
    {
        return type == chessPieceType;
    }

    public void CheckIfMergeIsPossible()
    {
        if (connections.Count >= 2) GetComponent<ChessPieceMerger>().MergePieces();
    } 
    
    public Tile GetInWhichTileIAm()
    {
        return GetComponentInParent<Tile>();
    }
}

public enum Type
{
    Pawn,
    Bishop,
    Knight,
    Rook,
    Queen,
    King
}
