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
    
    [Header("Flags")]
    [SerializeField] private bool _onlyGeneratePlayerPawn = false;
    [SerializeField] private bool _onlyGenerateEnemyPawn = false;

    public GameObject GenerateNextChessPiece()
    {
        int randomIndex;
        if (_onlyGeneratePlayerPawn) // If flag is activated - Test purposes
        {
            Debug.Log("Flag <_onlyGeneratePlayerPawn> is TRUE...");
            randomIndex = 0;
        }
        else
            randomIndex = new Random().Next(0, _chessPieces.Length); // First parameter: included --- Second parameter: excluded
        
        GameObject nextChessPiece = _chessPieces[randomIndex];

        _nextChessPieceType.text = nextChessPiece.name;
        _nextChessPieceImage.sprite = nextChessPiece.GetComponent<SpriteRenderer>().sprite;
        return nextChessPiece;
    }

    public GameObject GetRandomEnemy()
    {
        int randomIndex;
        if (_onlyGenerateEnemyPawn) // If flag is activated - Test purposes
        {
            Debug.Log("Flag <_onlyGenerateEnemyPawn> is TRUE...");
            randomIndex = 0;
        }
        else
            randomIndex = new Random().Next(0, _enemyPieces.Length); // First parameter: included --- Second parameter: excluded
        
        return _enemyPieces[randomIndex];
    }
}
