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
        if(Controlador.ActualPiece != null)
        GenerarPista(Controlador.ActualPiece);

        DialogueControl();
    }

    void DialogueControl()
    {
        if (Controlador.Completo_MovimientoBasico && Controlador.dialogo == 0) //Dialogo Derrape (0)
        {
            GenerateDialogue();

        }
        else if (Controlador.Completo_Derrape && Controlador.dialogo == 1) //Dialogo Turbo (1)
        {
            DriftDialogue();

        }
        else if (Controlador.Completo_Turbo && Controlador.dialogo == 2) //Dialogo SD (2)
        {
            GenerateDialogue();

        }
        else if (Controlador.Completo_SideAttack && Controlador.dialogo == 3) //Dialogo Energia (3)
        {
            GenerateDialogue();

        }
        else if (Controlador.Completo_RecargaEnergia && Controlador.dialogo == 4) //Dialogo Final (4)
        {
            GenerateDialogue();

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
        Destroy(this);
    }
}
