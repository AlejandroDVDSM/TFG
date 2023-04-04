using System;
using TMPro;
using UnityEngine;

public class GoogleFit : MonoBehaviour
{
    [SerializeField] private TMP_Text stepsText;
    
    [SerializeField] private WebRequestHelper requestHandler;
    [SerializeField] private JSONHelper jsonHelper;
    private string _authCode;

    public string AuthCode { set => _authCode = value; }

    public void CallAPI()
    {
        RetrieveAccessToken();
    }
    
    private void RetrieveAccessToken()
    {
        WWWForm form = new WWWForm();
        form.AddField("code", _authCode);
        form.AddField("client_id", FirebaseSettingsData.ClientId);
        form.AddField("client_secret", FirebaseSettingsData.ClientSecret);
        form.AddField("grant_type", "authorization_code");
        
        requestHandler.SendPostRequest(FirebaseSettingsData.TokenUri, form, GetSteps); // Se llama al método SendPostRequest y su resultado se envía como parámetro al método GetSteps
    }

    private void GetSteps(string response)
    {
        string accessToken = "Bearer " + jsonHelper.GetValue(response, "$.access_token");
        Debug.Log("Bearer " + jsonHelper.GetValue(response, "$.refresh_token"));

        const string uri = "https://www.googleapis.com/fitness/v1/users/me/dataset:aggregate";
        
        string startTimeMillis = "" + 1677766440000; // Este dato tiene que ser el momento en que Google Fit empezó el seguimiento de datos del usuario en Epoch milisegundos
        string endTimeMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
        
        string json = "{ 'aggregateBy': [{ 'dataTypeName': 'com.google.step_count.delta', 'dataSourceId': 'derived:com.google.step_count.delta:com.google.android.gms:merge_step_deltas'}],'bucketByTime': { 'durationMillis': 86400000 },'startTimeMillis': " + startTimeMillis + ",'endTimeMillis': " + endTimeMillis + "}";
        
        requestHandler.SendPostRequest(uri, accessToken, json, LogSteps); // Se llama al método SendPostRequest y su resultado se envía como parámetro al método LogSteps
        
    }

    private void LogSteps(string response)
    {
        string numberOfSteps = jsonHelper.GetValue(response, "$.bucket[0].dataset[0].point[0].value[0].intVal");
        stepsText.text = numberOfSteps;
    }
}