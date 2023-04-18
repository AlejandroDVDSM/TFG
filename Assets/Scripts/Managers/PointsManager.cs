using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    private int _totalPoints;
    private PointsDisplay _pointsDisplay;
    private bool _doublePointsFlag;

    private void Start()
    {
        _pointsDisplay = GetComponent<PointsDisplay>();
        _pointsDisplay.UpdateText(_totalPoints);

        GameStateManager.OnGameStateChanged += CheckUserHighestScore;
    }


    private void OnDestroy()
    {
        GameStateManager.OnGameStateChanged -= CheckUserHighestScore;
    }

    public void Add(int pointsToAdd)
    {
        if (_doublePointsFlag)
            _totalPoints += pointsToAdd * 2;
        else
            _totalPoints += pointsToAdd;
        
        _pointsDisplay.UpdateText(_totalPoints);
    }

    public void Subtract(int pointsToSubtract)
    {
        if (_totalPoints - pointsToSubtract < 0) return;
        _totalPoints -= pointsToSubtract;
        _pointsDisplay.UpdateText(_totalPoints);
    }

    public void ActivateDoublePoints()
    {
        _doublePointsFlag = true;
    }

    private void CheckUserHighestScore(GameState gameState)
    {
        if (gameState.Equals(GameState.End))
            StartCoroutine(CheckUserHighestScoreCoroutine());
    }
    
    private IEnumerator CheckUserHighestScoreCoroutine()
    {
        var dbReference = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        var userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        var userData = dbReference.Child("users").Child(userID).Child("highestScore").GetValueAsync();
        
        yield return new WaitUntil(predicate: () => userData.IsCompleted);

        if (userData == null) yield break;
        
        DataSnapshot snapshot = userData.Result;
        
        if ((long)snapshot.Value < _totalPoints)
            FindObjectOfType<FirebaseDatabase>().UpdateHighestScoreInDB(_totalPoints);
    }
}
