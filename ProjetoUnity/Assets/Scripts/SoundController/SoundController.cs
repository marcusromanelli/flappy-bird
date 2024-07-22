using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour, ISoundController
{
    private AudioSource audioSource;
    private SoundData soundData;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void GameStarted()
    {
    }

    public void PlayFlap()
    {
        Play(soundData.Flap);
    }

    public void PlayDied()
    {
        Play(soundData.Die);
    }

    public void PlayerScored()
    {
        Play(soundData.Score);
    }

    public void Setup(SoundData soundData)
    {
        this.soundData = soundData;

        audioSource.clip = soundData.BGM;
        audioSource.Play();
    }

    private void Play(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
