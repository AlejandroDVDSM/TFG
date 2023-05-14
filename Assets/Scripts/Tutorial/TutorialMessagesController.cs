using System;
using UnityEngine;
using UnityEngine.Serialization;

public class TutorialMessagesController : MonoBehaviour
{
    //[SerializeField] private GameObject _tutorialPanel;
    [SerializeField] private TutorialPanelDisplay tutorialPanelDisplay;
    [SerializeField] private GameObject _endTutorialPopUp;

    [SerializeField] private TutorialMessage[] messages;
    private int _currentMessageIndex;

    private void Start()
    {
        tutorialPanelDisplay.SetMessage(messages[0].Message);
        TutorialStateManager.OnTutorialStateChanged += EndTutorial;
    }

    private void OnDestroy()
    {
        TutorialStateManager.OnTutorialStateChanged -= EndTutorial;
    }

    public void Next()
    {
        //_currentMessageIndex++;

        TutorialMessage currentMessage = messages[_currentMessageIndex];
        TutorialMessage nextMessage = messages[_currentMessageIndex + 1];
        
        if (currentMessage.TutorialState != nextMessage.TutorialState)
            TutorialStateManager.Instance.UpdateTutorialState(nextMessage.TutorialState);
        
        tutorialPanelDisplay.SetMessage(nextMessage.Message);
        _currentMessageIndex++;
    }

    public void Prev()
    {
        if (_currentMessageIndex - 1 < 0)
            return;
        
        TutorialMessage currentMessage = messages[_currentMessageIndex];
        TutorialMessage prevMessage = messages[_currentMessageIndex - 1];
        
        if (prevMessage.TutorialState != currentMessage.TutorialState)
            return;
        
        tutorialPanelDisplay.SetMessage(prevMessage.Message);
        _currentMessageIndex--;
    }

    private void EndTutorial(TutorialState tutorialState)
    {
        if (tutorialState != TutorialState.End)
            return;
        
        Destroy(tutorialPanelDisplay.gameObject);
        Transform canvas = FindObjectOfType<Canvas>().transform;
        Instantiate(_endTutorialPopUp, canvas);
    }
}
