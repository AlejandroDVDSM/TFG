using System;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Database;

public class FirebaseDatabase : MonoBehaviour
{
    private DatabaseReference _dbReference;

    private string _result = string.Empty;
    
    // Invoke from "FirebaseInitializer.onDependenciesFixed"
    public void SetDatabaseReference()
    {
        _dbReference = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Check if user exists. If not, create an entry in the DB.
    public void CheckIfUserExistsInDB(string userID, string userName)
    {
        _dbReference.Child("users").Child(userID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                    Debug.Log($"CreateUserInDB - User '{userName} --- {userID}': has been created before");
                else
                    CreateUserInDB(userID, userName);
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
        _dbReference.Child("users")
            .Child(FirebaseAuthorization.CurrentUser.UserId)
            .Child("steps")
            .SetValueAsync(steps);
    }

    public void UpdateLastPlayedInEpochMillisInDB(string lastPlayedInEpochMillis)
    {
        _dbReference.Child("users")
            .Child(FirebaseAuthorization.CurrentUser.UserId)
            .Child("lastTimeInEpochMillis")
            .SetValueAsync(lastPlayedInEpochMillis);
    }

    public void GetFirstPlayedInEpochMillis()
    {
        _dbReference.Child("users")
            .Child(/*FirebaseAuthorization.CurrentUser.UserId*/"xUFlS4CG3mWJMrK202k5jB0Onnk2")
            .Child("firstPlayedInEpochMillis")
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    Debug.Log("FirebaseDatabase - GetFirstPlayedInEpochMillis success --" + snapshot.Value);
                    // PlayerPrefs.SetString(snapshot.Value.ToString());
                    
                }
                else
                {
                    Debug.LogError("GetFirstPlayedInEpochMillis - Couldn't be completed");

                }
            });
    }
}
