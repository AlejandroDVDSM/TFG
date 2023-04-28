using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public void Pause(string popUp)
    {
        PopUpManager.Instance.ShowPopUp(popUp);
        GameStateManager.Instance.UpdateGameState(GameState.Pause);
    }

    public void Resume()
    {
        GameStateManager.Instance.UpdateGameState(GameState.PlayerTurn);
    }
}
