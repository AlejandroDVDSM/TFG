using UnityEngine;

public class SpriteRendererAdapter : MonoBehaviour, ITarget
{
    public void SetSprite(Sprite sprite)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        
        var color = spriteRenderer.color;
        color = new Color(color.r, color.g, color.b, 1);
        spriteRenderer.color = color;
    }
}
