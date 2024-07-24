using TMPro;
using UnityEngine;

public class EndFaseText : MonoBehaviour
{
    ControladorTutorial controlador;
    TextMeshProUGUI text;

    private void Start()
    {
        controlador = GameObject.Find("Controlador").GetComponent<ControladorTutorial>();
        text = GetComponent<TextMeshProUGUI>();
    }

    void changeText()
    {
        text.text = "¡Bien hecho! Continua hasta la siguiente zona";
    }

    private void Update()
    {
        if (controlador.Completo_RecargaEnergia)
        {
            changeText();
            Destroy(this);
        }
    }
}
