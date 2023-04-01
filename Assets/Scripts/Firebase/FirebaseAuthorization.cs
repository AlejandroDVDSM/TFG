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
    
    [Space]
    [Header("Events")]
    [Space]
    public UnityEvent onAuthenticationSuccessful = new UnityEvent();    

    private void Start()
    {
        _firebaseInitializer = GetComponent<FirebaseInitializer>();
        _firebaseDatabase = GetComponent<FirebaseDatabase>();
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
                    onAuthenticationSuccessful.Invoke();
                }
            });
        }
        else
        {
            Debug.Log("Couldn't signing in with Google on Firebase because Firebase is not ready to use");
        }
    }

    // Invoke from "onAuthenticationSuccessful"
    public void CreateUserInDB()
    {
        FirebaseUser currentUser = _auth.CurrentUser;
        _firebaseDatabase.CheckIfUserExists(currentUser.UserId, currentUser.DisplayName);
    }
}
