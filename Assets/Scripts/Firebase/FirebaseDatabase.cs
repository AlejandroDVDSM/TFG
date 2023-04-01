using UnityEngine;
using Firebase.Database;

public class FirebaseDatabase : MonoBehaviour
{
    private DatabaseReference _dbReference;

    // Invoke from "FirebaseInitializer.onFirebaseInitialize"
    public void SetDatabaseReference()
    {
        _dbReference = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Check if user exists. If not, create an entry in the DB.
    public void CheckIfUserExists(string userID, string userName)
    {
        _dbReference.Child("users").Child(userID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    Debug.Log($"CreateUserInDB - User '{userName} --- {userID}': has been created before");
                }
                else
                {
                    CreateUserInDB(userID, userName);
                }
            }
            else
            {
                Debug.LogError("CheckIfUserExists - Couldn't be completed");
            }
        });
    }
    
    private void CreateUserInDB(string userID, string userName)
    {
        User newUser = new User(userID, userName, 0);
        string json = FindObjectOfType<JSONHelper>().CreateJsonFromObject(newUser);
        _dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
        Debug.Log($"CreateUserInDB - User '{userName} --- {userID}' successfully created");
    }
}
