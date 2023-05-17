using UnityEngine;

public class TileAppearance : MonoBehaviour
{
    [Header("Sprite")] 
    [SerializeField] private Sprite _whiteTileSprite;
    [SerializeField] private Sprite _brownTileSprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void SetSprite(bool isOffset)
    {
        Storage storage = FindObjectOfType<Storage>();

        if (storage == null)
        {
            Debug.LogWarning("TileAppearance - Couldn't find an object of type FirebaseStorage. Loading appearance with local sprites");
            _spriteRenderer.sprite = isOffset ? _brownTileSprite : _whiteTileSprite;
            return;
        }

        storage.InitializeSprite(isOffset ? "Chessboard/BrownTile.png" : "Chessboard/WhiteTile.png",
            GetComponent<ITarget>());
        
    }
}
