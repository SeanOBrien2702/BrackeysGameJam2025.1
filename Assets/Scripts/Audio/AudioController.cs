using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource musicSource;
    [SerializeField] AudioClip mainMenuMusic;

    public static AudioController Instance { get; private set; }
    int currentMusic = 0;

    float musicVolume = 0.5f;
    float soundVolume = 0.5f;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        musicSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        GameSettings.OnMusicVolumeChange += GameSettings_OnMusicVolumeChange;
        GameSettings.OnSoundFXVolumeChange += GameSettings_OnSoundFXVolumeChange;
    }

    private void OnDestroy()
    {
        GameSettings.OnMusicVolumeChange -= GameSettings_OnMusicVolumeChange;
        GameSettings.OnSoundFXVolumeChange -= GameSettings_OnSoundFXVolumeChange;
    }

    private void GameSettings_OnSoundFXVolumeChange(float newSoundVolume)
    {
        //soundVolume = newSoundVolume;
        //.setVolume(soundVolume);
    }

    private void GameSettings_OnMusicVolumeChange(float newMusicVolume)
    {
        musicVolume = newMusicVolume;
        musicSource.volume = musicVolume;
    }
}