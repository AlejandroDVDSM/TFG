using System;
using Firebase;
using Firebase.Auth;
using UnityEngine;

public class FirebaseAuthorization : MonoBehaviour
{
    private FirebaseInitializer _firebaseInitializer;
    private FirebaseAuth _auth;
    private FirebaseAuthorization _instance;
    private FirebaseDatabase _firebaseDatabase;
    private bool _initialized;
    //private MainMenuDisplay _mainMenuDisplay;
    
    public static event Action onSignInSuccessful;
    public static event Action onSignOutSuccessful;

    private void Awake()
    {
        // Singleton
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        /*if (FindObjectOfType<MainMenuDisplay>() != null)
            _mainMenuDisplay = FindObjectOfType<MainMenuDisplay>();*/
        
        // Initialize FirebaseAuth
        if (!_initialized)
            InitializedFirebaseAuthorization();
    }

    private void InitializedFirebaseAuthorization()
    {
        // Log messages
        //_mainMenuDisplay.ShowLoadingMessage("Initializing Firebase Authorization...");
        MainMenuDisplay.Instance.ShowLoadingMessage("Initializing Firebase Authorization...");
        Debug.Log("Initializing Firebase Authorization...");
        
        // Set references
        _auth = FirebaseAuth.DefaultInstance;
        _initialized = true;
        
        // UI
       //_mainMenuDisplay.HideLoadingMessage();
       MainMenuDisplay.Instance.HideLoadingMessage();
       // _mainMenuDisplay.SignedOutUI();
       MainMenuDisplay.Instance.SignedOutUI();
    }

    public void SignInWithGoogleOnFirebase(string idToken)
    {
       // _mainMenuDisplay.ShowLoadingMessage("Signing in, please wait...");
        MainMenuDisplay.Instance.ShowLoadingMessage("Signing in, please wait...");
        Debug.Log("SignInWithGoogleOnFirebase - Trying to sign in with Google on Firebase...");
        
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
        _auth.SignInWithCredentialAsync(credential).ContinueWith(task => //OnMainThread
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
                onSignInSuccessful?.Invoke();
            }
        });
    }

    public void SignOutAuthenticatedUser()
    {
        // Actions before signing out...
        FirebaseDatabase firebaseDatabase = FindObjectOfType<FirebaseDatabase>();
        firebaseDatabase.UpdateStepsInDB(int.Parse(PlayerPrefs.GetString("steps")));
        firebaseDatabase.UpdateLastPlayedInEpochMillis();
        
        // Sign out
        FirebaseAuth.DefaultInstance.SignOut();
        GetComponent<GoogleSignInService>().SignOutWithGoogle();
        
        // UI
        onSignOutSuccessful?.Invoke();
    }

    public static bool IsUserSignedIn()
    {
        return FirebaseAuth.DefaultInstance.CurrentUser != null;
    }
}
