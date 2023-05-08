using TMPro;
using UnityEngine;

public class EndGamePointsDisplay : MonoBehaviour
{
    void Start()
    {
        string totalPoints = FindObjectOfType<PointsManager>().TotalPoints.ToString();

        GetComponent<TMP_Text>().text = $"Your points: {totalPoints}";
    }
}
