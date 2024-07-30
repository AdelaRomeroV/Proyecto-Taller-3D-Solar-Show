using TMPro;
using UnityEngine;

public class DriftGoalText : MonoBehaviour
{
    [SerializeField] ControladorTutorial controlador;
    TextMeshProUGUI text;
    [SerializeField] bool isDrift;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        if (isDrift)
        {
            if(controlador.derrape <= 3)
            {
                text.text = $"Derrapes Exitosos:{controlador.derrape}/3";
            }
        }
    }

    public void SAtext()
    {
        isDrift = false;
    }
}
