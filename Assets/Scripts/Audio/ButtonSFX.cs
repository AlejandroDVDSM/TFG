using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    public void ClickSFX()
    {
        AudioManager.Instance.Play("Click");
    }
}
