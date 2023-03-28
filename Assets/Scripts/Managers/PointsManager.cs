using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    private int _totalPoints;

    [SerializeField] private TMP_Text pointsText;

    private void Start()
    {
        UpdateText();
    }

    public void Add(int pointsToAdd)
    {
        _totalPoints += pointsToAdd;
        UpdateText();
    }

    public void Substract(int pointsToSubstract)
    {
        if (_totalPoints - pointsToSubstract < 0) return;
        _totalPoints -= pointsToSubstract;
        UpdateText();
    }

    private void UpdateText()
    {
        pointsText.text = _totalPoints.ToString();
    }
}
