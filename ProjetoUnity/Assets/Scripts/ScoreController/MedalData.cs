using UnityEngine;

[CreateAssetMenu(fileName = "MedalData", menuName = "Data/Medal")]
public class MedalData : ScriptableObject
{
    public Sprite medalSprite;
    public int requiredPoints;
}
