using UnityEngine;

[CreateAssetMenu(fileName= "SkinData", menuName = "Data/New Skin")]
public class SkinData : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] new private string name;
    [SerializeField] private int layerIndex;

    public Sprite Icon => icon;
    public string Name => name;
    public int LayerIndex => layerIndex;
}
