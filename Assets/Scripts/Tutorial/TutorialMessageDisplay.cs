using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMessageDisplay : MonoBehaviour
{
    private TMP_Text _message;

    private void Start()
    {
        _message = GetComponent<TMP_Text>();
    }

    public void SetMessage(string newMessage)
    {
        _message.text = newMessage;
    }
}
