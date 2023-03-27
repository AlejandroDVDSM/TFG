using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceData : MonoBehaviour
{
    [SerializeField] private Piece _chessPiece;

    private void Start()
    {
        SetSprite();
    }

    private void SetSprite()
    {
        GetComponent<SpriteRenderer>().sprite = _chessPiece.sprite;
    }
    
    public Sprite GetSprite()
    {
        return _chessPiece.sprite;
    }
    
    public Type GetChessPieceType()
    {
        return _chessPiece.type;
    }
    
    public Tile GetInWhichTileIAm()
    {
        return GetComponentInParent<Tile>();
    }    
}
