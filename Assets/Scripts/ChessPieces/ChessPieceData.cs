using UnityEngine;

public class ChessPieceData : MonoBehaviour
{
    [SerializeField] private Piece _chessPiece;

    private void Start()
    {
        SetSprite();
        
        FindObjectOfType<PointsManager>().Add(GetPoints());
    }

    private void SetSprite()
    {
       FindObjectOfType<FirebaseStorageTest>().GetImage($"chesspieces/{GetTeam()}/{GetChessPieceType()}.png", GetComponent<SpriteRenderer>());
    }

    public Team GetTeam()
    {
        return _chessPiece.Team;
    }
    
    public Type GetChessPieceType()
    {
        return _chessPiece.Type;
    }
    
    public int GetPoints()
    {
        return _chessPiece.Points;
    }
    
    public Tile GetInWhichTileIAm()
    {
        return GetComponentInParent<Tile>();
    }    
}
