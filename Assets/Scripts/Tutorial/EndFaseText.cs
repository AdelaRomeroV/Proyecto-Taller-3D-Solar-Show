using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndFaseText : MonoBehaviour
{
    ControladorTutorial controlador;
    TextMeshProUGUI text;

    [SerializeField] bool move;
    [SerializeField] bool drift;
    [SerializeField] bool turbo;
    [SerializeField] bool side;
    [SerializeField] bool energy;

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
        if (move && controlador.Completo_MovimientoBasico) changeText();
        else if(drift && controlador.Completo_Derrape) changeText();
        else if(turbo && controlador.Completo_Turbo) changeText();
        else if(side && controlador.Completo_SideAttack) changeText();
        else if(energy && controlador.Completo_RecargaEnergia) changeText();
    }
}
