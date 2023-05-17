using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Touch _touch;
    
    [SerializeField] private ChessPieceSpawner _spawner;

    private ChessPieceMovement _selectedChessPiece;
    
    private void Update()
    {
        if (!GameStateManager.Instance.IsPlayerTurn()) return;
        
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
                Debug.Log("Tag: Tile");
                if (_selectedChessPiece != null && _selectedChessPiece.IsMoving)
                {
                    Debug.Log("Player is moving a piece...");
                    AudioManager.Instance.Play("Error");
                    break;
                }
                
                Tile tileWhereToSpawn = hitCollider.gameObject.GetComponent<Tile>();
                _spawner.SpawnChessPiece(tileWhereToSpawn);                
                break;
            
            case "PlayerPiece":
                Debug.Log("Tag: PlayerPiece");
                if (_selectedChessPiece != null && _selectedChessPiece.IsMoving)
                {
                    // Cancel movement with the selected chess piece
                    if (hitCollider.gameObject == _selectedChessPiece.gameObject)
                    {
                        if (_touch.tapCount == 2)
                        { // Go back to normal if we have tap two times in a row
                            _selectedChessPiece.GetBackToNormal();
                            _selectedChessPiece.IsMoving = false;
                            break;
                        }                        
                    }
                    else
                    { // Go back to normal if we have touch a different chess piece
                        _selectedChessPiece.GetBackToNormal();
                        _selectedChessPiece.IsMoving = false;
                        
                    }
                }
                
                _selectedChessPiece = hitCollider.GetComponent<ChessPieceMovement>();
                _selectedChessPiece.IsMoving = true;
                _selectedChessPiece.SetAllAvailableMoves();
                break;
            
            
            case "TileToMove":
                Debug.Log("Tag: TileToMove");
                Tile targetTile = hitCollider.gameObject.GetComponent<Tile>();
                _selectedChessPiece.Move(targetTile);
                _selectedChessPiece = null;
                break;
            
            case "EnemyPiece":
                Debug.Log("Tag: EnemyPiece");
                if (_selectedChessPiece != null && _selectedChessPiece.IsMoving)
                {
                    Tile targetTileWithEnemyPiece = hitCollider.transform.GetComponentInParent<Tile>();

                    if (targetTileWithEnemyPiece.tag.Equals("TileToMove"))
                    {
                        _selectedChessPiece.MoveAndEat(targetTileWithEnemyPiece);
                        _selectedChessPiece = null;
                    }
                }
                break;
            
            default:
                Debug.Log("Type mismatch");
                break;
        }
    }
}
