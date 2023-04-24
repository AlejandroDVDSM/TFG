using UnityEngine;

public class BoosterDoublePointsFactory : MonoBehaviour, IBoosterFactory
{
    public void ApplyBooster()
    {
        Debug.Log("'Double Points' bought");
        FindObjectOfType<PointsManager>().ActivateDoublePoints();
    }
}
