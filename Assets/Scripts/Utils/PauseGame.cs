using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    //private PopUpManager _popUpManager;
    
    /*private void Start()
    {
        _popUpManager = FindObjectOfType<PopUpManager>();
    }*/

    public void Pause(string popUp)
    {
        PopUpManager.Instance.ShowPopUp(popUp/*"PausePopUp"*/);
        //_popUpManager.ShowPopUp("PausePopUp");
        GameStateManager.Instance.UpdateGameState(GameState.Pause);
    }

    public void Resume()
    {
        GameStateManager.Instance.UpdateGameState(GameState.PlayerTurn);
    }
}
