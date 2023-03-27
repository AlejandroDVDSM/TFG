using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    public List<Vector2Int> GetAllAvailableMoves();
}
