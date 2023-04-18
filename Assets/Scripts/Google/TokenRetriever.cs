using System;
using Google;
using UnityEngine;

public class TokenRetriever : MonoBehaviour
{
    private WebRequestHelper _webRequestHelper;
    private JSONHelper _jsonHelper;
    private TokenRetriever _instance;
    
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
        _webRequestHelper = FindObjectOfType<WebRequestHelper>();
        _jsonHelper = FindObjectOfType<JSONHelper>();

        FirebaseAuthorization.OnSignInSuccessful += FirebaseAuthorizationOnSignInSuccessful;
    }

    private void OnDestroy()
    {
        FirebaseAuthorization.OnSignInSuccessful -= FirebaseAuthorizationOnSignInSuccessful;
    }

    private void FirebaseAuthorizationOnSignInSuccessful()
    {
        RetrieveTokens();
    }

    private void RetrieveTokens()
    {
        WWWForm form = new WWWForm();
        form.AddField("client_id", FirebaseSettingsData.ClientId);
        form.AddField("client_secret", FirebaseSettingsData.ClientSecret);

        if (PlayerPrefs.HasKey("refreshToken"))
        {
            Debug.Log("Trying to retrieve token with refresh token...");
            form.AddField("refresh_token", PlayerPrefs.GetString("refreshToken"));
            form.AddField("grant_type", "refresh_token");
        }
        else
        {
            Debug.Log("Trying to retrieve token with auth code...");
            form.AddField("code", GoogleSignInService.AuthCode);
            form.AddField("grant_type", "authorization_code");
        }
        
        _webRequestHelper.SendPostRequest(FirebaseSettingsData.TokenUri, form, SetAccessToken);
    }

    private void SetAccessToken(string response)
    {
        string accessToken = "Bearer " + _jsonHelper.GetValue(response, "$.access_token");
        PlayerPrefs.SetString("accessToken", accessToken);
        Debug.Log($"Access token ---> {accessToken}");

        if (!PlayerPrefs.HasKey("refreshToken")) SetRefreshToken(response);
        FindObjectOfType<GoogleFit>().CallFitnessAPI();
    }

    private void SetRefreshToken(string response)
    {
        string refreshToken = _jsonHelper.GetValue(response, "$.refresh_token");
        PlayerPrefs.SetString("refreshToken", refreshToken);
        Debug.Log($"Refresh token ---> {refreshToken}");
    }
}
