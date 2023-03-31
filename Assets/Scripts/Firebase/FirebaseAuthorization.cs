using System;
using Firebase;
using Firebase.Auth;
using UnityEngine;

public class FirebaseAuthorization : MonoBehaviour
{
    private FirebaseInitializer _firebaseInitializer;
    private FirebaseAuth _auth;

    private void Start()
    {
        _firebaseInitializer = GetComponent<FirebaseInitializer>();
    }

    public void SignInWithGoogleOnFirebase(string idToken)
    {
        Debug.Log("Trying to sign in with Google on Firebase...");
        if (_firebaseInitializer.IsFirebaseReady())
        {
            _auth = FirebaseAuth.DefaultInstance;
            Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
            
            _auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
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
                }
            });
        }
        else
        {
            Debug.Log("Couldn't signing in with Google on Firebase because Firebase is not ready to use");
        }
    }
}
