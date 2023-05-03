using UnityEngine;

public class BoosterKnightFactory : MonoBehaviour, IBoosterFactory
{
    [SerializeField] private GameObject _knightPrefab;

    public void ApplyBooster()
    {
        var chessPieceSpawner = FindObjectOfType<ChessPieceSpawner>(); 
        chessPieceSpawner.SetNextChessPiece(_knightPrefab.GetComponent<ChessPieceData>());
    }
}
