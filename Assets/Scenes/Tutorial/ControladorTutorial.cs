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

    [Header("Zonas de pista")]
    public GameObject PistaRecta;
    public List<GameObject> Zonas = new List<GameObject>();
    public List<GameObject> Dialogos = new List<GameObject>();

    [Header("FASES")]
    [NonSerialized] public bool Completo_MovimientoBasico;
    [NonSerialized] public bool Completo_Derrape;
    [NonSerialized] public bool Completo_Turbo;
    [NonSerialized] public bool Completo_SideAttack;
    [NonSerialized] public bool Completo_RecargaEnergia;

    [Header("Scrips")]
    [SerializeField] Turbo turboScript;
    [SerializeField] GameObject BarraDeEnergía;

    [Header("Objetivos por fase")] 

    //--------------------------------------------------------------------------------
    //Estas variables se pueden usar para el UI para aclarar los objetivos al jugador
    //--------------------------------------------------------------------------------
    public int dialogo = -1;

    /*[NonSerialized]*/ public int movimientoBasico = 0; //Necesita llegar al valor de 3
    [NonSerialized] public bool W_pressed;
    [NonSerialized] public bool A_pressed;
    [NonSerialized] public bool D_pressed;

    /*[NonSerialized]*/ public int derrape = 0; //Necesita llegar a 5 esquinas derrapando para continuar

    [SerializeField] [Range(0, 100)] float TurboGoal;

    public int sideAttack = 0;
    public bool RightClick_Pressed;
    public bool LeftClick_Pressed;

    //0: Derrape
    //1: Turbo
    //2: Side Attack
    //3: Recargar Energia
    //4: Final
    private void Start()
    {
        turboScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Turbo>();
        turboScript.enabled = false;

        StartCoroutine(StartDelay());
    }

    private void Update()
    {
        MovimientoBasico();
        Derrape();

        if (Completo_Derrape)
        {
            turboScript.enabled = true;
        }

        Turbo();
        SideAttack();
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

        if(movimientoBasico == 3 && dialogo == 0)
        {
            Completo_MovimientoBasico = true;
            Debug.Log("Basic COmplete");
        }
    }

    void Derrape()
    {
        if (derrape == 5)
        {
            if (!Completo_Derrape) Debug.Log("Drift COmplete");

            Completo_Derrape = true;
        }
    }

    void Turbo()
    {

        //Poner la energía necesaria para pasar a la siguiente fase

        if(!Completo_Turbo && turboScript.CurrentEnergy < 50)
        {
            Completo_Turbo = true;
            Debug.Log("TUrbo COmplete");
        }
    }

    void SideAttack()
    {
        if (Completo_Turbo)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !LeftClick_Pressed)
            {
                LeftClick_Pressed = true;
                sideAttack++;
                turboScript.CanAttackLeft = false;
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && !RightClick_Pressed)
            {
                RightClick_Pressed = true;
                sideAttack++;
                turboScript.CanAttackRight = false;
            }
        }

        if(sideAttack >= 2 && dialogo == 2)
        {
            Completo_SideAttack = true;
            Debug.Log("SideAttack COmplete");
        }
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(5);

        dialogo = 0;
    }

    
}
