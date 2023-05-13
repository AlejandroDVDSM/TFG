using System;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private TutorialMessageDisplay _tutorialMessageDisplay;
    [SerializeField] private GameObject _endTutorialPopUp;

    [SerializeField] private TutorialMessage[] messages;
    private int _currentMessageIndex;


    private void Start()
    {
        _tutorialMessageDisplay.SetMessage(messages[0].Message);
        TutorialStateManager.OnTutorialStateChanged += EndTutorial;
    }

    private void OnDestroy()
    {
        TutorialStateManager.OnTutorialStateChanged -= EndTutorial;
    }

    public void Next()
    {
        /*if (_currentMessageIndex + 1 >= messages.Length)
        {
            EndTutorial();
            return;
        }*/
        
        _currentMessageIndex++;

        TutorialMessage currentMessage = messages[_currentMessageIndex]; 
        TutorialMessage nextMessage = messages[_currentMessageIndex + 1]; 
        
        if (currentMessage.TutorialState != nextMessage.TutorialState)
            TutorialStateManager.Instance.UpdateTutorialState(nextMessage.TutorialState);
        
        string messageToShow = nextMessage.Message;
        _tutorialMessageDisplay.SetMessage(messageToShow);
    }

    /*public void Prev()
    {
        if (_currentMessageIndex - 1 < 0)
            return;
        
        _currentMessageIndex--;
        string messageToShow = messages[_currentMessageIndex].Message;
        _tutorialMessageDisplay.SetMessage(messageToShow);
    }*/

    private void EndTutorial(TutorialState tutorialState)
    {
        if (tutorialState != TutorialState.End)
            return;
        
        Destroy(_tutorialPanel);
        Transform canvas = FindObjectOfType<Canvas>().transform;
        Instantiate(_endTutorialPopUp, canvas);
    }
}
