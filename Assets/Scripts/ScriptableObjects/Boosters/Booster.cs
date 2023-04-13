using UnityEngine;

[CreateAssetMenu(fileName = "New Booster", menuName = "Booster")]
public class Booster : ScriptableObject
{
    public string BoosterName;
    
    [TextArea]
    public string Description;
    public Sprite Sprite;
    public int Cost;
}
