using UnityEngine;

public class Tile : MonoBehaviour
{
    private ChessboardManager _chessboardManager;

    public bool IsFree { get; set; } = true;

    public Vector2Int Coordinates { get; set; }

    private void Start()
    {
        _chessboardManager = FindObjectOfType<ChessboardManager>();
    }
    
    public void CheckNearbyTiles()
    {
        Debug.Log($"Tile where I am: {name}" );

        if (IsThereAPieceAboveMe()) {
            var chessPieceAboveMe = GetTileAboveMe().GetComponentInChildren<ChessPieceConnections>();
            
            // Check if is not an enemy piece
            if (chessPieceAboveMe != null) 
                GetComponentInChildren<ChessPieceConnections>().ConnectPieces(chessPieceAboveMe);
        }
        if (IsThereAPieceBelowMe()) {
            var chessPieceBelowMe = GetTileBelowMe().GetComponentInChildren<ChessPieceConnections>();
            
            // Check if is not an enemy piece
            if (chessPieceBelowMe != null)
                GetComponentInChildren<ChessPieceConnections>().ConnectPieces(chessPieceBelowMe);
        }
        if (IsThereAPieceAtMyLeft()) {
            var chessPieceAtMyLeft = GetTileAtMyLeft().GetComponentInChildren<ChessPieceConnections>();
            
            // Check if is not an enemy piece
            if (chessPieceAtMyLeft != null)
                GetComponentInChildren<ChessPieceConnections>().ConnectPieces(chessPieceAtMyLeft);
        }
        if (IsThereAPieceAtMyRight()) {
            var chessPieceAtMyRight = GetTileAtMyRight().GetComponentInChildren<ChessPieceConnections>();
            
            // Check if is not an enemy piece
            if (chessPieceAtMyRight != null)
                GetComponentInChildren<ChessPieceConnections>().ConnectPieces(chessPieceAtMyRight);
        }
    }
    
    public bool IsThereAPieceAboveMe()
    {
        if (Coordinates.x == 0) return false; // Off the chessboard
        
        Tile tileAboveMe = GetTileAboveMe();
        
        if (!tileAboveMe.IsFree)
        {
            // Debug.Log("ABOVE: true");
            return true;
        }

        // Debug.Log("ABOVE: false");
        return false;
    }
    
    private Tile GetTileAboveMe()
    {
        if (Coordinates.x == 0) return null; // Off the chessboard
        Vector2Int tileAboveCoordinates = new Vector2Int(Coordinates.x - 1, Coordinates.y);
        Tile tileAboveMe = _chessboardManager.GetTileAtPosition(tileAboveCoordinates);
        return tileAboveMe;
    }

    public bool IsThereAPieceBelowMe()
    {
        if (Coordinates.x == 5) return false; // Off the chessboard
        

        Tile tileBelowMe = GetTileBelowMe();
        if (!tileBelowMe.IsFree)
        {
            // Debug.Log("BELOW: true");
            return true;
        }

        // Debug.Log("BELOW: false");
        return false;
    }

    private Tile GetTileBelowMe()
    {
        if (Coordinates.x == 5) return null; // Off the chessboard
        Vector2Int tileBelowCoordinates = new Vector2Int(Coordinates.x + 1, Coordinates.y);
        Tile tileBelowMe = _chessboardManager.GetTileAtPosition(tileBelowCoordinates);
        return tileBelowMe;
    }

    public bool IsThereAPieceAtMyLeft()
    {
        if (Coordinates.y == 0) return false; // Off the chessboard
        

        Tile tileAtMyLeft = GetTileAtMyLeft();
        if (!tileAtMyLeft.IsFree)
        {
            // Debug.Log("LEFT: true");
            return true;
        }
    
        // Debug.Log("LEFT: false");
        return false;
    }

    private Tile GetTileAtMyLeft()
    {
        if (Coordinates.y == 0) return null; // Off the chessboard
        
        Vector2Int tileAtMyLeftCoordinates = new Vector2Int(Coordinates.x, Coordinates.y - 1);
        Tile tileAtMyLeft = _chessboardManager.GetTileAtPosition(tileAtMyLeftCoordinates);
        return tileAtMyLeft;
    }
    
    public bool IsThereAPieceAtMyRight()
    {
        if (Coordinates.y == 5) return false; // Off the chessboard
        

        Tile tileAtMyRight = GetTileAtMyRight();
        if (!tileAtMyRight.IsFree)
        {
            // Debug.Log("RIGHT: true");
            return true;
        }
    
        // Debug.Log("RIGHT: false");
        return false;
    }

    private Tile GetTileAtMyRight()
    {
        if (Coordinates.y == 5) return null; // Off the chessboard
        
        Vector2Int tileAtMyRightCoordinates = new Vector2Int(Coordinates.x, Coordinates.y + 1);
        Tile tileAtMyRight = _chessboardManager.GetTileAtPosition(tileAtMyRightCoordinates);
        return tileAtMyRight;
    }

    public bool IsThereAPieceAt(Vector2Int coordinates)
    {
        Tile tileAt = _chessboardManager.GetTileAtPosition(coordinates);
        if (!tileAt.IsFree) return true;
        
        return false;
    }
}
