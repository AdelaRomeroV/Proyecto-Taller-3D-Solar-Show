using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetPlayerTimeView : MonoBehaviour
{
    [SerializeField] private Button getTimeButton;
    [SerializeField] private TMP_Text resultText;

    private GetPlayerTimeController controller;

    private void Awake()
    {
        controller = GetComponent<GetPlayerTimeController>();
        getTimeButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        controller.GetPlayerTime(resultText);
    }
}