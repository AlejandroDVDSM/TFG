using UnityEngine;

public class MainMenuDisplay : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _signInButton;
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _signOutButton;

    public void EnableMainMenu()
    {
        _signInButton.SetActive(false);
        _playButton.SetActive(true);
        _signOutButton.SetActive(true);
    }
    
    public void DisableMainMenu()
    {
        _signInButton.SetActive(true);
        _playButton.SetActive(false);
        _signOutButton.SetActive(false);
    }
}
