using UnityEngine;

[CreateAssetMenu(fileName = "New Booster", menuName = "Booster")]
public class Booster : ScriptableObject
{
    [Space]
    public string boosterName;
    [TextArea]
    public string description;
    public Sprite sprite;
    public int cost;
}
