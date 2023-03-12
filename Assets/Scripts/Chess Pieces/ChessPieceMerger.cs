using UnityEngine;

public class ChessPieceMerger : MonoBehaviour
{
    [SerializeField] private GameObject _chessPieceUpgraded;
    
    private void Start()
    {
        Tile myTile = GetInWhichTileIAm();
        myTile.CheckNearbyTiles();
    }
    
    private Tile GetInWhichTileIAm()
    {
        return GetComponentInParent<Tile>();
    }
    
    // Called from the event "OnChessPieceConnected" in the editor
    public void MergePieces()
    {
        ChessPiece thisChessPiece = GetComponent<ChessPiece>();

        foreach (var chessPiece in thisChessPiece.connections)
        {
            Destroy(chessPiece.gameObject);
            chessPiece.GetComponentInParent<Tile>().IsFree = true;
        }
        
        Instantiate(_chessPieceUpgraded, GetInWhichTileIAm().transform);
        Destroy(gameObject);
    }
}
