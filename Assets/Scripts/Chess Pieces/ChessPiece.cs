using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ChessPiece : MonoBehaviour
{
    public Type type;
    public List<ChessPiece> connections;

    public UnityEvent OnChessPieceConnected = new UnityEvent();

    public void ConnectPieces(ChessPiece chessPiece)
    {
        if (!SameTypes(chessPiece.type)) return;
        
        connections.Add(chessPiece);
        if (chessPiece.IsConnected()) connections.Add(chessPiece.GetConnection());
        chessPiece.connections.Add(this);
        
        if (connections.Count >= 2) OnChessPieceConnected.Invoke(); // == 2 (?)
    }
    
    private bool IsConnected()
    {
        if (connections.Count > 0) return true;
        return false;
    }
    
    private ChessPiece GetConnection()
    {
        return connections.FirstOrDefault(piece => piece != this);
    }

    private bool SameTypes(Type chessPieceType)
    {
        return type == chessPieceType;
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
