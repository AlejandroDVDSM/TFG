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
    
    private void Start()
    {
        SetGoogleSignInConfiguration();
    }

    private void SetGoogleSignInConfiguration()
    {
        string webClientId = FindObjectOfType<JSONHelper>().GetValueFromJson("firebase_parameters", "$.web.client_id");
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
        Debug.Log("Signing out...");
        GoogleSignIn.DefaultInstance.SignOut();
    }

    private void OnGoogleAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        Debug.Log("GoogleSignInService - Trying to authenticate player...");
        if (task.IsFaulted)
        {
            using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    Debug.LogError("OnGoogleAuthenticationFinished got an error: " + error.Status + " " + error.Message);
                }
                else
                {
                    Debug.LogError("OnGoogleAuthenticationFinished got an unexpected exception:" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            Debug.Log("GoogleSignInService - Authentication was canceled");
        }
        else
        {
            Debug.Log("GoogleSignInService - Authentication succeeded");
            GetComponent<GoogleFit>().AuthCode = task.Result.AuthCode; 
            FirebaseAuthorization firebaseAuthorization = FindObjectOfType<FirebaseAuthorization>();
            firebaseAuthorization.SignInWithGoogleOnFirebase(task.Result.IdToken);
        }
    }
}
