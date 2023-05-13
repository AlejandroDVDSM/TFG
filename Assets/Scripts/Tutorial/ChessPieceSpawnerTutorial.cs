using System.Collections;
using UnityEngine;

public class ChessPieceSpawnerTutorial : MonoBehaviour
{
    [SerializeField] private ChessboardManager _chessboardManager;
    [SerializeField] private GameObject _pawnPrefab;
    [SerializeField] private GameObject _enemyPawnPrefab;

    [SerializeField] private TopPanelDisplay _topPanelDisplay;
    
    void Start()
    {
        TutorialStateManager.OnTutorialStateChanged += SpawnPawn;
        TutorialStateManager.OnTutorialStateChanged += MergePawn;
        TutorialStateManager.OnTutorialStateChanged += SpawnEnemyPawn;
        
        _topPanelDisplay.UpdateTopPanel(_pawnPrefab.GetComponent<ChessPieceData>());
    }

    private void OnDestroy()
    {
        TutorialStateManager.OnTutorialStateChanged -= SpawnPawn;
        TutorialStateManager.OnTutorialStateChanged -= MergePawn;
        TutorialStateManager.OnTutorialStateChanged -= SpawnEnemyPawn;
    }

    private void SpawnPawn(TutorialState tutorialState)
    {
        if (tutorialState != TutorialState.ChessPiece)
            return;
        
        StartCoroutine(SpawnPawn());
    }
    
    private IEnumerator SpawnPawn()
    {
        for (int i = 0; i < 2; i++)
        {
            Tile tile = _chessboardManager.GetTileAtPosition(new Vector2Int(0, i));
            Instantiate(_pawnPrefab, tile.transform);
            tile.IsFree = false;
            yield return new WaitForSeconds(.5f);
        }
    }
    
    private void MergePawn(TutorialState tutorialState)
    {
        if (tutorialState != TutorialState.Merge)
            return;
        
        Tile tile = _chessboardManager.GetTileAtPosition(new Vector2Int(0, 2));
        Instantiate(_pawnPrefab, tile.transform);
        tile.IsFree = false;
    }
    
    private void SpawnEnemyPawn(TutorialState tutorialState)
    {
        if (tutorialState != TutorialState.Enemy)
            return;
        
        Tile tile = _chessboardManager.GetTileAtPosition(new Vector2Int(2, 4));
        Instantiate(_enemyPawnPrefab, tile.transform);
        tile.IsFree = false;
        _topPanelDisplay.UpdateTopPanel(_enemyPawnPrefab.GetComponent<ChessPieceData>());
    }    
}
