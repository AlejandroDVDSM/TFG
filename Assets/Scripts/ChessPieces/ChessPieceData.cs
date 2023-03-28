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
        GetComponent<SpriteRenderer>().sprite = _chessPiece.sprite;
    }
    
    public Sprite GetSprite()
    {
        return _chessPiece.sprite;
    }
    
    public Type GetChessPieceType()
    {
        return _chessPiece.type;
    }
    
    public int GetPoints()
    {
        return _chessPiece.points;
    }
    
    public Tile GetInWhichTileIAm()
    {
        return GetComponentInParent<Tile>();
    }    
}
