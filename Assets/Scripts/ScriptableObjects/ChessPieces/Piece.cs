using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Piece", menuName = "Piece")]
public class Piece : ScriptableObject
{
    public Type Type;
    //public Sprite sprite;
    public Team Team;
    public int Points;
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

public enum Team
{
    White,
    Black
}