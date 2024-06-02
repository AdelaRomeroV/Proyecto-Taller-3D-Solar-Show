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
    public bool Completo_MovimientoBasico;
    public bool Completo_Derrape;
    [NonSerialized] public bool Completo_Turbo;
    [NonSerialized] public bool Completo_RecargaEnergia;

    [Header("Objetivos por fase")] 

    //--------------------------------------------------------------------------------
    //Estas variables se pueden usar para el UI para aclarar los objetivos al jugador
    //--------------------------------------------------------------------------------

    [NonSerialized] public int movimientoBasico = 0; //Necesita llegar al valor de 3 (W, A, D)
    [NonSerialized] public bool W_pressed;
    [NonSerialized] public bool A_pressed;
    [NonSerialized] public bool D_pressed;

     public int derrape = 0; //Necesita llegar a 5 esquinas derrapando para continuar

    //[Header("Scrips")]
    //[SerializeField] Turbo turboScript;

    private void Update()
    {
        MovimientoBasico();
        Derrape();

        if(movimientoBasico >= 3 && derrape >= 4)
        {

        }
    }

    void MovimientoBasico()
    {
        if (Input.GetKeyDown(KeyCode.W) && !W_pressed)
        {
            W_pressed = true;
            movimientoBasico++;
        }
        if (Input.GetKeyDown(KeyCode.A) && !A_pressed)
        {
            A_pressed = true;
            movimientoBasico++;
        }
        if (Input.GetKeyDown(KeyCode.D) && !D_pressed)
        {
            D_pressed = true;
            movimientoBasico++;
        }

        if(movimientoBasico >= 3)
        {
            Completo_MovimientoBasico = true;
        }
    }

    void Derrape()
    {
        if (derrape >= 4)
        {
            Completo_Derrape = true;
        }
    }
}
