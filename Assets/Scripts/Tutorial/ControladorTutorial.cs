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
    [NonSerialized] public bool Left_pressed;
    [NonSerialized] public bool Right_pressed;

    /*[NonSerialized]*/ public int derrape = 0; //Necesita llegar a 5 esquinas derrapando para continuar

    [SerializeField] [Range(0, 100)] float TurboGoal;

    public int sideAttack = 0;
    public bool D_Pressed;
    public bool A_Pressed;

    //0: Derrape
    //1: Turbo
    //2: Side Attack
    //3: Recargar Energia
    //4: Final
    private void Start()
    {
        turboScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Turbo>();
        turboScript.enabled = false;
        BarraDeEnergía.SetActive(false);

        StartCoroutine(StartDelay());
    }

    private void Update()
    {
        MovimientoBasico();
        Derrape();
        Turbo();
        SideAttack();
        EnergyCharge();
        Final();
    }


    void MovimientoBasico()
    {
        if (Input.GetKeyDown(KeyCode.W) && !W_pressed)
        {
            W_pressed = true;
            movimientoBasico++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !Left_pressed)
        {
            Left_pressed = true;
            movimientoBasico++;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !Right_pressed)
        {
            Right_pressed = true;
            movimientoBasico++;
        }

        if(movimientoBasico == 3 && dialogo == 0)
        {
            Completo_MovimientoBasico = true;
            Debug.Log("Basic Complete");
        }
    }

    void Derrape()
    {
        if (derrape == 5)
        {
            if (!Completo_Derrape) Debug.Log("Drift Complete");

            Completo_Derrape = true;
        }
    }

    void Turbo()
    {
        if (Completo_Derrape)
        {
            turboScript.enabled = true;
            BarraDeEnergía.SetActive(true);
        }

        if (!Completo_Turbo && turboScript.CurrentEnergy < TurboGoal)
        {
            Completo_Turbo = true;
            Debug.Log("TUrbo Complete");
            turboScript.canUseTurbo = false;
        }
    }

    void SideAttack()
    {
        if (Completo_Turbo)
        {
            if (Input.GetKeyDown(KeyCode.A) && !A_Pressed)
            {
                A_Pressed = true;
                sideAttack++;
                Invoke("DisableLeft", 0.5f);
                
            }

            if (Input.GetKeyDown(KeyCode.D) && !D_Pressed)
            {
                D_Pressed = true;
                sideAttack++;
                Invoke("DisableRight", 0.5f);
            }
        }

        if(sideAttack >= 2 && dialogo == 3)
        {
            Completo_SideAttack = true;
            Debug.Log("SideAttack Complete");
        }
    }

    void EnergyCharge()
    {
        if (Completo_SideAttack)
        {
            if(turboScript.CurrentEnergy == 100)
            {
                Completo_RecargaEnergia = true;
                Debug.Log("EnergyCharge Complete");
            }
        }
    }

    void Final()
    {
        if (Completo_RecargaEnergia)
        {
            Debug.Log("Fin del tutorial");
        }
        
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(5);

        dialogo = 0;
    }

    void DisableLeft()
    {
        turboScript.CanAttackLeft = false;
    }

    void DisableRight()
    {
        turboScript.CanAttackRight = false;
    }
    
}
