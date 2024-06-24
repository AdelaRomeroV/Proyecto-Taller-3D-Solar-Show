using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorTutorial : MonoBehaviour
{
    [Header("Zonas de pista")]
    public GameObject GoalPiece;

    public List<GameObject> PistaRecta = new List<GameObject>();

    public List<GameObject> ZonaDeCurvas = new List<GameObject>();
    [Range(0, 2)] public int ActualDrifZone = 0;

    public GameObject Rampa;
    public List<GameObject> ZonaDeTurbo = new List<GameObject>();
    [Range(0, 2)] public int ActualTurboZone = 0;

    public GameObject ZonaDeHazards;

    public List<GameObject> Dialogos = new List<GameObject>();

    public GameObject ActualPiece;
    public bool GoingStraight;

    [Header("FASES")]
    public bool Completo_MovimientoBasico;
    public bool Completo_Derrape;
    public bool Completo_Turbo;
    public bool Completo_SideAttack;
    public bool Completo_RecargaEnergia;
    public bool PasarEscena;

    [Header("Scrips")]
    [SerializeField] GameObject Player;
    Mov movScript;
    Turbo turboScript;
    [SerializeField] GameObject BarraDeEnergía;

    [Header("Objetivos por fase")]
    public int dialogo = 0;

    /*[NonSerialized]*/
    public int movimientoBasico = 0; //Necesita llegar al valor de 3
    [NonSerialized] public bool W_pressed;
    [NonSerialized] public bool A_pressed;
    [NonSerialized] public bool D_pressed;

    /*[NonSerialized]*/
    public int derrape = 0; //Necesita llegar a 5 esquinas derrapando para continuar

    [SerializeField][Range(0, 100)] float TurboGoal;

    public int sideAttack = 0;
    public bool Right_Pressed;
    public bool Left_Pressed;

    [Header("Cambio de escena")]
    [SerializeField] string NombreDeEscena;

    //0: Derrape
    //1: Side Attack
    //2: Turbo
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

        if (Completo_Turbo && Completo_SideAttack)
        {
            if (!Completo_Turbo)
            {
                turboScript.CurrentEnergy += 100 - turboScript.CurrentEnergy;
            }
            else
            {
                if (turboScript.CurrentEnergy > 15)
                {
                    turboScript.canUseTurbo = true;
                    turboScript.CanAttackLeft = true;
                    turboScript.CanAttackRight = true;
                }
                else
                {
                    turboScript.canUseTurbo = false;
                    turboScript.CanAttackLeft = false;
                    turboScript.CanAttackRight = false;
                }
            }
        }

        if (PasarEscena)
        {
            StartCoroutine(NextScene());
        }
    }

    void FaseControl()
    {
        if (!Completo_MovimientoBasico) MovimientoBasico();

        else if (Completo_MovimientoBasico && !Completo_Derrape) Derrape();

        else if (Completo_Derrape && !Completo_SideAttack) SideAttack();

        else if (Completo_SideAttack && !Completo_Turbo) Turbo();

        else if (Completo_SideAttack && !Completo_RecargaEnergia) EnergyCharge(); ;
    }

    void SpawnStraightPieces()
    {
        int piece = UnityEngine.Random.Range(0, PistaRecta.Count);
        ActualPiece = PistaRecta[piece];
    }
    void SpawnDriftZone()
    {
        if (GoingStraight)
        {
            SpawnStraightPieces();
        }
        else
        {
            Generador_Tutorial t = GameObject.Find("SpawnPoint").GetComponent<Generador_Tutorial>();
            t.DriftPieces();
        }
    }

    void SpawnTurboZone()
    {
        if (GoingStraight)
        {
            SpawnStraightPieces();
        }
        else
        {
            Generador_Tutorial t = GameObject.Find("SpawnPoint").GetComponent<Generador_Tutorial>();
            t.TurboPieces();
        }
    }

    void SpawnHazardZone()
    {
        if (GoingStraight)
        {
            SpawnStraightPieces();
        }
        else
        {
            ActualPiece = ZonaDeHazards;
        }
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

        ActualPiece = null;
    }

    void Derrape()
    {
        if (derrape == 5)
        { 
            Completo_Derrape = true;
        }
        
        SpawnDriftZone();
    }

    void Turbo()
    {
        if (turboScript.CurrentEnergy < TurboGoal)
        {
            Completo_Turbo = true;
            turboScript.canUseTurbo = false;
        }

        SpawnTurboZone();
    }

    void SideAttack()
    {
        turboScript.canUseTurbo = false;


        if (turboScript.RightAtacking && !Left_Pressed)
        {
            Left_Pressed = true;
            sideAttack++;
            Invoke("DisableLeft", 0.5f);
                
        }

        if (turboScript.LeftAttaking && !Right_Pressed)
        {
            Right_Pressed = true;
            sideAttack++;
            Invoke("DisableRight", 0.5f);
        }


        if(sideAttack >= 2 && dialogo == 2)
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

        SpawnHazardZone();
    }

    public void ActiveSA()
    {
        turboScript.CanAttackLeft = true;
        turboScript.CanAttackRight = true;
    }

    public void DeactiveSA()
    {
        turboScript.CanAttackLeft = false;
        turboScript.CanAttackRight = false;
    }
    void DisableLeft()
    {
        turboScript.CanAttackLeft = false;
    }

    void DisableRight()
    {
        turboScript.CanAttackRight = false;
    }

    public void GoingStraightFalse()
    {
        GoingStraight = false;
    }

    public void ActiveTurbo()
    {
        turboScript.canUseTurbo = true;
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
