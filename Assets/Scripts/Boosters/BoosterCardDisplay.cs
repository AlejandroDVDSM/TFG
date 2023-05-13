using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoosterCardDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Image _image;
 
    public void DisplayData(Booster booster)
    {
        // Texts
        _name.text = booster.BoosterName;
        _cost.text = booster.Cost.ToString();
        ChangeCostTextColor(booster.Cost);
        
        // Image
        string boosterName = booster.BoosterName.Replace(" ", "");
        string path = $"Boosters/{boosterName}.png";
        FindObjectOfType<Storage>().InitializeSprite(path,_image.GetComponent<ITarget>());
    }

    private void ChangeCostTextColor(int cost)
    {
        int steps = int.Parse(PlayerPrefs.GetString("steps"));
        _cost.color = steps < cost ? Color.red : Color.white;
    }
}
