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
    [NonSerialized] public bool Completo_MovimientoBasico;
    [NonSerialized] public bool Completo_Derrape;
    [NonSerialized] public bool Completo_Turbo;
    [NonSerialized] public bool Completo_RecargaEnergia;

    [Header("Objetivos por fase")] 

    //--------------------------------------------------------------------------------
    //Estas variables se pueden usar para el UI para aclarar los objetivos al jugador
    //--------------------------------------------------------------------------------

    /*[NonSerialized]*/ public int movimientoBasico = 0; //Necesita llegar al valor de 3 (W, A, D)
    [NonSerialized] public bool W_pressed;
    [NonSerialized] public bool A_pressed;
    [NonSerialized] public bool D_pressed;

    [NonSerialized] public int derrape = 0; //Necesita llegar a 5 esquinas derrapando para continuar

    //[Header("Scrips")]
    //[SerializeField] Turbo turboScript;

    public int dialogo = 0;

    //1: Derrape
    //2: Turbo
    //3: Hazard
    //4: SideArrack
    //5: Final
    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    private void Update()
    {
        MovimientoBasico();
        Derrape();
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

        if(movimientoBasico == 3 && dialogo == 1)
        {
            Completo_MovimientoBasico = true;
        }
    }

    void Derrape()
    {
        if (derrape == 5)
        {
            Completo_Derrape = true;
        }
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(5);

        dialogo = 1;
    }

    
}
