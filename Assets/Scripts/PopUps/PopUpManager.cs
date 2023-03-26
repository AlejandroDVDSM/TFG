using System;
using System.Linq;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{ 
    [SerializeField] private GameObject[] _popUps;

    public static PopUpManager Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this);
    }

    public void ShowPopUp(string popUpName)
    {
        Transform canvas = FindObjectOfType<Canvas>().transform;
        GameObject popUp = _popUps.First(popUp => popUp.name == popUpName);
        Instantiate(popUp, canvas);
    }
}
