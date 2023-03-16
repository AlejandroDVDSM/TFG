using System.Linq;
using UnityEngine;
using Random = System.Random;

public class ChessPieceSpawner : MonoBehaviour
{
    private GameObject _nextChessPiece;

    private ChessPieceGenerator _chessPieceGenerator;

    private void Start()
    {
        _chessPieceGenerator = GetComponent<ChessPieceGenerator>();
        _nextChessPiece = _chessPieceGenerator.GenerateNextChessPiece();
    }

    public void SpawnChessPiece(Tile tileWhereToSpawn)
    {
        Debug.Log($"Trying to spawn a chess piece in: <{tileWhereToSpawn.name}>...");
                        
        if (tileWhereToSpawn.IsFree)
        {
            Instantiate(_nextChessPiece, tileWhereToSpawn.transform);
            tileWhereToSpawn.IsFree = false;
            
            _nextChessPiece = _chessPieceGenerator.GenerateNextChessPiece();
            Debug.Log($"Spawn successful in <{tileWhereToSpawn.name}>");
            GameStateManager.instance.UpdateGameState(GameState.EnemyTurn);
        }
        else
        {
            Debug.Log($"<{tileWhereToSpawn.name}> is NOT free");
        }
    }

    public void SpawnEnemy()
    {
        Tile[] freeTiles = FindObjectsOfType<Tile>().Where(tile => tile.IsFree).ToArray();
        
        int randomIndex = new Random().Next(0, freeTiles.Length); // First parameter: included --- Second parameter: excluded
        var randomTile = freeTiles[randomIndex];

        Instantiate(_chessPieceGenerator.GetRandomEnemy(), randomTile.transform);
        randomTile.IsFree = false;

        GameStateManager.instance.gameState = GameState.PlayerTurn;

    }
}
