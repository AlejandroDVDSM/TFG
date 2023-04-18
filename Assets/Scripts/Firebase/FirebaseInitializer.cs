using Firebase;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseInitializer : MonoBehaviour
{
    private static bool _firebaseIsReady;
    private static FirebaseInitializer _instance;
    
    [SerializeField] private GameObject _signInManagerPrefab;
    [SerializeField] private GameObject _firebaseDatabasePrefab;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        if (!_firebaseIsReady)
            CheckFirebaseDependencies();
    }
                                                                                       
    private void OnDestroy()
    {
        FirebaseAuthorization.OnSignInSuccessful -= FirebaseAuthorizationOnSignInSuccessful;
        FirebaseAuthorization.OnSignOutSuccessful -= FirebaseAuthorizationOnSignOutSuccessful;
    }

    private void CheckFirebaseDependencies()
    {
        MainMenuDisplay.Instance.ShowLoadingMessage("Fixing dependencies...");
        Debug.Log("Trying to check and fix dependencies...");
        
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result == DependencyStatus.Available)
                {
                    Debug.Log("Dependencies fixed - Status: " + task.Result);
                    
                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                    _firebaseIsReady = true;
                    MainMenuDisplay.Instance.HideLoadingMessage();
                    Instantiate(_signInManagerPrefab, transform);
                    FirebaseAuthorization.OnSignInSuccessful += FirebaseAuthorizationOnSignInSuccessful;
                    FirebaseAuthorization.OnSignOutSuccessful += FirebaseAuthorizationOnSignOutSuccessful;
                }
                else
                {
                    _firebaseIsReady = false;
                    Debug.LogError("Could not resolve all Firebase dependencies: " + task.Result);
                }
            }
            else
            {
                _firebaseIsReady = false;
                Debug.LogError("Dependency check was not completed. Error : " + task.Exception?.Message);
            }
        });
    }

    public static bool IsFirebaseReady()
    {
        return _firebaseIsReady;
    }
    
    private void FirebaseAuthorizationOnSignInSuccessful()
    {
        Instantiate(_firebaseDatabasePrefab, transform);
        MainMenuDisplay.Instance.HideLoadingMessage();
    }

    private void FirebaseAuthorizationOnSignOutSuccessful()
    {
        MainMenuDisplay.Instance.ShowLoadingMessage("Signing out...");
        MainMenuDisplay.Instance.SignedOutUI();
        MainMenuDisplay.Instance.HideLoadingMessage();
    }
}
