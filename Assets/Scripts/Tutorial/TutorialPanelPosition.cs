using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanelPosition : MonoBehaviour
{
    [Header("Positions")]
    [SerializeField] private Vector2 _top;
    [SerializeField] private Vector2 _middle;
    [SerializeField] private Vector2 _bottom;

    // Start is called before the first frame update
    void Start()
    {
        BottomPosition();
    }

    public void TopPosition()
    {
        transform.localPosition = _top;
    }
    
    public void MiddlePosition()
    {
        transform.localPosition = _middle;
    }
    
    public void BottomPosition()
    {
        transform.localPosition = _bottom;
    }
}
