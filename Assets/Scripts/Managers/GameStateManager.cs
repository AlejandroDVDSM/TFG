using System;
using UnityEngine;

public enum GameState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    Pause,
    Won,
    Lost
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    
    public GameState gameState = GameState.PlayerTurn; // This must be changed to "Start" when the main menu is implemented

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        instance = this;
    }

    public bool IsPlayerTurn()
    {
        return gameState == GameState.PlayerTurn;
    }

    public bool IsEnemyTurn()
    {
        return gameState == GameState.EnemyTurn;
    }

    public void UpdateGameState(GameState newGameState)
    {
        gameState = newGameState;

        switch (newGameState)
        {
            case GameState.Start:
                // PENDING TO DO
                break;
            case GameState.PlayerTurn:
                // PENDING TO DO
                break;
            case GameState.EnemyTurn:
                // PENDING TO DO
                break;
            case GameState.Pause:
                // PENDING TO DO
                break;
            case GameState.Won:
                // PENDING TO DO
                break;
            case GameState.Lost:
                // PENDING TO DO
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
        }
        
        OnGameStateChanged?.Invoke(newGameState);
    }
}
