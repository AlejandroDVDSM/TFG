using System;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseAuthorization : MonoBehaviour
{
    private FirebaseInitializer _firebaseInitializer;
    private FirebaseAuth _auth;

    [Header("UI")]
    //test
    [SerializeField] private GameObject loginButton;
    [SerializeField] private GameObject profileScreen;
    [SerializeField] private TMP_Text userName, userEmail;
    [SerializeField] private GameObject play, logout;

    private void Start()
    {
        _firebaseInitializer = FindObjectOfType<FirebaseInitializer>();
    }

    public void SignInWithGoogleOnFirebase(string idToken)
    {
        Debug.Log("Trying to sign in with Google on Firebase...");
        if (_firebaseInitializer.IsFirebaseReady())
        {
            _auth = FirebaseAuth.DefaultInstance;
            Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
            
            _auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
            {
                AggregateException ex = task.Exception;
                if (ex != null)
                {
                    if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
                        Debug.LogError("\nSignInWithGoogleOnFirebase: Error code = " + inner.ErrorCode + " Message = " + inner.Message);
                }
                else
                {
                    Debug.Log("Sign in Successful");
                    EnableProfileScreen(task.Result);
                }
            });
        }
        else
        {
            Debug.Log("Couldn't signing in with Google on Firebase because Firebase is not ready to use");
        }
    }
    
    private void EnableProfileScreen(FirebaseUser user)
    {
        loginButton.SetActive(false);
        profileScreen.SetActive(true);
        play.SetActive(true);
        logout.SetActive(true);
        
        userName.text = user.DisplayName;
        userEmail.text = user.Email;;
    }


}
