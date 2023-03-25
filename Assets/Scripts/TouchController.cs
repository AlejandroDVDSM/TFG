using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Touch _touch;
    
    [SerializeField] private ChessPieceSpawner _spawner;

    private ChessPieceMovement _selectedChessPiece;
    
    private void Update()
    {
        if (!GameStateManager.instance.IsPlayerTurn()) return;
        
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(_touch.position);
            
            switch (_touch.phase)
            {
                case TouchPhase.Began:
                    Collider2D hitCollider2D = Physics2D.OverlapPoint(touchPosition);
                    
                    if (hitCollider2D != null)
                        OnPlayerTouch(hitCollider2D);
                    else
                        Debug.LogError("OnPlayerTouch - Hit collider: null");
                    break;
            }
        }
    }
    
    private void OnPlayerTouch(Collider2D hitCollider)
    {
        switch (hitCollider.tag)
        {
            case "Tile":
                if (_selectedChessPiece != null && _selectedChessPiece.IsMoving)
                {
                    Debug.Log("Player is moving a piece...");
                    break;
                }
                
                Tile tileWhereToSpawn = hitCollider.gameObject.GetComponent<Tile>();
                _spawner.SpawnChessPiece(tileWhereToSpawn);                
                break;
            
            case "PlayerPiece":
                if (_selectedChessPiece != null && _selectedChessPiece.IsMoving && hitCollider.gameObject == _selectedChessPiece.gameObject)
                {
                    if (_touch.tapCount == 2)
                    {
                        _selectedChessPiece.GetBackToNormal();
                        _selectedChessPiece.IsMoving = false;
                        break;
                    }
                }
                
                _selectedChessPiece = hitCollider.GetComponent<ChessPieceMovement>();
                _selectedChessPiece.IsMoving = true;
                _selectedChessPiece.SetAllAvailableMoves();
                break;
            
            case "TileToMove":
                Tile targetTile = hitCollider.gameObject.GetComponent<Tile>();
                _selectedChessPiece.Move(targetTile);
                _selectedChessPiece = null;
                break;
            
            case "EnemyPiece":
                if (_selectedChessPiece != null && _selectedChessPiece.IsMoving)
                {
                    Tile targetTileWithEnemyPiece = hitCollider.transform.GetComponentInParent<Tile>();
                    _selectedChessPiece.MoveAndEat(targetTileWithEnemyPiece);
                    _selectedChessPiece = null;
                }
                break;
            
            default:
                Debug.Log("Type mismatch");
                break;
        }
    }
}
