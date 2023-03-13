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
        ChessPiece thisChessPiece = GetComponent<ChessPiece>();

        foreach (var chessPiece in thisChessPiece.connections)
        {
            chessPiece.GetInWhichTileIAm().IsFree = true;
            Destroy(chessPiece.gameObject);
        }

        isMergeAvailable = true;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (!isMergeAvailable) return;
        ChessPiece thisChessPiece = GetComponent<ChessPiece>();
        Instantiate(_chessPieceUpgraded, thisChessPiece.GetInWhichTileIAm().transform);
    }
}
