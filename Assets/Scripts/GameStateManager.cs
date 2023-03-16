using System;
using UnityEngine;

public enum GameState
{
    Start,
    PlayerTurn,
    EnemyTurn,
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
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.EnemyTurn:
                break;
            case GameState.Won:
                break;
            case GameState.Lost:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
        }
        
        OnGameStateChanged?.Invoke(newGameState);
    }
}
