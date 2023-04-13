using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoosterCard : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Image _image;
    
    public void LoadData(Booster booster)
    {
        name = booster.BoosterName;
        _name.text = booster.BoosterName;
        // _description.text = _booster.Description;
        _cost.text = booster.Cost.ToString();
        _image.sprite = booster.Sprite;
    }
}
