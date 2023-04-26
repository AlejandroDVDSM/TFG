using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefVersionTEST : MonoBehaviour
{
    [SerializeField] private int _version;
    
    public void SetPlayerPrefVersion()
    {
        PlayerPrefs.SetInt("version", 1);
        Debug.Log($"Set PlayerPref 'version' to '{_version}'");
    }
}
