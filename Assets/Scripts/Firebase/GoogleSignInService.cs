using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Google;
using TMPro;
using UnityEngine.UI;

public class GoogleSignInService : MonoBehaviour
{
    private GoogleSignInConfiguration _configuration;
    
    [Header("UI")]
    //test    
    [SerializeField] private GameObject loginButton;
    [SerializeField] private GameObject profileScreen;
    [SerializeField] private GameObject play, logout;

    private void Start()
    {
        SetGoogleSignInConfiguration();
    }

    private void SetGoogleSignInConfiguration()
    {
        string webClientId = FindObjectOfType<JSONHelper>().GetValueFromJson("google-services", "$.client[0].oauth_client[0].client_id");
        string[] scopes = { "https://www.googleapis.com/auth/fitness.activity.read" };
        
        _configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId, 
            RequestEmail = true, 
            RequestIdToken = true,
            RequestAuthCode = true,
            AdditionalScopes = scopes,
            UseGameSignIn = false // ???
        };
        
        GoogleSignIn.Configuration = _configuration;
    }
    
    //public void SignInWithGoogle() { OnSignIn(); }
    //public void SignOutFromGoogle() { OnSignOut(); }

    /*private void OnSignIn()
    {
        //GoogleSignIn.Configuration = _configuration;
        /*GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestAuthCode = true;
        GoogleSignIn.Configuration.AdditionalScopes = scopes;
        
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticationFinished);
    }*/
    
    public void SignInWithGoogle()
    {
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticationFinished);
    }

    /*private void OnSignOut()
    {
        Debug.Log("Trying to signin out...");
        GoogleSignIn.DefaultInstance.SignOut();
        //DisableProfileScreen();
    }*/

    public void SignOutWithGoogle()
    {
        Debug.Log("Trying to signin out...");
        GoogleSignIn.DefaultInstance.SignOut();
        DisableProfileScreen();
    }

    private void OnGoogleAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        Debug.Log("Trying to authenticate player...");
        if (task.IsFaulted)
        {
            using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    Debug.LogError("Authentication got an error: " + error.Status + " " + error.Message);
                }
                else
                {
                    Debug.LogError("Authentication got an unexpected exception:" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            Debug.Log("Authentication was canceled");
        }
        else
        {
            Debug.Log("Authentication succeeded");
            // string authCode = task.Result.AuthCode;
            FirebaseAuthorization firebaseAuthorization = FindObjectOfType<FirebaseAuthorization>();
            firebaseAuthorization.SignInWithGoogleOnFirebase(task.Result.IdToken);
        }
    }
    
    private void DisableProfileScreen()
    {
        profileScreen.SetActive(false);
        play.SetActive(false);
        logout.SetActive(false);
        loginButton.SetActive(true);
        
    }
}
