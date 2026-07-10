using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InsertLevelTimeView : MonoBehaviour
{
    [SerializeField] private int levelId;
    [SerializeField] private int timeToSave;
    [SerializeField] private Button saveButton;
    [SerializeField] private TMP_Text resultText;

    private InsertLevelTimeController controller;

    private void Awake()
    {
        controller = GetComponent<InsertLevelTimeController>();
        saveButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        controller.SaveTime(PlayerIdManager.currentPlayerId, levelId, timeToSave, resultText);
    }
}