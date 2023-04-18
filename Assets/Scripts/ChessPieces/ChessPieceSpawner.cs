using UnityEngine;

public class ChessPieceSpawner : MonoBehaviour
{
    [SerializeField] private ChessboardManager _chessboardManager; 
    private ChessPieceGenerator _chessPieceGenerator;
    private GameObject _nextChessPiece;

    [SerializeField] private bool ignoreEnemySpawn = false; // FLAG

    private void Start()
    {
        _chessPieceGenerator = GetComponent<ChessPieceGenerator>();
        SetNextRandomChessPiece();
        
        //_nextChessPiece = _chessPieceGenerator.GenerateNextChessPiece();
    }

    public void SpawnChessPiece(Tile tileWhereToSpawn)
    {
        Debug.Log($"Trying to spawn a chess piece in: <{tileWhereToSpawn.name}>...");
                        
        if (tileWhereToSpawn.IsFree)
        {
            Instantiate(_nextChessPiece, tileWhereToSpawn.transform);
            tileWhereToSpawn.IsFree = false;
            
            if (_chessboardManager.AreAllTilesOccupy())
                GameStateManager.Instance.UpdateGameState(GameState.End);
            else
                SetNextRandomChessPiece();
            
            Debug.Log($"Spawn successful in <{tileWhereToSpawn.name}>");
            //GameStateManager.instance.UpdateGameState(GameState.EnemyTurn);
        }
        else
        {
            Debug.Log($"<{tileWhereToSpawn.name}> is NOT free");
        }
    }

    public void SetNextRandomChessPiece()
    {
        _nextChessPiece = _chessPieceGenerator.GenerateNextChessPiece();
    }

    /*public void SpawnEnemy()
    {
        if (ignoreEnemySpawn) // TEST PURPOSES
        {
            Debug.Log("Flag <ignoreEnemySpawn> is TRUE...");
            GameStateManager.instance.UpdateGameState(GameState.PlayerTurn);
            return;
        }
        
        Tile[] freeTiles = FindObjectsOfType<Tile>().Where(tile => tile.IsFree).ToArray();
        
        int randomIndex = new Random().Next(0, freeTiles.Length); // First parameter: included --- Second parameter: excluded
        var randomTile = freeTiles[randomIndex];

        Instantiate(_chessPieceGenerator.GetRandomEnemy(), randomTile.transform);
        randomTile.IsFree = false;
        
        GameStateManager.instance.UpdateGameState(GameState.PlayerTurn);
    }*/
}
