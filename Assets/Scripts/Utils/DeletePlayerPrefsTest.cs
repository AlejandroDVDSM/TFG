using UnityEngine;

public class DeletePlayerPrefsTest : MonoBehaviour
{
    public void DeletePlayerPrefs()
    { 
        PlayerPrefs.DeleteAll();
        Debug.Log("Delete player prefs");
        // PlayerPrefs.DeleteKey("authCode");
    }
}
