using TMPro;
using UnityEngine;

public class StepsDisplay : MonoBehaviour
{
    private TMP_Text _stepsText;
    
    private void Start()
    {
        _stepsText = GetComponent<TMP_Text>();
        GoogleFit.OnStepsUpdated += PrintSteps;
        PrintSteps();
    }

    private void OnDestroy()
    {
        GoogleFit.OnStepsUpdated -= PrintSteps;
    }

    private void PrintSteps()
    {
        _stepsText.text = $"Your steps: {PlayerPrefs.GetString("steps")}";
    }
}
