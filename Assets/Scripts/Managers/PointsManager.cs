using UnityEngine;
using UnityEngine.Serialization;

public class PointsManager : MonoBehaviour
{
    private int _totalPoints;
    private PointsDisplay _pointsDisplay;
    private bool _doublePointsFlag;

    private void Start()
    {
        _pointsDisplay = GetComponent<PointsDisplay>();
        _pointsDisplay.UpdateText(_totalPoints);
    }

    public void Add(int pointsToAdd)
    {
        if (_doublePointsFlag)
            _totalPoints += pointsToAdd * 2;
        else
            _totalPoints += pointsToAdd;
        
        _pointsDisplay.UpdateText(_totalPoints);
    }

    public void Subtract(int pointsToSubtract)
    {
        if (_totalPoints - pointsToSubtract < 0) return;
        _totalPoints -= pointsToSubtract;
        _pointsDisplay.UpdateText(_totalPoints);
    }

    public void ActivateDoublePoints()
    {
        _doublePointsFlag = true;
    }
}
