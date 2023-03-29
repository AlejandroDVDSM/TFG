using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using UnityEngine.UI;
using Google;
using TMPro;
using UnityEngine.Networking;

public class FirebaseAuthTest : MonoBehaviour
{
    [Header("Server Info")]
    public string webClientId = "13853559004-qomviaelujbnlm66poei5ufbh0bf7034.apps.googleusercontent.com"; // Este valor se coge desde la consola de Firebase
    private bool firebaseIsReadyToUse = false;

    private FirebaseAuth auth;
    private GoogleSignInConfiguration configuration;
    private string[] scopes = { "https://www.googleapis.com/auth/fitness.activity.read" };

    [Header("UI")]
    [SerializeField] private GameObject loginScreen;
    [SerializeField] private GameObject profileScreen;
    [SerializeField] private Image userProfilePic;
    [SerializeField] private TMP_Text userName, userEmail;
    private string imageUrl;

    public string authCode;
    
    private void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId, 
            RequestEmail = true, 
            RequestIdToken = true,
            RequestAuthCode = true,
            AdditionalScopes = scopes
            
        };
        CheckFirebaseDependencies();
    }

    private void CheckFirebaseDependencies()
    {
        Debug.Log("Trying to check and fix dependencies...");
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result == DependencyStatus.Available)
                {
                    Debug.Log("Dependencies fixed - Status: " + task.Result);
                    
                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                    firebaseIsReadyToUse = true;
                }
                else
                {
                    firebaseIsReadyToUse = false;
                    Debug.LogError("Could not resolve all Firebase dependencies: " + task.Result);
                }
            }
            else
            {
                firebaseIsReadyToUse = false;
                Debug.LogError("Dependency check was not completed. Error : " + task.Exception.Message);
            }
        });
    }

    public void SignInWithGoogle() { OnSignIn(); }
    public void SignOutFromGoogle() { OnSignOut(); }

    private void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestAuthCode = true;
        GoogleSignIn.Configuration.AdditionalScopes = scopes;
        
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished); // Al principio estaba en "ContinueWith"
    }

    private void OnSignOut()
    {
        Debug.Log("Trying to signin out...");
        GoogleSignIn.DefaultInstance.SignOut();
        DisableProfileScreen();
    }

    public void OnDisconnect()
    {
        Debug.Log("Disconnecting...");
        GoogleSignIn.DefaultInstance.Disconnect();
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
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
            authCode = task.Result.AuthCode;
            SignInWithGoogleOnFirebase(task.Result.IdToken);
        }
    }

    private void SignInWithGoogleOnFirebase(string idToken)
    {
        Debug.Log("Trying to sign in with Google on Firebase...");
        if (firebaseIsReadyToUse)
        {
            auth = FirebaseAuth.DefaultInstance;
            Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
            /**
             * IMPORTANTE: La siguiente línea tiene que ser "ContinueWithOnMainThread" y no "ContinueWith" porque si no
             * la aplicación petará al tratar de actualizar la UI con el método "EnableProfileScreen".
             */
            
            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
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

    public void OnSignInSilently()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;

        GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
    }

    public void OnGamesSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;
        
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }
    
    private void EnableProfileScreen(FirebaseUser user)
    {
        loginScreen.SetActive(false);
        profileScreen.SetActive(true);
        
        userName.text = user.DisplayName;
        userEmail.text = user.Email;;
        StartCoroutine(LoadImage(CheckImageUrl(user.PhotoUrl.ToString())));
    }

    private void DisableProfileScreen()
    {
        profileScreen.SetActive(false);
        loginScreen.SetActive(true);
    }
    
    private string CheckImageUrl(string url)
    {
        if (!string.IsNullOrEmpty(url))
        {
            return url;
        }

        return imageUrl;
    }

    IEnumerator LoadImage(string imageUri)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUri);
        yield return www.SendWebRequest();
        
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("WWW error: " + www.error);
        }
        else
        {
            var texture = DownloadHandlerTexture.GetContent(www);
            userProfilePic.sprite = 
                Sprite.Create(texture, new Rect(0,0, texture.width, texture.height), new Vector2(0, 0));
        }
    }
}