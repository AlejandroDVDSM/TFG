using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = System.Random;

public class ChessPieceGenerator : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _nextChessPieceType;
    [SerializeField] private Image _nextChessPieceImage;
    
    [Header("Player's pieces")]
    [SerializeField] private GameObject[] _chessPieces;

    [Header("Enemy's pieces")] 
    [SerializeField] private GameObject[] _enemyPieces;
    
    [Header("Player flags")]
    [SerializeField] private bool _onlyGeneratePlayerPawn = false;
    [SerializeField] private bool _onlyGeneratePlayerBishop = false;
    [SerializeField] private bool _onlyGeneratePlayerKnight = false;
    [SerializeField] private bool _onlyGeneratePlayerRook = false;
    [SerializeField] private bool _onlyGeneratePlayerQueen = false;
    [SerializeField] private bool _onlyGeneratePlayerKing = false;
    
    [Header("Enemy flags")]
    [SerializeField] private bool _onlyGenerateEnemyPawn = false;
    [SerializeField] private bool _onlyGenerateEnemyBishop = false;
    [SerializeField] private bool _onlyGenerateEnemyKnight = false;
    [SerializeField] private bool _onlyGenerateEnemyRook = false;
    [SerializeField] private bool _onlyGenerateEnemyQueen = false;
    [SerializeField] private bool _onlyGenerateEnemyKing = false;
    
    public GameObject GenerateNextChessPiece()
    {
        int randomIndex;
        if (_onlyGeneratePlayerPawn) // If flag is activated - Test purposes
            randomIndex = 0;
        else if (_onlyGeneratePlayerBishop)
            randomIndex = 1;
        else if (_onlyGeneratePlayerKnight)
            randomIndex = 2;
        else if (_onlyGeneratePlayerRook)
            randomIndex = 3;
        else if (_onlyGeneratePlayerQueen)
            randomIndex = 4;
        else if (_onlyGeneratePlayerKing)
            randomIndex = 5;
        else
            randomIndex = new Random().Next(0, _chessPieces.Length); // First parameter: included --- Second parameter: excluded*/
        
        GameObject nextChessPiece = _chessPieces[randomIndex];
        ChessPieceData nextChessPieceData = nextChessPiece.GetComponent<ChessPieceData>();
        
        _nextChessPieceType.text = nextChessPieceData.GetChessPieceType().ToString();
        _nextChessPieceImage.sprite = nextChessPieceData.GetSprite();
        return nextChessPiece;
    }

    public GameObject GetRandomEnemy()
    {
        int randomIndex;
        if (_onlyGenerateEnemyPawn) // If flag is activated - Test purposes
            randomIndex = 0;
        else if (_onlyGenerateEnemyBishop)
            randomIndex = 1;
        else if (_onlyGenerateEnemyKnight)
            randomIndex = 2;
        else if (_onlyGenerateEnemyRook)
            randomIndex = 3;
        else if (_onlyGenerateEnemyQueen)
            randomIndex = 4;
        else if (_onlyGenerateEnemyKing)
            randomIndex = 5;
        else
            randomIndex = new Random().Next(0, _enemyPieces.Length); // First parameter: included --- Second parameter: excluded
        
        return _enemyPieces[randomIndex];
    }
}
