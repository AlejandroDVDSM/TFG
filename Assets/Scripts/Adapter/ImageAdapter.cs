using UnityEngine;
using UnityEngine.UI;

public class ImageAdapter : MonoBehaviour, ITarget
{
    public void SetSprite(Sprite sprite)
    {
        Image image = GetComponent<Image>(); 
        image.sprite = sprite;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);

    }
}
