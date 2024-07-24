using UnityEngine;

public class Generador_Tutorial : GeneradorDePista
{
    [Header("Dependencias")]
    ControladorTutorial Controlador;

    private void Start()
    {
        Controlador = GameObject.Find("Controlador").GetComponent<ControladorTutorial>();
        Jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if(Controlador.ActualPiece != null && !Controlador.Completo_RecargaEnergia)
        {
            GenerarPista(Controlador.ActualPiece);
        }
        else if (Controlador.Completo_RecargaEnergia)
        {
            GenerarPista(Controlador.GoalPiece);
        }
        DialogueControl();
    }

    void DialogueControl()
    {
        if (Controlador.Completo_MovimientoBasico && Controlador.dialogo == 0) //Dialogo Derrape (0)
        {
            Controlador.GoingStraight = true;
            Controlador.dialogo++;
        }
        else if (Controlador.Completo_Derrape && Controlador.dialogo == 1) //Dialogo SideAttack (1)
        {
            DriftDialogue();

        }
        else if (Controlador.Completo_Turbo && Controlador.dialogo == 2) //Dialogo Turbo (2)
        {
            GenerateDialogue();

        }
        else if (Controlador.Completo_RecargaEnergia && Controlador.dialogo == 3) //Dialogo Energia (3)
        {
            DriftDialogue();

        }
    }

    void GenerateDialogue()
    {
        Instantiate(Controlador.Dialogos[Controlador.dialogo], Jugador.transform.position, Jugador.transform.rotation);
        Controlador.dialogo++;
        Controlador.GoingStraight = true;

    }

    void DriftDialogue()
    {
        Instantiate(Controlador.PistaRecta[1], transform.position, transform.rotation);
        Instantiate(Controlador.Dialogos[Controlador.dialogo], transform.position, transform.rotation);
        Controlador.dialogo++;
        Controlador.GoingStraight = true;
        Destroy(gameObject);
    }

    public void DriftPieces()
    {
        //Para alternar entre objetos de un arreglo
        if (Vector3.Distance(Jugador.position, transform.position) <= 220)
        {
            Controlador.ActualPiece = Controlador.ZonaDeCurvas[Controlador.ActualDrifZone];
            Controlador.ActualDrifZone++;

            if (Controlador.ActualDrifZone >= Controlador.ZonaDeCurvas.Count)
            {
                Controlador.ActualDrifZone = 0;
            }
        }
    }
    public void TurboPieces()
    {
        //Para alternar entre objetos de un arreglo
        if (Vector3.Distance(Jugador.position, transform.position) <= 220)
        {
            if(Controlador.Rampa != null)
            {
                Controlador.ActualPiece = Controlador.Rampa;
                Controlador.Rampa = null;
            }
            else
            {
                Controlador.ActualPiece = Controlador.ZonaDeTurbo[Controlador.ActualTurboZone];
                Controlador.ActualTurboZone++;

                if (Controlador.ActualTurboZone >= Controlador.ZonaDeTurbo.Count)
                {
                    Controlador.ActualTurboZone = 0;
                }
            }
        }
    }
    
}
