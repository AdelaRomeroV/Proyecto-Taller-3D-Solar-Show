using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorTutorial : MonoBehaviour
{
    //----------------------------------------------------------
    //DISCLAIMER
    //Este Script seguira la secuencia descrita en el LDD
    //----------------------------------------------------------

    [Header("FASES")]
    [NonSerialized] public bool MovimientoBasico_Completo;
    [NonSerialized] public bool Derrape_Completo;
    [NonSerialized] public bool Turbo_Completo;
    [NonSerialized] public bool RecargaEnergia_Completo;

    [Header("Objetivos por fase")] 

    //--------------------------------------------------------------------------------
    //Estas variables se pueden usar para el UI para aclarar los objetivos al jugador
    //--------------------------------------------------------------------------------

    [NonSerialized] public int movimientoBasico = 0; //Necesita llegar al valor de 3 (W, A, D)
    [NonSerialized] public bool W_pressed;
    [NonSerialized] public bool A_pressed;
    [NonSerialized] public bool D_pressed;

    [NonSerialized] public int derrape = 0; //Necesita llegar a 4 esquinas derrapando para continuar

    [Header("Scrips")]
    [SerializeField] Turbo turboScript;

    private void Update()
    {
        MovimientoBasico();

        if(movimientoBasico >= 3 && derrape >= 4)
        {

        }
    }

    void MovimientoBasico()
    {
        if (Input.GetKeyUp(KeyCode.W) && !W_pressed)
        {
            W_pressed = true;
            movimientoBasico++;
        }
        if (Input.GetKeyUp(KeyCode.A) && !A_pressed)
        {
            A_pressed = true;
            movimientoBasico++;
        }
        if (Input.GetKeyUp(KeyCode.D) && !D_pressed)
        {
            D_pressed = true;
            movimientoBasico++;
        }
    }
}
