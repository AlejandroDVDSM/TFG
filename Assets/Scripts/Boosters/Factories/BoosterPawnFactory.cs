using UnityEngine;

public class BoosterPawnFactory : MonoBehaviour, IBoosterFactory
{
    [SerializeField] private GameObject _pawnPrefab;

    public void ApplyBooster()
    {
        var chessPieceSpawner = FindObjectOfType<ChessPieceSpawner>(); 
        chessPieceSpawner.SetNextChessPiece(_pawnPrefab.GetComponent<ChessPieceData>());
    }
}
