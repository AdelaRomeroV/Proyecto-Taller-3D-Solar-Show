using TMPro;
using UnityEngine;

public class DriftGoalText : MonoBehaviour
{
    ControladorTutorial controlador;
    TextMeshProUGUI text;

    private void Start()
    {
        controlador = GameObject.Find("Controlador").GetComponent<ControladorTutorial>();
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        if(controlador.derrape <= 5)
        {
            text.text = $"Derrapes Existosos:{controlador.derrape}/5";
        }
    }
}
