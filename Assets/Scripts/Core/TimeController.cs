using System;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    bool isForcedPaused = false;

    private void Start()
    {
        Boss.OnBossDefeated += Boss_OnBossDefeated;
        PlayerController.OnPlayerDefeated += PlayerController_OnPlayerDefeated;
        SceneController.OnSceneChange += SceneController_OnSceneChange;
        CrossSceneUI.OnUIToggled += CrossSceneUI_OnUIToggled;
    }

    private void OnDestroy()
    {
        Boss.OnBossDefeated -= Boss_OnBossDefeated;
        PlayerController.OnPlayerDefeated -= PlayerController_OnPlayerDefeated;
        SceneController.OnSceneChange -= SceneController_OnSceneChange;
        CrossSceneUI.OnUIToggled -= CrossSceneUI_OnUIToggled;
    }

    private void Boss_OnBossDefeated()
    {
        Time.timeScale = 0;
        isForcedPaused = true;
    }

    private void PlayerController_OnPlayerDefeated()
    {
        Time.timeScale = 0;
        isForcedPaused = true;
    }

    private void SceneController_OnSceneChange(string sceneName)
    {
        Time.timeScale = 1;
        isForcedPaused = false;
    }

    private void CrossSceneUI_OnUIToggled(bool isToggled)
    {
        if (!isForcedPaused)
        {
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        }
    }
}