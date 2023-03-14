using UnityEngine;

public class ChessPieceSpawner : MonoBehaviour
{
    private GameObject _nextChessPiece;

    private bool _needToGenerateNextChessPiece = true;

    private ChessPieceGenerator _chessPieceGenerator;
    
    private Touch _touch;

    private void Start()
    {
        _chessPieceGenerator = GetComponent<ChessPieceGenerator>();
    }

    private void Update()
    {
        if (GameStateManager.gameState != GameState.PlayerTurn) return;
        
        if (_needToGenerateNextChessPiece)
        {
            _nextChessPiece = _chessPieceGenerator.GenerateNextChessPiece();
            _needToGenerateNextChessPiece = false;
        }
        
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(_touch.position);
            
            switch (_touch.phase)
            {
                case TouchPhase.Began:
                    Collider2D hitCollider2D = Physics2D.OverlapPoint(touchPosition);
                    
                    if (hitCollider2D != null && hitCollider2D.CompareTag("Tile"))
                    {
                        var tileWhereToSpawn = hitCollider2D.gameObject.GetComponent<Tile>();
                        SpawnChessPiece(tileWhereToSpawn);
                        _needToGenerateNextChessPiece = true;
                    }
                    
                    break;
            }
        }
    }

    private void SpawnChessPiece(Tile tileWhereToSpawn)
    {
        Debug.Log($"Trying to spawn a chess piece in: <{tileWhereToSpawn.name}>...");
                        
        if (tileWhereToSpawn.IsFree)
        {
            //var _spawnedChessPiece = Instantiate(_chessPiece, tileWhereToSpawn.transform);
            Instantiate(_nextChessPiece, tileWhereToSpawn.transform);
            tileWhereToSpawn.IsFree = false;
            
            Debug.Log($"Spawn successful in <{tileWhereToSpawn.name}>");
        }
        else
        {
            Debug.Log($"<{tileWhereToSpawn.name}> is NOT free");
        }
    }
}
