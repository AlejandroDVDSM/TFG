using System;
using Firebase;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.Events;

public class FirebaseInitializer : MonoBehaviour
{
    private bool _firebaseIsReady;
    
    [Space]
    [Header("Events")]
    [Space]
    public UnityEvent onDependenciesFixed = new UnityEvent();

    private void Awake()
    {
        CheckFirebaseDependencies();
    }

    // RECUERDA QUITAR ESTO
    private void Start()
    {
        if (PlayerPrefs.HasKey("firstPlayedInEpochMillis"))
            PlayerPrefs.DeleteKey("firstPlayedInEpochMillis");
    }

    private void CheckFirebaseDependencies()
    {
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
                    onDependenciesFixed.Invoke();
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
                Debug.LogError("Dependency check was not completed. Error : " + task.Exception.Message);
            }
        });
    }

    public bool IsFirebaseReady()
    {
        return _firebaseIsReady;
    }
}
