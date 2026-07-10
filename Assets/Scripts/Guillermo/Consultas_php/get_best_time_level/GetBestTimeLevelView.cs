using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetBestTimeLevelView : MonoBehaviour
{
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button level1Button;
    [SerializeField] private Button level2Button;
    [SerializeField] private Button levelBossButton;
    [SerializeField] private TMP_Text resultText;

    private GetBestTimeLevelController controller;

    private void Awake()
    {
        controller = GetComponent<GetBestTimeLevelController>();

        tutorialButton.onClick.AddListener(OnClickTutorial);
        level1Button.onClick.AddListener(OnClickLevel1);
        level2Button.onClick.AddListener(OnClickLevel2);
        levelBossButton.onClick.AddListener(OnClickLevelBoss);
    }

    private void OnClickTutorial()
    {
        controller.GetBestTime(1, resultText);
    }

    private void OnClickLevel1()
    {
        controller.GetBestTime(2, resultText);
    }

    private void OnClickLevel2()
    {
        controller.GetBestTime(3, resultText);
    }

    private void OnClickLevelBoss()
    {
        controller.GetBestTime(4, resultText);
    }
}