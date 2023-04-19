using UnityEngine;

public class DeletePlayerPrefsTest : MonoBehaviour
{
    public void DeletePlayerPrefs()
    { 
        PlayerPrefs.DeleteAll();
        Debug.Log("Delete ALL PlayerPrefs");
        // PlayerPrefs.DeleteKey("authCode");
    }

    public void DeleteTokensPrefs()
    {
        PlayerPrefs.DeleteKey("accessToken");
        PlayerPrefs.DeleteKey("refreshToken");
        Debug.Log("Delete accessToken and refreshToken from PlayerPrefs");
    }
}
