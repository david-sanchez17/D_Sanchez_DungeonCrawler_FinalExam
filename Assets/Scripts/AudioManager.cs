using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";
    private const float DefaultVolume = 1f;

    private void Awake()
    {
        Instance = this;
        LoadAudioSettings();
    }

    private void LoadAudioSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, DefaultVolume);
        float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey, DefaultVolume);
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
        PlayerPrefs.Save();
    }

    public float GetMusicVolume()
    {
        return musicSource.volume;
    }

    public float GetSFXVolume()
    {
        return sfxSource.volume;
    }

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

    public void StopMusic()
    {
        if (musicSource == null)
        {
            return;
        }
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource == null || clip == null)
        {
            return;
        }
        sfxSource.PlayOneShot(clip);
    }

    public void StopSFX()
    {
        if (sfxSource == null)
        {
            return;
        }
        sfxSource.Stop();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
