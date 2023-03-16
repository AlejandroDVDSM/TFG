using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Touch _touch;
    
    [SerializeField] private ChessPieceSpawner _spawer;
    
    private void Update()
    {
        if (!GameStateManager.instance.IsPlayerTurn()) return; // Cambiar a --> IsPlayerTurn() <--
        
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
                Tile tileWhereToSpawn = hitCollider.gameObject.GetComponent<Tile>();
                _spawer.SpawnChessPiece(tileWhereToSpawn);                
                break;
            case "PlayerPiece":
                Debug.Log("PlayerPiece");
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
