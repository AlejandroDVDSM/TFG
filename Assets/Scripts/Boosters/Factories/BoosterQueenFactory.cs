using UnityEngine;

public class BoosterQueenFactory : MonoBehaviour, IBoosterFactory
{
    [SerializeField] private GameObject _queenPrefab;

    public void ApplyBooster()
    {
        var chessPieceSpawner = FindObjectOfType<ChessPieceSpawner>(); 
        chessPieceSpawner.SetNextChessPiece(_queenPrefab.GetComponent<ChessPieceData>());
    }
}
