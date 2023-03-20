using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessboardManager : MonoBehaviour
{
    [SerializeField] private Tile _tilePrefab;
    
    private Dictionary<Vector2Int, Tile> _tiles;

    private void Start()
    {
        GenerateChessboard();
    }

    void GenerateChessboard()
    {
        _tiles = new Dictionary<Vector2Int, Tile>();
        
        int constraintCount = GetComponent<GridLayoutGroup>().constraintCount;
        
        for (int row = 0; row < constraintCount; row++) 
        {
            for (int column = 0; column < constraintCount; column++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(row, column), Quaternion.identity, transform);
                spawnedTile.name = $"Tile - R:{row} C:{column}";

                bool isOffset = (row % 2 == 0 && column % 2 != 0) || (row % 2 != 0 && column % 2 == 0);
                spawnedTile.GetComponent<TileAppearance>().SetSprite(isOffset);

                Vector2Int coordinates = new Vector2Int(row, column);
                spawnedTile.Coordinates = coordinates;

                _tiles.Add(coordinates, spawnedTile);
            }
        }
    }

    public Tile GetTileAtPosition(Vector2Int coordinates)
    {
        return _tiles.TryGetValue(coordinates, out var tile) ? tile : null;
    }
}
