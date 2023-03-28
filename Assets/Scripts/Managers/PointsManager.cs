using UnityEngine;

public class PointsManager : MonoBehaviour
{
    private int _totalPoints;
    private PointsDisplay _pointsDisplay;

    private void Start()
    {
        _pointsDisplay = GetComponent<PointsDisplay>();
        _pointsDisplay.UpdateText(_totalPoints);
    }

    public void Add(int pointsToAdd)
    {
        _totalPoints += pointsToAdd;
        _pointsDisplay.UpdateText(_totalPoints);
    }

    public void Substract(int pointsToSubstract)
    {
        if (_totalPoints - pointsToSubstract < 0) return;
        _totalPoints -= pointsToSubstract;
        _pointsDisplay.UpdateText(_totalPoints);
    }
}
