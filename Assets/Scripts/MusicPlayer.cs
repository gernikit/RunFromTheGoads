using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _firstMusic;
    [SerializeField] private AudioClip _secondMusic;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = _firstMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void SetLoop(bool loop)
    {
        audioSource.loop = loop;
    }

    public void PlaySecondMusic()
    {
        audioSource.clip = _secondMusic;
        audioSource.Play();
    }
}
