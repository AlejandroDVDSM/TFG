using TMPro;
using UnityEngine;

public class PointsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text pointsText;
    
    public void UpdateText(int _totalPoints)
    {
        pointsText.text = _totalPoints.ToString();
    }
    
}
