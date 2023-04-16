using System;
using TMPro;
using UnityEngine;

public class MainMenuDisplay : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _signedInMenu;
    [SerializeField] private GameObject _signedOutMenu;
    [SerializeField] private TMP_Text _loadingMessage;

    public static MainMenuDisplay Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        ChooseUIToDisplay();
    }

    private void ChooseUIToDisplay()
    {
        if (FirebaseInitializer.IsFirebaseReady())
        {
            if (FirebaseAuthorization.IsUserSignedIn())
                SignedInUI();
            else
                SignedOutUI();
        }
    }
    
    public void SignedInUI()
    {
        _signedInMenu.SetActive(true);
        _signedOutMenu.SetActive(false);
    }
    
    public void SignedOutUI()
    {
        _signedOutMenu.SetActive(true);
        _signedInMenu.SetActive(false);
    }
    
    public void ShowLoadingMessage(string message)
    {
        _loadingMessage.gameObject.SetActive(true);
        _loadingMessage.text = message;
    }

    public void HideLoadingMessage()
    {
        _loadingMessage.text = String.Empty;
        _loadingMessage.gameObject.SetActive(false);
    }
}
