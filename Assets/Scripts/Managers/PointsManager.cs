using System.Collections;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    private int _totalPoints = 0;
    private PointsDisplay _pointsDisplay;
    private bool _doublePointsFlag;

    public int TotalPoints => _totalPoints;

    private void Start()
    {
        _pointsDisplay = GetComponent<PointsDisplay>();
        _pointsDisplay.UpdateText(/*TotalPoints*/);

        GameStateManager.OnGameStateChanged += CheckUserHighestScore;
    }


    private void OnDestroy()
    {
        GameStateManager.OnGameStateChanged -= CheckUserHighestScore;
    }

    public void Add(int pointsToAdd)
    {
        if (_doublePointsFlag)
            _totalPoints = TotalPoints + pointsToAdd * 2;
        else
            _totalPoints = TotalPoints + pointsToAdd;
        
        _pointsDisplay.UpdateText(/*TotalPoints*/);
    }

    public void Subtract(int pointsToSubtract)
    {
        if (TotalPoints - pointsToSubtract < 0) return;
        _totalPoints = TotalPoints - pointsToSubtract;
        _pointsDisplay.UpdateText(/*TotalPoints*/);
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
        
        if ((long)snapshot.Value < TotalPoints)
            FindObjectOfType<FirebaseDatabase>().UpdateHighestScoreInDB(TotalPoints);
    }
}
