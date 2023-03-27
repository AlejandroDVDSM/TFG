using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Piece", menuName = "Piece")]
public class Piece : ScriptableObject
{
    public Type type;

    public Sprite sprite;

    public int nDirections;
}

public enum Type
{
    Pawn,
    Bishop,
    Knight,
    Rook,
    Queen,
    King
}