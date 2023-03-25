using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{ 
    [SerializeField] private Transform _canvas;
    [Space]
    [SerializeField] private GameObject[] _popUps;

    public void ShowPopUp(string name)
    {
        GameObject popUp = _popUps.First(popUp => popUp.name == name);
        Instantiate(popUp, _canvas);
    }
}
