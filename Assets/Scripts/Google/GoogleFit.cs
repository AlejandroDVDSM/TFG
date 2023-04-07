using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class GoogleFit : MonoBehaviour
{
    [SerializeField] private TMP_Text stepsText;
    
    [SerializeField] private WebRequestHelper requestHandler;
    [SerializeField] private JSONHelper jsonHelper;

    private void Start()
    {
        FirebaseInitializer firebaseInitializer = FindObjectOfType<FirebaseInitializer>();
        FirebaseAuthorization firebaseAuthorization = FindObjectOfType<FirebaseAuthorization>();
        if (firebaseInitializer.IsFirebaseReady() && firebaseAuthorization.IsUserSignedIn()) CallAPI();
    }

    public void CallAPI()
    {
        GetSteps();
    }

    private void GetSteps()
    {
        const string uri = "https://www.googleapis.com/fitness/v1/users/me/dataset:aggregate";
        
        string startTimeMillis = "" + 1677766440000; // Este dato tiene que ser el momento en que Google Fit empezó el seguimiento de datos del usuario en Epoch milisegundos
        string endTimeMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
        
        
        string json = "{ 'aggregateBy': [{ 'dataTypeName': 'com.google.step_count.delta', 'dataSourceId': 'derived:com.google.step_count.delta:com.google.android.gms:merge_step_deltas'}],'bucketByTime': { 'durationMillis': 86400000 },'startTimeMillis': " + startTimeMillis + ",'endTimeMillis': " + endTimeMillis + "}";
        string accessToken = PlayerPrefs.GetString("accessToken");
        requestHandler.SendPostRequest(uri, accessToken, json, LogSteps); // Se llama al método SendPostRequest y su resultado se envía como parámetro al método LogSteps
    }

    private void LogSteps(string response)
    {
        int bucketChildren = jsonHelper.GetToken(response, "$.bucket").Children().Count();
        int nSteps = 0;
        
        for (int i = 0; i < bucketChildren; i++)
        {
            nSteps += int.Parse(jsonHelper.GetValue(response, $"$.bucket[{i}].dataset[0].point[0].value[0].intVal"));
        }
        
        stepsText.text = nSteps.ToString();
    }
}