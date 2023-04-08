using System;
using UnityEngine;

public class MainMenuDisplay : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _signedInMenu;
    [SerializeField] private GameObject _signedOutMenu;

    private FirebaseAuthorization _firebaseAuthorization;
    
    private void Start()
    {
        _firebaseAuthorization = FindObjectOfType<FirebaseAuthorization>();
        
        if (_firebaseAuthorization == null)
            Debug.LogError("MainMenuDisplay - Object of type 'FirebaseAuthorization' could not be found");
    }

    // Invoke from "FirebaseInitializer.onDependenciesFixed"
    public void ChooseUIToDisplay()
    {
        if (_firebaseAuthorization == null)
            return;
        
        if (_firebaseAuthorization.IsUserSignedIn())
            SignedInUI();
        else
            SignedOutUI();
    }
    
    private void SignedInUI()
    {
        _signedInMenu.SetActive(true);
    }

    
    private void SignedOutUI()
    {
        _signedOutMenu.SetActive(true);
    }

    // Invoke from "FirebaseAuthorization.onSignInSuccessful" and "FirebaseAuthorization.onSignOutSuccessful" 
    public void ChangeUIDisplayed()
    {
        _signedInMenu.SetActive(!_signedInMenu.activeSelf);
        _signedOutMenu.SetActive(!_signedOutMenu.activeSelf);
    }
}
