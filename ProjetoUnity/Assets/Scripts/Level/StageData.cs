using UnityEngine;

[CreateAssetMenu(fileName ="StageData", menuName ="Data/StageData")]
public class StageData : ScriptableObject
{
    [SerializeField] public float spawnDistance;
    [SerializeField] public Texture backgroundTexture;
    [SerializeField] public Texture floorTexture;
    [SerializeField] public Obstacle obstaclePrefab;
    [SerializeField] public ScreenflashData screenflashData;
    [SerializeField] public SoundData soundData;
    [SerializeField] public SkinData[] availableSkins;
}
