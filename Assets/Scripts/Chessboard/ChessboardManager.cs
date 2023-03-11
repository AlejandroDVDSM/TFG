using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessboardManager : MonoBehaviour
{
    [SerializeField] private Tile _tilePrefab;
    
    private Dictionary<Vector2, Tile> _tiles;

    private void Start()
    {
        GenerateChessboard();
    }

    void GenerateChessboard()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        
        int constraintCount = GetComponent<GridLayoutGroup>().constraintCount;
        
        for (int row = 0; row < constraintCount; row++) 
        {
            for (int column = 0; column < constraintCount; column++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(row, column), Quaternion.identity, transform);
                spawnedTile.name = $"Tile {row} {column}";

                bool isOffset = (row % 2 == 0 && column % 2 != 0) || (row % 2 != 0 && column % 2 == 0);
                spawnedTile.GetComponent<TileAppearance>().SetSprite(isOffset);

                Vector2 coordinates = new Vector2(row, column);
                spawnedTile.SetCoordinates(coordinates);

                _tiles.Add(coordinates, spawnedTile);
            }
        }
    }

    public Tile GetTileAtPosition(Vector2 coordinates)
    {
        return _tiles.TryGetValue(coordinates, out var tile) ? tile : null;
    }
}
