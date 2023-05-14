using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanelDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _message;

    private void Start()
    {
        TutorialStateManager.OnTutorialStateChanged += UpdateTitle;
    }

    private void OnDestroy()
    {
        TutorialStateManager.OnTutorialStateChanged -= UpdateTitle;
    }

    private void UpdateTitle(TutorialState tutorialState)
    {
        _title.text = tutorialState.ToString();
    }

    public void SetMessage(string newMessage)
    {
        _message.text = newMessage;
    }
}
