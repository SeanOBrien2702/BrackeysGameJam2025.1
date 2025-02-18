using System;
using UnityEngine;

public class CrossSceneUI : MonoBehaviour
{
    public static event Action<bool> OnUIToggled = delegate { };
    public static CrossSceneUI Instance { get; private set; }
    [SerializeField] GameObject settingsUI;

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
        settingsUI.SetActive(false);
    }

    private void Update()
    {      
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettings();
        }
    }

    public void ToggleSettings()
    {
        settingsUI.SetActive(!settingsUI.activeSelf);
        OnUIToggled?.Invoke(settingsUI.activeSelf);
    }

    public void MainMenu()
    {
        settingsUI.SetActive(false);
        SceneController.Instance.LoadNextScene(K.MenuScene);
    }
}