using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankingTimeView : MonoBehaviour
{
    [SerializeField] private Button getRankingButton;
    [SerializeField] private TMP_Text resultText;

    private RankingTimeController controller;

    private void Awake()
    {
        controller = GetComponent<RankingTimeController>();
        getRankingButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        controller.GetRanking(resultText);
    }
}