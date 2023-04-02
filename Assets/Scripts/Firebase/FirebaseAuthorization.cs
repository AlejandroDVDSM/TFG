using System;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.Events;

public class FirebaseAuthorization : MonoBehaviour
{
    private FirebaseInitializer _firebaseInitializer;
    private FirebaseAuth _auth;
    private FirebaseDatabase _firebaseDatabase;

    public FirebaseAuthorization instance;
    
    [Space]
    [Header("Events")]
    [Space]
    public UnityEvent onSignInSuccessful = new UnityEvent();    
    public UnityEvent onSignOutSuccessful = new UnityEvent();    

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);
    }
    
    private void Start()
    {
        _firebaseInitializer = GetComponent<FirebaseInitializer>();
        _firebaseDatabase = GetComponent<FirebaseDatabase>();
    }
    
    // Invoke from "FirebaseInitializer.onFirebaseInitialize"
    public void SetFirebaseAuthReference()
    {
        _auth = FirebaseAuth.DefaultInstance;
        Debug.Log(FirebaseAuth.DefaultInstance.CurrentUser != null ? $"Current user: {FirebaseAuth.DefaultInstance.CurrentUser.DisplayName}" : "Current user: null");
    }

    public void SignInWithGoogleOnFirebase(string idToken)
    {
        Debug.Log("SignInWithGoogleOnFirebase - Trying to sign in with Google on Firebase...");
        if (_firebaseInitializer.IsFirebaseReady())
        {
            // _auth = FirebaseAuth.DefaultInstance;
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
                    Debug.Log("SignInWithGoogleOnFirebase - Sign in Successful");
                    onSignInSuccessful.Invoke();
                }
            });
        }
        else
        {
            Debug.Log("SignInWithGoogleOnFirebase - Firebase is not ready to use yet");
        }
    }

    public void SignOutAuthenticatedUser()
    {
        _auth.SignOut();
        GetComponent<GoogleSignInService>().SignOutWithGoogle();
        onSignOutSuccessful.Invoke();
    }

    public bool IsUserSignedIn()
    {
        return _auth.CurrentUser != null;
    }

    // Invoke from "onSignInSuccessful"
    public void CheckIfUserExistsInDB()
    {
        FirebaseUser currentUser = _auth.CurrentUser;
        _firebaseDatabase.CheckIfUserExistsInDB(currentUser.UserId, currentUser.DisplayName);
    }
}
