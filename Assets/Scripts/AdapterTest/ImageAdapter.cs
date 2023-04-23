using UnityEngine;
using UnityEngine.UI;

public class ImageAdapter : MonoBehaviour, ITarget
{
    public void SetSprite(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }
}
