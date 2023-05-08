using TMPro;
using UnityEngine;

public class PointsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text pointsText;
    private PointsManager _pointsManager;
    
    private void Awake()
    {
        _pointsManager = FindObjectOfType<PointsManager>();
        
        if (_pointsManager == null)
            Debug.LogError("Could not find an object of type 'PointsManager'");
    }

    public void UpdateText(/*int totalPoints*/)
    {
        pointsText.text = _pointsManager.TotalPoints.ToString()/*totalPoints.ToString()*/;
    }
}
