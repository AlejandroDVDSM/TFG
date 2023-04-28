using System;
using UnityEngine;

public enum GameState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    Pause,
    End,
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    
    public GameState gameState /*= GameState.Start/*PlayerTurn*/; // This must be changed to "Start" when the 

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.Start);
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
                AudioManager.Instance.Stop("MainMenuTheme");
                AudioManager.Instance.Play("GameTheme");
                UpdateGameState(GameState.PlayerTurn);
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
            case GameState.End:
                // PENDING TO DO
                // Stop GameTheme (maybe play "EndGameTheme (?))"
                PopUpManager.Instance.ShowPopUp("EndGamePopUp");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
        }
        
        OnGameStateChanged?.Invoke(newGameState);
    }
}
