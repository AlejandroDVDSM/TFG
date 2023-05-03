using UnityEngine;

public class BoosterRookFactory : MonoBehaviour, IBoosterFactory
{
    [SerializeField] private GameObject _rookPrefab;

    public void ApplyBooster()
    {
        var chessPieceSpawner = FindObjectOfType<ChessPieceSpawner>(); 
        chessPieceSpawner.SetNextChessPiece(_rookPrefab.GetComponent<ChessPieceData>());
    }
}
