using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Sprite _whiteTileSprite, _brownTileSprite;

    [SerializeField] private Image _image;

    public void SetSprite(bool isOffset)
    {
        _image.sprite = isOffset ? _brownTileSprite : _whiteTileSprite;
    }
}
