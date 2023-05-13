using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ChessPieceConnections : MonoBehaviour
{
    private Type _type;
    
    public List<ChessPieceConnections> connections;

    public UnityEvent onChessPieceConnected = new();

    private void Start()
    {
        ChessPieceData chessPieceData = GetComponent<ChessPieceData>();
        Tile myTile = chessPieceData.GetInWhichTileIAm();
        _type = chessPieceData.GetChessPieceType();
        name = $"{_type} {myTile.Coordinates.x} {myTile.Coordinates.y}";
        
        myTile.CheckNearbyTiles();
    }
    
    public void ConnectPieces(ChessPieceConnections chessPieceConnectionsToConnect)
    {
        if (!SameTypes(chessPieceConnectionsToConnect._type)) 
            return;
        
        AddConnections(chessPieceConnectionsToConnect);
        chessPieceConnectionsToConnect.AddConnections(this);
        
        foreach (var piece in chessPieceConnectionsToConnect.connections.Where(piece => piece != this))
            piece.AddConnections(this);
        
        onChessPieceConnected.Invoke();
    }

    private void AddConnections(ChessPieceConnections chessPieceConnections)
    {
        if (!connections.Contains(chessPieceConnections)) 
            connections.Add(chessPieceConnections);
        
        connections = connections.Union(chessPieceConnections.connections.Where(piece => piece != this)).ToList(); // Union() excludes duplicated elements.
    }

    public void RemoveConnections()
    {
        foreach (var connection in connections)
        {
            connection.connections.Remove(this);
        }
        
        connections.Clear();
    }

    private bool SameTypes(Type chessPieceType)
    {
        return _type == chessPieceType;
    }

    public void CheckIfMergeIsPossible()
    {
        if (connections.Count >= 2 && _type != Type.King) 
            GetComponent<ChessPieceMerger>().MergePieces();
    }
}
