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
    public List<GameObject> PistaRecta = new List<GameObject>();

    public List<GameObject> ZonaDeCurvas = new List<GameObject>();
    [NonSerialized] [Range(0,2)] public int ActualDrifZone = 0;

    public List<GameObject> ZonaDeHazards = new List<GameObject>();
    [NonSerialized][Range(0, 2)] public int ActualHazardZone = 0;

    public List<GameObject> Dialogos = new List<GameObject>();

    public GameObject ActualPiece;
    public bool GoingStraight;

    [Header("FASES")]
    [NonSerialized] public bool Completo_MovimientoBasico;
    [NonSerialized] public bool Completo_Derrape;
    [NonSerialized] public bool Completo_Turbo;
    [NonSerialized] public bool Completo_SideAttack;
    [NonSerialized] public bool Completo_RecargaEnergia;
    public bool PasarEscena;

    [Header("Scrips")]
    [SerializeField] GameObject Player;
    Mov movScript;
    Turbo turboScript;
    [SerializeField] GameObject BarraDeEnergía;

    [Header("Objetivos por fase")] 
    public int dialogo = -1;

    /*[NonSerialized]*/ public int movimientoBasico = 0; //Necesita llegar al valor de 3
    [NonSerialized] public bool W_pressed;
    [NonSerialized] public bool A_pressed;
    [NonSerialized] public bool D_pressed;

    /*[NonSerialized]*/ public int derrape = 0; //Necesita llegar a 5 esquinas derrapando para continuar

    [SerializeField] [Range(0, 100)] float TurboGoal;

    public int sideAttack = 0;
    public bool Right_Pressed;
    public bool Left_Pressed;

    [Header("Cambio de escena")]
    [SerializeField] string NombreDeEscena;

    //0: Derrape
    //1: Turbo
    //2: Side Attack
    //3: Recargar Energia
    //4: Final
    private void Start()
    {
        turboScript = Player.GetComponent<Turbo>();
        movScript = Player.GetComponent<Mov>();

        turboScript.enabled = false;
        BarraDeEnergía.SetActive(false);
    }

    private void Update()
    {
        FaseControl();


        if (turboScript.CurrentEnergy > 15)
        {
            turboScript.canUseTurbo = true;
            turboScript.CanAttackLeft = true;
            turboScript.CanAttackRight = true;
        }

        if (PasarEscena)
        {
            StartCoroutine(NextScene());
        }
    }

    void FaseControl()
    {
        if (!Completo_MovimientoBasico) MovimientoBasico();

        if (Completo_MovimientoBasico && !Completo_Derrape) Derrape();

        if (Completo_Derrape && !Completo_Turbo) Turbo();

        if (Completo_Turbo && !Completo_SideAttack) SideAttack();

        if (Completo_SideAttack && !Completo_RecargaEnergia) EnergyCharge();
    }

    void SpawnStraightPieces()
    {
        int piece = UnityEngine.Random.Range(0, PistaRecta.Count);
        ActualPiece = PistaRecta[piece];
    }

    void MovimientoBasico()
    {
        if(movScript.enabled == true)
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
        }

        if(movimientoBasico == 3 && dialogo == 0)
        {
            Completo_MovimientoBasico = true;
        }

        SpawnStraightPieces();
    }

    void Derrape()
    {
        if (derrape == 5)
        { 
            Completo_Derrape = true;
        }

        if (GoingStraight)
        {
            SpawnStraightPieces();
        }
        else
        {
            ActualPiece = ZonaDeCurvas[ActualDrifZone];

            if(ActualDrifZone == 0) ActualDrifZone = 1;
            else if (ActualDrifZone == 1) ActualDrifZone = 0;
        }
    }

    void Turbo()
    {
        if (turboScript.CurrentEnergy < TurboGoal)
        {
            Completo_Turbo = true;
            turboScript.canUseTurbo = false;
        }

        SpawnStraightPieces();
    }

    void SideAttack()
    {
        if (Completo_Turbo)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !Left_Pressed)
            {
                Left_Pressed = true;
                sideAttack++;
                Invoke("DisableLeft", 0.5f);
                
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && !Right_Pressed)
            {
                Right_Pressed = true;
                sideAttack++;
                Invoke("DisableRight", 0.5f);
            }
        }

        if(sideAttack >= 2 && dialogo == 3)
        {
            Completo_SideAttack = true;
        }

        SpawnStraightPieces();
    }

    void EnergyCharge()
    {
        if(turboScript.CurrentEnergy == 100)
        {
            Completo_RecargaEnergia = true;
        }


        if (GoingStraight)
        {
            SpawnStraightPieces();
        }
        else
        {
            ActualPiece = ZonaDeHazards[ActualHazardZone];

            if (ActualHazardZone == 0) ActualHazardZone = 1;
            else if (ActualHazardZone == 1) ActualHazardZone = 0;
        }
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
