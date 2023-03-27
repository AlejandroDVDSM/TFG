using UnityEngine;

[CreateAssetMenu(fileName = "New Piece", menuName = "Piece")]
public class Piece : ScriptableObject
{
    [Space]
    public Type type;
    public Sprite sprite;
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