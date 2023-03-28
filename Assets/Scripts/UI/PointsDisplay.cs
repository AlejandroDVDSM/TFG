using TMPro;
using UnityEngine;

public class PointsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text pointsText;
    
    public void UpdateText(int totalPoints)
    {
        pointsText.text = totalPoints.ToString();
    }
}
