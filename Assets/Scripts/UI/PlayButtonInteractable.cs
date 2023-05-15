using UnityEngine;
using UnityEngine.UI;

public class PlayButtonInteractable : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Button>().interactable = GoogleFit.AccountConnectedToGoogleFit;
    }
}
