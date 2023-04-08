using UnityEngine;

public class FirebaseUserData : MonoBehaviour
{
    private string _userName;
    
    private int _highestScore;
    private int _steps;

    private string _firstPlayedInEpochMillis;
    private string _lastPlayedInEpochMillis;

    public void SetFirebaseUserData(string userName, int highestScore, int steps, string firstPlayedInEpochMillis, string lastPlayedInEpochMillis)
    {
        _userName = userName;
        _highestScore = highestScore;
        _steps = steps;
        _firstPlayedInEpochMillis = firstPlayedInEpochMillis;
        _lastPlayedInEpochMillis = lastPlayedInEpochMillis;
    }
}
