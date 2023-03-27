using System;
using UnityEngine;
using UnityEngine.Events;

public class ChessPieceMerger : MonoBehaviour
{
    [SerializeField] private GameObject _chessPieceUpgraded;
    private bool isMergeAvailable = false;
    
    // Called from the event "OnChessPieceConnected()" in the editor
    public void MergePieces()
    {
        ChessPieceConnections thisChessPieceConnections = GetComponent<ChessPieceConnections>();

        foreach (var chessPiece in thisChessPieceConnections.connections)
        {
            chessPiece.GetComponent<ChessPieceData>().GetInWhichTileIAm().IsFree = true;
            Destroy(chessPiece.gameObject);
        }

        isMergeAvailable = true;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (!isMergeAvailable) return;
        // transform.parent = tile where I am
        Instantiate(_chessPieceUpgraded, transform.parent);
    }
}
