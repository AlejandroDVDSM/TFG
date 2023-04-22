using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = System.Random;

public class ChessPieceGenerator : MonoBehaviour
{
    [Header("Player's pieces")]
    [SerializeField] private GameObject[] _chessPieces;

    [SerializeField] private TopPanelDisplay _topPanelDisplay;

    public GameObject GenerateNextChessPiece()
    {
        int randomIndex = new Random().Next(0, _chessPieces.Length); // First parameter: included --- Second parameter: excluded*/
        
        GameObject nextChessPiece = _chessPieces[randomIndex];
        
        ChessPieceData nextChessPieceData = nextChessPiece.GetComponent<ChessPieceData>();
        //Type nextChessPieceType = nextChessPieceData.GetChessPieceType();
        //Sprite nextChessPieceSprite = FindObjectOfType<FirebaseStorageTest>().GetImage($"chesspieces/{nextChessPieceData.GetTeam()}/{nextChessPieceData.GetChessPieceType()}.png")//nextChessPieceData.GetSprite();
        _topPanelDisplay.UpdateTopPanel(/*nextChessPieceType, nextChessPieceSprite*/nextChessPieceData);
        
        return nextChessPiece;
    }
}
