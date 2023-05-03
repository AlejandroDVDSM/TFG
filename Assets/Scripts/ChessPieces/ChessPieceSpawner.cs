using UnityEngine;

[RequireComponent(typeof(ChessPieceGenerator))]
public class ChessPieceSpawner : MonoBehaviour
{
    [SerializeField] private ChessboardManager _chessboardManager; 
    private ChessPieceGenerator _chessPieceGenerator;
    private GameObject _nextChessPiece;

    private void Start()
    {
        _chessPieceGenerator = GetComponent<ChessPieceGenerator>();
        SetNextRandomChessPiece();
    }

    public void SpawnChessPiece(Tile tileWhereToSpawn)
    {
        Debug.Log($"Trying to spawn a chess piece in: <{tileWhereToSpawn.name}>...");
                        
        if (tileWhereToSpawn.IsFree)
        {
            Instantiate(_nextChessPiece, tileWhereToSpawn.transform);
            AudioManager.Instance.Play("ChesspieceMove");
            tileWhereToSpawn.IsFree = false;
            
            if (_chessboardManager.AreAllTilesOccupy())
                GameStateManager.Instance.UpdateGameState(GameState.End);
            else
                SetNextRandomChessPiece();
            
            Debug.Log($"Spawn successful in <{tileWhereToSpawn.name}>");
        }
        else
        {
            AudioManager.Instance.Play("Error");
            Debug.Log($"<{tileWhereToSpawn.name}> is NOT free");
        }
    }

    public void SetNextRandomChessPiece()
    {
        _nextChessPiece = _chessPieceGenerator.GenerateNextChessPiece();
    }

    public void SetNextChessPiece(ChessPieceData nextChessPiece)
    {
        _nextChessPiece = nextChessPiece.gameObject;
        FindObjectOfType<TopPanelDisplay>().UpdateTopPanel(nextChessPiece);
    }
}
