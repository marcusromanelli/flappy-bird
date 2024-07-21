using AYellowpaper;
using UnityEngine;

public class StageData : ScriptableObject
{
    [RequireInterface(typeof(IStage))]
    public MonoBehaviour prefab;
}
