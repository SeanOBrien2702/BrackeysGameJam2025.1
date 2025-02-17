using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] GameObject victoryScreen;
    [SerializeField] GameObject defeatScreen;

    void Start()
    {
        victoryScreen.SetActive(false);
        defeatScreen.SetActive(false);
        Boss.OnBossDefeated += Boss_OnBossDefeated;
        PlayerController.OnPlayerDefeated += PlayerController_OnPlayerDefeated;
    }

    void OnDestroy()
    {
        Boss.OnBossDefeated -= Boss_OnBossDefeated;
        PlayerController.OnPlayerDefeated -= PlayerController_OnPlayerDefeated;
    }

    void PlayerController_OnPlayerDefeated()
    {
        victoryScreen.SetActive(true);
    }

    void Boss_OnBossDefeated()
    {
        defeatScreen.SetActive(true);
    }

    public void MainMenuButton()
    {
        SceneController.Instance.LoadNextScene(K.MenuScene);
    }
}