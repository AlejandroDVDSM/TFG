using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Extensions;
using UnityEngine;
using Google;
using UnityEngine.Events;

public class GoogleSignInService : MonoBehaviour
{
    private GoogleSignInConfiguration _configuration;

    [Space]
    [Header("Events")]
    [Space]
    public UnityEvent onSignOutSuccessful = new UnityEvent();
    
    private void Start()
    {
        SetGoogleSignInConfiguration();
    }

    private void SetGoogleSignInConfiguration()
    {
        string webClientId = FindObjectOfType<JSONHelper>().GetValueFromJson("google-services", "$.client[0].oauth_client[1].client_id");
        string[] scopes = { "https://www.googleapis.com/auth/fitness.activity.read" };
        
        _configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId, 
            RequestEmail = true, 
            RequestIdToken = true,
            RequestAuthCode = true,
            AdditionalScopes = scopes,
            UseGameSignIn = false
        };
        
        GoogleSignIn.Configuration = _configuration;
    }
    
    public void SignInWithGoogle()
    {
        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(OnGoogleAuthenticationFinished);
    }

    public void SignOutWithGoogle()
    {
        Debug.Log("Trying to signin out...");
        GoogleSignIn.DefaultInstance.SignOut();
        onSignOutSuccessful.Invoke();
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
            FirebaseAuthorization firebaseAuthorization = GetComponent<FirebaseAuthorization>();
            firebaseAuthorization.SignInWithGoogleOnFirebase(task.Result.IdToken);
        }
    }
}
