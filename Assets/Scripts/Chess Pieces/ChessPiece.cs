using System.Collections;
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
        if (chessPiece.type != type) return;
        
        connections.Add(chessPiece);
        if (chessPiece.IsConnected()) connections.Add(chessPiece.GetConnection());
        chessPiece.connections.Add(this);
        
        if (connections.Count == 2) OnChessPieceConnected.Invoke();
    }
    
    public bool IsConnected()
    {
        if (connections.Count > 0) return true;
        return false;
    }
    
    public ChessPiece GetConnection()
    {
        return connections.FirstOrDefault(piece => piece != this);
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
