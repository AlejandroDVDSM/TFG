using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public static event Action OnSceneLoaded;
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        OnSceneLoaded?.Invoke();
    }
}
