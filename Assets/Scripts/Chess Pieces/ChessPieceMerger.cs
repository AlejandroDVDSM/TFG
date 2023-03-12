using UnityEngine;

public class ChessPieceMerger : MonoBehaviour
{
    [SerializeField] private GameObject _chessPieceUpgraded;
    
    // Called from the event "OnChessPieceConnected" in the editor
    public void MergePieces()
    {
        ChessPiece thisChessPiece = GetComponent<ChessPiece>();

        foreach (var chessPiece in thisChessPiece.connections)
        {
            chessPiece.GetComponentInParent<Tile>().IsFree = true;
            Destroy(chessPiece.gameObject);
        }
        
        GameObject spawnedChessPiece = Instantiate(_chessPieceUpgraded, thisChessPiece.GetInWhichTileIAm().transform);
        Destroy(gameObject);
    }
}
