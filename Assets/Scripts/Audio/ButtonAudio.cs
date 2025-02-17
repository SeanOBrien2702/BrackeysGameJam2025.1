using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(AudioSource))]
public class ButtonAudio : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] AudioClip hoverSound;
    [SerializeField] AudioClip pressedSound;
    Button button;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        AudioController.OnSoundFXVolumeChange += AudioController_OnSoundFXVolumeChange;
    }

    private void OnDestroy()
    {
        AudioController.OnSoundFXVolumeChange -= AudioController_OnSoundFXVolumeChange;
    }

    private void AudioController_OnSoundFXVolumeChange(float volume)
    {
        audioSource.volume = volume;
    }

    private void OnButtonClick()
    {
        if (button.interactable)
        {
            audioSource.clip = pressedSound;
            audioSource.Play();
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (button.interactable)
        {
            audioSource.clip = hoverSound;
            audioSource.Play(); 
        }
    }
}