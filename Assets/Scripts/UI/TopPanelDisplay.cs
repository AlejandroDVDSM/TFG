using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TopPanelDisplay : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _nextChessPieceType;
    [SerializeField] private Image _nextChessPieceImage;
    
    public void UpdateTopPanel(Type nextChessPieceType, Sprite nextChessPieceImage)
    {
        _nextChessPieceType.text = nextChessPieceType.ToString();
        _nextChessPieceImage.sprite = nextChessPieceImage;
    }
}
