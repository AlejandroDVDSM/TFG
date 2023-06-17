using UnityEngine;
using UnityEngine.UI;

public class ButtonShopState : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().interactable = GoogleFit.AccountConnectedToGoogleFit;
    } 
}
