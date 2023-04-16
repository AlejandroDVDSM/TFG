using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterNewRandomPieceFactory : MonoBehaviour, IBoosterFactory
{
    public void ApplyBooster()
    {
        Debug.Log("Manolito Molinete");
        FindObjectOfType<ChessPieceSpawner>().SetNextRandomChessPiece();
    }
}
