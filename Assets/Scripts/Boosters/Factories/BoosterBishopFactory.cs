using UnityEngine;

public class BoosterBishopFactory : MonoBehaviour, IBoosterFactory
{
    [SerializeField] private GameObject _bishopPrefab;

    public void ApplyBooster()
    {
        var chessPieceSpawner = FindObjectOfType<ChessPieceSpawner>(); 
        chessPieceSpawner.SetNextChessPiece(_bishopPrefab.GetComponent<ChessPieceData>());
    }
}
