using System;
using Firebase.Auth;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseDatabase : MonoBehaviour
{
    private DatabaseReference _dbReference;
    private bool _initialized;
    private MainMenuDisplay _mainMenuDisplay;
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
        if (FindObjectOfType<MainMenuDisplay>() != null)
            _mainMenuDisplay = FindObjectOfType<MainMenuDisplay>();
        
        // Initialize FirebaseDatabase
        if (!_initialized)
            InitializedDatabase();
    }

    private void InitializedDatabase()
    {
        // Log messages
        _mainMenuDisplay.ShowLoadingMessage("Initializing Firebase Database...");
        Debug.Log("Initializing Firebase Database...");
        
        // Set references
        _dbReference = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        _initialized = true;
        CheckIfUserExistsInDB();
        
        // UI
        _mainMenuDisplay.HideLoadingMessage();
    }

    // Check if user exists. If not, create an entry in the DB.
    private void CheckIfUserExistsInDB()
    {
        _mainMenuDisplay.ShowLoadingMessage("Checking if user exist in DB...");
        
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
                
                _mainMenuDisplay.HideLoadingMessage();
                _mainMenuDisplay.SignedInUI();
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
        // RetrievePlayerData();
    }
    
    /*public void RetrievePlayerData()
    {
        _dbReference.Child("users").Child(FirebaseAuthorization.CurrentUser.UserId).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                foreach (DataSnapshot s in snapshot.Children)
                {
                    Debug.Log($"SORPRÉNDEME: {s.Key} <-> {s.Value}");
                    PlayerPrefs.SetString(s.Key, s.Value.ToString());
                }
            }
            else
            {
                Debug.LogError("RetrievePlayerData - Couldn't be completed");
            }
        });
    }*/
}
