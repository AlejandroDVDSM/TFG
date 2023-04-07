using System;
using UnityEngine;

public class TokenRetriever : MonoBehaviour
{
    [SerializeField] private WebRequestHelper requestHandler;
    [SerializeField] private JSONHelper jsonHelper;

    private void Start()
    {
        FirebaseInitializer firebaseInitializer = FindObjectOfType<FirebaseInitializer>();
        FirebaseAuthorization firebaseAuthorization = FindObjectOfType<FirebaseAuthorization>();
        if (firebaseInitializer.IsFirebaseReady() && firebaseAuthorization.IsUserSignedIn()) RetrieveTokens();
    }

    public void RetrieveTokens()
    {
        WWWForm form = new WWWForm();
        form.AddField("code", PlayerPrefs.GetString("authCode")/*authCode*/);
        form.AddField("client_id", FirebaseSettingsData.ClientId);
        form.AddField("client_secret", FirebaseSettingsData.ClientSecret);

        if (PlayerPrefs.HasKey("refreshToken"))
        {
            form.AddField("refresh_token", PlayerPrefs.GetString("refreshToken"));
            form.AddField("grant_type", "refresh_token");
        }
        else
        {
            form.AddField("grant_type", "authorization_code");
        }
        
        requestHandler.SendPostRequest(FirebaseSettingsData.TokenUri, form, SetAccessToken);
    }

    private void SetAccessToken(string response)
    {
        string accessToken = "Bearer " + jsonHelper.GetValue(response, "$.access_token");
        PlayerPrefs.SetString("accessToken", accessToken);
        Debug.Log($"Access token ---> {accessToken}");

        if (!PlayerPrefs.HasKey("refreshToken")) SetRefreshToken(response);
    }

    private void SetRefreshToken(string response)
    {
        string refreshToken = jsonHelper.GetValue(response, "$.refresh_token");
        PlayerPrefs.SetString("refreshToken", refreshToken);
        // PlayerPrefs.SetInt("needToUseRefreshToken", 0);
        Debug.Log($"Refresh token ---> {refreshToken}");    }
}
