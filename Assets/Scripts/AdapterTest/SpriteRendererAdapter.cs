using UnityEngine;

public class SpriteRendererAdapter : MonoBehaviour, ITarget
{
    public void SetSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
