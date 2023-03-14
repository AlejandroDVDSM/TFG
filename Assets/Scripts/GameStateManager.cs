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
    public static GameState gameState = GameState.PlayerTurn; // This must be changed to "Start" when the main menu is implemented

    public bool IsPlayerTurn()
    {
        return gameState == GameState.PlayerTurn;
    }

    public bool IsEnemyTurn()
    {
        return gameState == GameState.EnemyTurn;
    }
}
