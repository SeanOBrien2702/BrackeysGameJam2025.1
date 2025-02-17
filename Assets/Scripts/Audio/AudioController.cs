using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static event Action<float> OnSoundFXVolumeChange = delegate { };
    AudioSource musicSource;
    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip[] bossMusic;
    public static AudioController Instance { get; private set; }

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

        OnSoundFXVolumeChange?.Invoke(soundVolume);
        musicSource = GetComponent<AudioSource>();
        musicSource.volume = musicVolume;     
        musicSource.clip = mainMenuMusic;
        musicSource.Play();
    }

    void Start()
    {
        GameSettings.OnMusicVolumeChange += GameSettings_OnMusicVolumeChange;
        GameSettings.OnSoundFXVolumeChange += GameSettings_OnSoundFXVolumeChange;
        SceneController.OnSceneChange += SceneController_OnSceneChange;
    }

    private void OnDestroy()
    {
        GameSettings.OnMusicVolumeChange -= GameSettings_OnMusicVolumeChange;
        GameSettings.OnSoundFXVolumeChange -= GameSettings_OnSoundFXVolumeChange;
        SceneController.OnSceneChange -= SceneController_OnSceneChange;
    }

    private void GameSettings_OnSoundFXVolumeChange(float newSoundVolume)
    {
        soundVolume = newSoundVolume;
        OnSoundFXVolumeChange?.Invoke(soundVolume);
    }

    private void GameSettings_OnMusicVolumeChange(float newMusicVolume)
    {
        musicVolume = newMusicVolume;
        musicSource.volume = musicVolume;
    }


    private void SceneController_OnSceneChange(string sceneName)
    {
        if(sceneName == K.MenuScene)
        {
            musicSource.clip = mainMenuMusic;
        }
        else
        {
            //will need to get current boss
            musicSource.clip = bossMusic[0];
        }
        musicSource.Play();
    }
}