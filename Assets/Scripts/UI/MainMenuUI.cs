using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject creditsPanel;

    public void StartGame()
    {
        SceneController.Instance.LoadNextScene(K.GameScene);
    }

    public void ToggleSettigns()
    {
        CrossSceneUI.Instance.ToggleSettings();
    }

    public void ToggleCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
    }
}