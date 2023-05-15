using System;
using UnityEngine;

public enum TutorialState
{
    Start,
    ChessPiece,
    Merge,
    Enemy,
    ButtonShop,
    Shop,
    Movement,
    Eat,
    End
}

public class TutorialStateManager : MonoBehaviour
{
    public static TutorialStateManager Instance;

    public TutorialState tutorialState; 

    public static event Action<TutorialState> OnTutorialStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AudioManager.Instance.StopAllThemes();
        AudioManager.Instance.Play("TutorialTheme");
    }

    public void UpdateTutorialState(TutorialState newTutorialState)
    {
        Debug.Log($"Tutorial state updated from '{tutorialState}' to '{newTutorialState}'");
        tutorialState = newTutorialState;
        
        OnTutorialStateChanged?.Invoke(newTutorialState);
    }
}
