using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private float ambientSource;

    public void PlayMusic(AudioClip music)
    {
        if (musicSource == null || music == null)
        {
            return;
        }
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
