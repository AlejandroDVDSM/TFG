using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Sprite _whiteTileSprite, _brownTileSprite;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private bool _isFree = true;
    public bool IsFree { get => _isFree; set => _isFree = value; }

    public void SetSprite(bool isOffset)
    {
        _spriteRenderer.sprite = isOffset ? _brownTileSprite : _whiteTileSprite;
    }
}
