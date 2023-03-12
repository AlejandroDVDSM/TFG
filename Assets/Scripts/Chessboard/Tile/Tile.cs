using UnityEngine;

public class Tile : MonoBehaviour
{
    private ChessboardManager _chessboardManager;

    private Vector2 _coordinates;
    
    public bool IsFree { get; set; } = true;

    private void Start()
    {
        _chessboardManager = FindObjectOfType<ChessboardManager>();
    }
    
    public void SetCoordinates(Vector2 coordinates)
    {
        _coordinates = coordinates;
    }
    
    public void CheckNearbyTiles()
    {
        Debug.Log($"Tile where I am: {name}" );

        if (IsThereAPieceAboveMe()) {
            var chessPieceAboveMe = GetTileAboveMe().GetComponentInChildren<ChessPiece>();
            GetComponentInChildren<ChessPiece>().ConnectPieces(chessPieceAboveMe);
        }
        if (IsThereAPieceBelowMe()) {
            var chessPieceBelowMe = GetTileBelowMe().GetComponentInChildren<ChessPiece>();
            GetComponentInChildren<ChessPiece>().ConnectPieces(chessPieceBelowMe);
        }
        if (IsThereAPieceAtMyLeft()) {
            var chessPieceAtMyLeft = GetTileAtMyLeft().GetComponentInChildren<ChessPiece>();
            GetComponentInChildren<ChessPiece>().ConnectPieces(chessPieceAtMyLeft);
        }
        if (IsThereAPieceAtMyRight()) {
            var chessPieceAtMyRight = GetTileAtMyRight().GetComponentInChildren<ChessPiece>();
            GetComponentInChildren<ChessPiece>().ConnectPieces(chessPieceAtMyRight);
        }
    }
    
    private bool IsThereAPieceAboveMe()
    {
        if (_coordinates.x == 0) return false; // Off the chessboard
        
        Tile tileAboveMe = GetTileAboveMe();
        ChessPiece chessPieceAboveMe = tileAboveMe.GetComponentInChildren<ChessPiece>();
        
        if (chessPieceAboveMe != null)
        {
            Debug.Log("ABOVE: true");
            return true;
        }

        Debug.Log("ABOVE: false");
        return false;
    }
    
    private Tile GetTileAboveMe()
    {
        if (_coordinates.x == 0) return null; // Off the chessboard
        Vector2 tileAboveCoordinates = new Vector2(_coordinates.x - 1, _coordinates.y);
        Tile tileAboveMe = _chessboardManager.GetTileAtPosition(tileAboveCoordinates);
        return tileAboveMe;
    }

    private bool IsThereAPieceBelowMe()
    {
        if (_coordinates.x == 5) return false; // Off the chessboard
        

        Tile tileBelowMe = GetTileBelowMe();
        ChessPiece chessPieceBelowMe = tileBelowMe.GetComponentInChildren<ChessPiece>();
        if (chessPieceBelowMe != null)
        {
            Debug.Log("BELOW: true");
            return true;
        }

        Debug.Log("BELOW: false");
        return false;
    }

    private Tile GetTileBelowMe()
    {
        if (_coordinates.x == 5) return null; // Off the chessboard
        Vector2 tileBelowCoordinates = new Vector2(_coordinates.x + 1, _coordinates.y);
        Tile tileBelowMe = _chessboardManager.GetTileAtPosition(tileBelowCoordinates);
        return tileBelowMe;
    }

    private bool IsThereAPieceAtMyLeft()
    {
        if (_coordinates.y == 0) return false; // Off the chessboard
        

        Tile tileAtMyLeft = GetTileAtMyLeft();
        ChessPiece chessPieceAtMyLeft = tileAtMyLeft.GetComponentInChildren<ChessPiece>();
        if (chessPieceAtMyLeft != null)
        {
            Debug.Log("LEFT: true");
            return true;
        }
    
        Debug.Log("LEFT: false");
        return false;
    }

    private Tile GetTileAtMyLeft()
    {
        if (_coordinates.y == 0) return null; // Off the chessboard
        
        Vector2 tileAtMyLeftCoordinates = new Vector2(_coordinates.x, _coordinates.y - 1);
        Tile tileAtMyLeft = _chessboardManager.GetTileAtPosition(tileAtMyLeftCoordinates);
        return tileAtMyLeft;
    }
    
    private bool IsThereAPieceAtMyRight()
    {
        if (_coordinates.y == 5) return false; // Off the chessboard
        

        Tile tileAtMyRight = GetTileAtMyRight();
        ChessPiece chessPieceAtMyRight = tileAtMyRight.GetComponentInChildren<ChessPiece>();
        if (chessPieceAtMyRight != null)
        {
            Debug.Log("RIGHT: true");
            return true;
        }
    
        Debug.Log("RIGHT: false");
        return false;
    }

    private Tile GetTileAtMyRight()
    {
        if (_coordinates.y == 5) return null; // Off the chessboard
        
        Vector2 tileAtMyRightCoordinates = new Vector2(_coordinates.x, _coordinates.y + 1);
        Tile tileAtMyRight = _chessboardManager.GetTileAtPosition(tileAtMyRightCoordinates);
        return tileAtMyRight;
    }
}
