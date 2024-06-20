using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorTutorial : MonoBehaviour
{
    //----------------------------------------------------------
    //DISCLAIMER
    //Este Script seguira la secuencia descrita en el LDD
    //----------------------------------------------------------

    [Header("Zonas de pista")]
    public List<GameObject> PistaRecta;
    public List<GameObject> Zonas = new List<GameObject>();
    public List<GameObject> Dialogos = new List<GameObject>();

    [Header("FASES")]
    [NonSerialized] public bool Completo_MovimientoBasico;
    [NonSerialized] public bool Completo_Derrape;
    [NonSerialized] public bool Completo_Turbo;
    [NonSerialized] public bool Completo_SideAttack;
    [NonSerialized] public bool Completo_RecargaEnergia;
    public bool PasarEscena;

    [Header("Scrips")]
    [SerializeField] Turbo turboScript;
    [SerializeField] GameObject BarraDeEnergía;

    [Header("Objetivos por fase")] 
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

    [Header("Cambio de escena")]
    [SerializeField] string NombreDeEscena;

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

        if (PasarEscena)
        {
            StartCoroutine(NextScene());
        }
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
        }
    }

    void Derrape()
    {
        if (derrape == 5)
        {
            if (!Completo_Derrape)

            Completo_Derrape = true;
        }
    }

    void Turbo()
    {
        if (!Completo_Turbo && turboScript.CurrentEnergy < TurboGoal && !Completo_SideAttack)
        {
            Completo_Turbo = true;
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
        }
    }

    void EnergyCharge()
    {
        if (Completo_SideAttack)
        {
            if(turboScript.CurrentEnergy == 100)
            {
                Completo_RecargaEnergia = true;
            }

            if (turboScript.CurrentEnergy > 30)
            {
                turboScript.canUseTurbo = true;
                turboScript.CanAttackLeft = true;
                turboScript.CanAttackRight = true;
            }
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

    public void ChangeScene()
    {
        StartCoroutine(NextScene());
    }
    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(NombreDeEscena);
    }
    
}
