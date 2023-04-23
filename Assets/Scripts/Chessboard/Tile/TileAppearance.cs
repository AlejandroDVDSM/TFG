using UnityEngine;

public class TileAppearance : MonoBehaviour
{
    [Header("Sprite")] 
    [SerializeField] private Sprite _whiteTileSprite;
    [SerializeField] private Sprite _brownTileSprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void SetSprite(bool isOffset)
    {
        //_spriteRenderer.sprite = isOffset ? _brownTileSprite : _whiteTileSprite;
        FirebaseStorageTest firebaseStorageTest = FindObjectOfType<FirebaseStorageTest>();

        if (firebaseStorageTest == null)
        {
            Debug.LogError("TileAppearance - Couldn't find an object of type FirebaseStorage");
            return;
        }

        firebaseStorageTest.GetImage(isOffset ? "Chessboard/BrownTile.png" : "Chessboard/WhiteTile.png",
            GetComponent<ITarget>());
    }
}
