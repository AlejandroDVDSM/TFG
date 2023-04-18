using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using TMPro;
using UnityEngine;

public class LeaderboardDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboardRowPrefab;
    private DatabaseReference _dbReference;

    private void Start()
    {
        _dbReference = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;
        StartCoroutine(DisplayLeaderboardCoroutine());
    }

    private IEnumerator DisplayLeaderboardCoroutine()
    {
        var userData = _dbReference.Child("users").OrderByChild("highestScore").GetValueAsync();
        yield return new WaitUntil(predicate: () => userData.IsCompleted);

        if (userData == null) yield break;
        
        DataSnapshot snapshot = userData.Result;
        List<DataSnapshot> snapshotsList = new List<DataSnapshot>(snapshot.Children);

        var rank = 1;
        for (int i = (int)snapshot.ChildrenCount - 1; i >= 0; i--)
        {
            var leaderboardInstantiated = Instantiate(_leaderboardRowPrefab, transform);
            leaderboardInstantiated.transform.GetChild(0).GetComponent<TMP_Text>().text = rank.ToString();
            leaderboardInstantiated.transform.GetChild(1).GetComponent<TMP_Text>().text = ((Dictionary<string, object>)snapshotsList[i].Value)["userName"].ToString();
            leaderboardInstantiated.transform.GetChild(2).GetComponent<TMP_Text>().text = ((Dictionary<string, object>)snapshotsList[i].Value)["highestScore"].ToString();
            
            rank++;
        }
    }
}
