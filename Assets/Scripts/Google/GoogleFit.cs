using System;
using System.Linq;
using UnityEngine;

public class GoogleFit : MonoBehaviour
{
    private int _steps;
    private bool _initialized;
    
    private WebRequestHelper _webRequestHelper;
    private JSONHelper _jsonHelper;
    private static GoogleFit _instance;

    public static event Action OnStepsUpdated;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _webRequestHelper = FindObjectOfType<WebRequestHelper>();
        _jsonHelper = FindObjectOfType<JSONHelper>();
        
        _steps = PlayerPrefs.HasKey("steps") ? int.Parse(PlayerPrefs.GetString("steps")) : 0;
        OnStepsUpdated?.Invoke();
    }
    
    public void CallFitnessAPI()
    {
        const string uri = "https://www.googleapis.com/fitness/v1/users/me/dataset:aggregate";
        
        string startTimeInEpochMillis = PlayerPrefs.GetString("lastPlayedInEpochMillis");
        string endTimeInEpochMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();

        string json = "{ 'aggregateBy': [{ 'dataTypeName': 'com.google.step_count.delta', 'dataSourceId': 'derived:com.google.step_count.delta:com.google.android.gms:merge_step_deltas'}],'bucketByTime': { 'durationMillis': 86400000 },'startTimeMillis': " + startTimeInEpochMillis + ",'endTimeMillis': " + endTimeInEpochMillis + "}";
        string accessToken = PlayerPrefs.GetString("accessToken");
        
        _webRequestHelper.SendPostRequest(uri, accessToken, json, SetSteps); // Se llama al método SendPostRequest y su resultado se envía como parámetro al método SetSteps
    }

    private void SetSteps(string response)
    {
        int bucketChildren = _jsonHelper.GetToken(response, "$.bucket").Children().Count();
        
        for (int i = 0; i < bucketChildren; i++)
        {
            if (!_jsonHelper.GetToken(response, $"$.bucket[{i}].dataset[0].point").Children().Any())
            {
                break;
            }
            _steps += int.Parse(_jsonHelper.GetValue(response, $"$.bucket[{i}].dataset[0].point[0].value[0].intVal"));
        }
        
        UpdateSteps();
    }

    private void UpdateSteps()
    {
        PlayerPrefs.SetString("steps", _steps.ToString());
        FindObjectOfType<FirebaseDatabase>().UpdateStepsInDB(_steps);
        OnStepsUpdated?.Invoke();
    }
    
    public void SubtractSteps(int steps)
    {
        _steps -= steps;
        UpdateSteps();
    }
}

