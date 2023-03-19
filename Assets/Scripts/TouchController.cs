using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Touch _touch;
    
    [SerializeField] private ChessPieceSpawner _spawner;

    private GameObject _selectedChessPiece;
    
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
                    OnPlayerTouch(hitCollider2D);
                    break;
            }
        }
    }
    
    private void OnPlayerTouch(Collider2D hitCollider)
    {
        switch (GetHitColliderTag(hitCollider))
        {
            case "Tile":
                if (_selectedChessPiece != null && _selectedChessPiece.GetComponent<ChessPieceMovement>().IsMoving)
                {
                    Debug.Log("Player is moving a piece...");
                    return;
                }
                
                Tile tileWhereToSpawn = hitCollider.gameObject.GetComponent<Tile>();
                _spawner.SpawnChessPiece(tileWhereToSpawn);                
                break;
            
            case "PlayerPiece":
                if (_selectedChessPiece != null && _selectedChessPiece.GetComponent<ChessPieceMovement>().IsMoving)
                {
                    Debug.Log("Player is already moving one piece...");
                    return;
                }
                
                _selectedChessPiece = hitCollider.gameObject;
                ChessPieceMovement chessPieceMovement = _selectedChessPiece.GetComponent<ChessPieceMovement>(); 
                chessPieceMovement.IsMoving = true;
                chessPieceMovement.SetAllAvailableMoves();
                break;
            
            case "TileToMove":
                Tile targetTile = hitCollider.gameObject.GetComponent<Tile>();
                _selectedChessPiece.GetComponent<ChessPieceMovement>().Move(targetTile);
                _selectedChessPiece = null;
                break;
            
            default:
                Debug.Log("Type mismatch");
                break;
        }
    }
    
    private string GetHitColliderTag(Collider2D hitCollider)
    {
        return hitCollider.tag;
    }
}
