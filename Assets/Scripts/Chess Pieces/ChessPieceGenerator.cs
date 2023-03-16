using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = System.Random;

public class ChessPieceGenerator : MonoBehaviour
{
    
    [Header("Player's pieces")]
    [SerializeField] private GameObject[] _chessPieces;
    [SerializeField] private TMP_Text _nextChessPieceType;
    [SerializeField] private Image _nextChessPieceImage;

    [Header("Enemy's pieces")] 
    [SerializeField] private GameObject[] _enemyPieces;

    public GameObject GenerateNextChessPiece()
    {
        int randomIndex = new Random().Next(0, _chessPieces.Length); // First parameter: included --- Second parameter: excluded
        GameObject nextChessPiece = _chessPieces[randomIndex];

        _nextChessPieceType.text = nextChessPiece.name;
        _nextChessPieceImage.sprite = nextChessPiece.GetComponent<SpriteRenderer>().sprite;
        return nextChessPiece;
    }

    public GameObject GetRandomEnemy()
    {
        int randomIndex = new Random().Next(0, _enemyPieces.Length); // First parameter: included --- Second parameter: excluded
        return _enemyPieces[randomIndex];
    }
}
