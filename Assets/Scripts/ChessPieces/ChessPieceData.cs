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
        string path = $"Chesspieces/{GetTeam()}/{GetChessPieceType()}.png";
        ITarget target = GetComponent<ITarget>();
        FindObjectOfType<Storage>().InitializeSprite(path, target);
    }

    public Team GetTeam()
    {
        return _chessPiece.Team;
    }
    
    public Type GetChessPieceType()
    {
        return _chessPiece.Type;
    }
    
    private int GetPoints()
    {
        return _chessPiece.Points;
    }
    
    public Tile GetInWhichTileIAm()
    {
        return GetComponentInParent<Tile>();
    }    
}
