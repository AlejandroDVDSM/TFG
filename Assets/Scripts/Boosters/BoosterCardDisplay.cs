using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoosterCardDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Image _image;
 
    public void DisplayData(Booster booster)
    {
        _name.text = booster.BoosterName;
        _cost.text = booster.Cost.ToString();
        _image.sprite = booster.Sprite;
    }
}
