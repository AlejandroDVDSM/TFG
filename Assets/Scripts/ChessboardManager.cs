using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessboardManager : MonoBehaviour
{
    [SerializeField] private Tile _tilePrefab;
    
    private void Start()
    {
        GenerateChessboard();
    }

    void GenerateChessboard()
    {
        int constraintCount = GetComponent<GridLayoutGroup>().constraintCount;
        
        for (int row = 0; row < constraintCount; row++) 
        {
            for (int column = 0; column < constraintCount; column++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(row, column), Quaternion.identity, transform);
                spawnedTile.name = $"Tile {row} {column}";

                bool isOffset = (row % 2 == 0 && column % 2 != 0) || (row % 2 != 0 && column % 2 == 0);
                spawnedTile.SetSprite(isOffset);
            }
        }
    }
}
