using UnityEngine;

[CreateAssetMenu(fileName = "New Booster", menuName = "Booster")]
public class Booster : ScriptableObject
{
    public string BoosterName;
    
    public Sprite Sprite;
    public int Cost;
}
