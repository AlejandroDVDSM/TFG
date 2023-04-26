using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TopPanelDisplay : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _nextChessPieceType;
    [SerializeField] private Image _nextChessPieceImage;
    
    public void UpdateTopPanel(ChessPieceData nextChessPiece)
    {
        _nextChessPieceType.text = nextChessPiece.GetChessPieceType().ToString();

        string path = $"chesspieces/{nextChessPiece.GetTeam()}/{nextChessPiece.GetChessPieceType()}.png";
        ITarget target = _nextChessPieceImage.GetComponent<ITarget>();
        FindObjectOfType<Storage>().TestDownloadLoad(path, target);
    }
}
