using System.Linq;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Touch _touch;
    [SerializeField] private ChessPieceSpawner _spawner;
    
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
                Tile tile = hitCollider.GetComponent<Tile>();
                HandleTouchedTile(tile);
                break;

            case "TileToMove":
                Tile targetTile = hitCollider.GetComponent<Tile>();
                HandleTouchedTileToMove(targetTile);
                break;
        }
    }

    private void HandleTouchedTile(Tile tile)
    {
        bool isThereAPieceMoving = FindObjectsOfType<ChessPieceMovement>().Any(cP => cP.IsMoving);

        if (tile.IsFree && !isThereAPieceMoving)
            _spawner.SpawnChessPiece(tile);
        else
        {
            ChessPieceData chessPieceToMove = tile.GetChessPiece();

            if (chessPieceToMove == null)
                return;

            if (chessPieceToMove.GetTeam() == Team.Black)
            {
                ChessPieceMovement chessPieceMovement = chessPieceToMove.GetComponent<ChessPieceMovement>();
                if (!chessPieceMovement.IsMoving)
                {
                    var previousChessPieceMovement = FindObjectsOfType<ChessPieceMovement>()
                        .Where(chessPiece => chessPiece.IsMoving).ToList();

                    foreach (var previousChessPiece in previousChessPieceMovement)
                        previousChessPiece.StopMoving();

                    chessPieceMovement.StartMoving();
                }
                else
                    chessPieceMovement.StopMoving();
            }
        }
    }

    private void HandleTouchedTileToMove(Tile targetTile)
    {
        var chessPieceMoving = FindObjectsOfType<ChessPieceMovement>().FirstOrDefault(cP => cP.IsMoving);

        if (chessPieceMoving == null)
            return;

        if (!targetTile.IsFree)
            chessPieceMoving.MoveAndEat(targetTile);
        else
            chessPieceMoving.Move(targetTile);
    }
}
