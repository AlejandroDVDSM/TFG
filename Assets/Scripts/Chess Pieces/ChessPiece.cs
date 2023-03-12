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
        name = $"{type} {GetComponentInParent<Tile>().Coordinates.x} {GetComponentInParent<Tile>().Coordinates.y}";
        
        Tile myTile = GetInWhichTileIAm();
        myTile.CheckNearbyTiles();
    }
    
    public void ConnectPieces(ChessPiece chessPiece)
    {
        if (!SameTypes(chessPiece.type)) return;
        
        connections.Add(chessPiece);
        if (chessPiece.IsConnected()) connections.Add(chessPiece.GetConnection());
        chessPiece.connections.Add(this);
        
        if (connections.Count == 2) onChessPieceConnected.Invoke();
    }    
    
    private bool IsConnected()
    {
        return connections.Count > 0;
    }
    
    private ChessPiece GetConnection()
    {
        return connections.FirstOrDefault(piece => piece != this);
    }

    private bool SameTypes(Type chessPieceType)
    {
        return type == chessPieceType;
    }

    public void RemoveNullConnectionsAfterMerge()
    {
        foreach (var connection in connections)
        {
            if (connection == null) connections.Remove(connection);
        }
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
