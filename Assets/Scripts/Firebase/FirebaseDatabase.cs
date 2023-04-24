using System;
using Firebase.Auth;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseDatabase : MonoBehaviour
{
    private DatabaseReference _dbReference;
    private bool _initialized;
    private FirebaseDatabase _instance;

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
        // Initialize FirebaseDatabase
        if (!_initialized)
            FirebaseInitializer.OnDependenciesFixed += InitializeDatabase;
            //InitializeDatabase();
    }

    private void OnDestroy()
    {
        FirebaseInitializer.OnDependenciesFixed -= InitializeDatabase;
        FirebaseAuthorization.OnSignInSuccessful -= CheckIfUserExistsInDB;
    }

    private void OnApplicationQuit()
    {
        UpdateLastPlayedInEpochMillis();
    }

    private void InitializeDatabase()
    {
        // Log messages
        MainMenuDisplay.Instance.ShowLoadingMessage("Initializing Firebase Database...");
        Debug.Log("Initializing Firebase Database...");
        
        // Set references
        _dbReference = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseAuthorization.OnSignInSuccessful += CheckIfUserExistsInDB;
        _initialized = true;
        //CheckIfUserExistsInDB();
        
        // UI
        MainMenuDisplay.Instance.HideLoadingMessage();
    }

    // Check if user exists. If not, create an entry in the DB.
    private void CheckIfUserExistsInDB()
    {
        MainMenuDisplay.Instance.ShowLoadingMessage("Checking if user exist in DB...");
        
        string userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        string userName = FirebaseAuth.DefaultInstance.CurrentUser.DisplayName;
        
        _dbReference.Child("users").Child(userID).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                    Debug.Log($"CreateUserInDB - User '{userName} --- {userID}': has been created before");
                else
                    CreateUserInDB(userID, userName);
                
                SetPlayerPrefs();
                MainMenuDisplay.Instance.HideLoadingMessage();
                MainMenuDisplay.Instance.SignedInUI();
            }
            else
            {
                Debug.LogError("CheckIfUserExists - Couldn't be completed");
            }
        });
    }
    
    private void CreateUserInDB(string userID, string userName)
    {
        string timeInEpochMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
        
        User newUser = new User(userName, 0, 0, timeInEpochMillis, timeInEpochMillis);
        string json = FindObjectOfType<JSONHelper>().CreateJsonFromObject(newUser);
        _dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
        
        Debug.Log($"CreateUserInDB - User '{userName} --- {userID}' successfully created");
    }

    public void UpdateStepsInDB(int steps)
    {
        Debug.Log("Updating 'steps'...");
        string userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        _dbReference.Child("users").Child(userID).Child("steps").SetValueAsync(steps);
    }
    
    public void UpdateHighestScoreInDB(int newHighestScore)
    {
        Debug.Log("Updating 'highestScore'...");
        string userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        _dbReference.Child("users").Child(userID).Child("highestScore").SetValueAsync(newHighestScore);
    }
    
    public void UpdateLastPlayedInEpochMillis()
    {
        Debug.Log("Updating 'lastPlayedInEpochMillis'...");
        string userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        string lastPlayedInEpochMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
        _dbReference.Child("users").Child(userID).Child("lastPlayedInEpochMillis").SetValueAsync(lastPlayedInEpochMillis);
    }    
    
    private void SetPlayerPrefs()
    {
        Debug.Log("Setting PlayerPrefs with the data found in DB...");
        string userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        
        _dbReference.Child("users").Child(userID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                foreach (DataSnapshot s in snapshot.Children)
                {
                    Debug.Log($"SetPlayerPrefs: {s.Key} <-> {s.Value}");
                    PlayerPrefs.SetString(s.Key, s.Value.ToString());
                }
            }
            else
            {
                Debug.LogError("RetrievePlayerData - Couldn't be completed");
            }
        });
    }
}
