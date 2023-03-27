using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceSelector : MonoBehaviour
{
    private GameObject _selectedChessPiece;

    public void SelectChessPiece(GameObject chessPiece)
    {
        _selectedChessPiece = chessPiece;
    }
    
    

}
