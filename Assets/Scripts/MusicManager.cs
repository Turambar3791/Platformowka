using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Music")]
    public AudioSource musicSource;
    public AudioClip backgroundMusic;

    void Awake()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
}
