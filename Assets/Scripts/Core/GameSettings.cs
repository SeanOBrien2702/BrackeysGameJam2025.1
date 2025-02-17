using System;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public static event Action<float> OnMusicVolumeChange = delegate { };
    public static event Action<float> OnSoundFXVolumeChange = delegate { };

    [SerializeField] Slider musicVolume;
    [SerializeField] Slider soundFXVolume;

    void Awake()
    {
        musicVolume.onValueChanged.AddListener(delegate { UpdateMusicVolume(); });
        soundFXVolume.onValueChanged.AddListener(delegate { UpdateSoundFXVolume(); });
    }

    private void UpdateMusicVolume()
    {
        OnMusicVolumeChange?.Invoke(musicVolume.value);
    }

    private void UpdateSoundFXVolume()
    {
        OnSoundFXVolumeChange?.Invoke(soundFXVolume.value);
    }
}