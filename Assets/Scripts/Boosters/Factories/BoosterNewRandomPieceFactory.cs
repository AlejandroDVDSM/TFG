using UnityEngine;

public class BoosterNewRandomPieceFactory : MonoBehaviour, IBoosterFactory
{
    public void ApplyBooster()
    {
        Debug.Log("'New Random Piece' bought");
        FindObjectOfType<ChessPieceSpawner>().SetNextRandomChessPiece();
    }
}
