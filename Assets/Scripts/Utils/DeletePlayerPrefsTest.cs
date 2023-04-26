using UnityEngine;

public class DeletePlayerPrefsTest : MonoBehaviour
{
    [SerializeField] private string _specificPlayerPrefToRemove;
    
    public void DeletePlayerPrefs()
    { 
        PlayerPrefs.DeleteAll();
        Debug.Log("Delete ALL PlayerPrefs");
    }

    public void DeleteTokensPrefs()
    {
        PlayerPrefs.DeleteKey("accessToken");
        PlayerPrefs.DeleteKey("refreshToken");
        Debug.Log("Delete accessToken and refreshToken PlayerPrefs");
    }

    public void DeleteSpecificPlayerPref()
    {
        PlayerPrefs.DeleteKey(_specificPlayerPrefToRemove);

        if (PlayerPrefs.HasKey(_specificPlayerPrefToRemove))
        {
            PlayerPrefs.DeleteKey(_specificPlayerPrefToRemove);
            Debug.Log($"Delete '{_specificPlayerPrefToRemove}' PlayerPrefs");
        }
        else
        {
            Debug.Log($"Could not delete '{_specificPlayerPrefToRemove}' PlayerPrefs because it doesn't exist");
        }
    }
}
