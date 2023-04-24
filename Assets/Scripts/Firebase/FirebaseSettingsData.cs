using UnityEngine;

public class FirebaseSettingsData : MonoBehaviour
{
    private static string _clientId;
    private static string _clientSecret;
    private static string _tokenUri;
    public static string ClientId => _clientId;
    public static string ClientSecret => _clientSecret;
    public static string TokenUri => _tokenUri;
    
    private JSONHelper _jsonHelper;
    private const string _jsonName = "firebase_parameters";
    
    private void Awake()
    {
        _jsonHelper = FindObjectOfType<JSONHelper>();
        if (_jsonHelper == null) Debug.LogError("FirebaseSettingsData - Could not find object of type 'JSONHelper'");

        SetFirebaseParameters();
    }

    private void SetFirebaseParameters()
    {
        _clientId = _jsonHelper.GetValueFromJson(_jsonName, "$.web.client_id");
        _clientSecret = _jsonHelper.GetValueFromJson(_jsonName, "$.web.client_secret");
        _tokenUri = _jsonHelper.GetValueFromJson(_jsonName, "$.web.token_uri");
    }
}
