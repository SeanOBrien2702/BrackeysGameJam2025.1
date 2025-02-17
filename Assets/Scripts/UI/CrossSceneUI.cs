using TMPro;
using UnityEngine;

public class CrossSceneUI : MonoBehaviour
{
    public static CrossSceneUI Instance { get; private set; }
    [SerializeField] GameObject settingsUI;

    //[Header("Time UI")]
    //[SerializeField] GameObject timePanel;
    //[SerializeField] TextMeshProUGUI dayText;
    //[SerializeField] TextMeshProUGUI timeOfDayText;

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

    void Start()
    {
        //timePanel.SetActive(false);
        SceneController.OnSceneChange += SceneController_OnSceneChange;
    }

    private void OnDestroy()
    {
        SceneController.OnSceneChange -= SceneController_OnSceneChange;
    }

    private void SceneController_OnSceneChange(string sceneName)
    {
        //if()
        //{
        //    timePanel.SetActive(false);
        //}
        //else
        //{
        //    timePanel.SetActive(true);
        //    //dayText.text = "Day " + TimeController.Instance.Day;
        //   // timeOfDayText.text = TimeController.Instance.TimeOfday.ToString();
        //}
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
    }

    public void MainMenu()
    {
        settingsUI.SetActive(false);
        SceneController.Instance.LoadNextScene(K.MenuScene);
    }
}