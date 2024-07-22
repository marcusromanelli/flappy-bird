using UnityEngine;

[CreateAssetMenu(fileName ="SoundData", menuName ="Data/SoundData")]
public class SoundData : ScriptableObject
{
    [SerializeField] public AudioClip BGM;
    [SerializeField] public AudioClip Die;
    [SerializeField] public AudioClip Score;
    [SerializeField] public AudioClip Flap;
}
