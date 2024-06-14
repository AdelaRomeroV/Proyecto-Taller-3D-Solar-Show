using System.Collections.Generic;
using UnityEngine;

public class Generador_Tutorial : GeneradorDePista
{
    [Header("Zonas de pista")]
    GameObject PistaRecta;
    List<GameObject> Zonas = new List<GameObject>();
    List<GameObject> Dialogos = new List<GameObject>();

    [Header("Dependencias")]
    ControladorTutorial Controlador;
    
    private void Start()
    {
        Controlador = GameObject.Find("Controlador").GetComponent<ControladorTutorial>();
        Jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        PistaRecta = Controlador.PistaRecta;
        Zonas = Controlador.Zonas;
        Dialogos = Controlador.Dialogos;
    }

    private void Update()
    {
        ControladorDePiezas();
    }

    void ControladorDePiezas()
    {
        if (!Controlador.Completo_MovimientoBasico) //0
        {
            GenerarPista(PistaRecta);
        }
        else if (!Controlador.Completo_Derrape) //1
        {
            if (Controlador.dialogo == 0)
            {
                GenerarDialogo();
            }
            else
            {
                GenerarPista(Zonas[0]);
            }

        }
        else if (!Controlador.Completo_Turbo) //2
        {
            if (Controlador.dialogo == 1)
            {
                GenerarDialogo();
                Destroy(this);
            }
            else
            {
                GenerarPista(PistaRecta);
            }

        }else if (!Controlador.Completo_SideAttack) //3
        {
            if (Controlador.dialogo == 2)
            {
                Instantiate(Dialogos[2], Jugador.position, Jugador.rotation);
                Controlador.dialogo = 3;

            }
            else
            {
                GenerarPista(Zonas[4]);
            }
        }
        else if (!Controlador.Completo_RecargaEnergia) //4
        {
            if (Controlador.dialogo == 3)
            {
                GenerarDialogo();
            }
            else
            {
                GenerarPista(Zonas[3]);
            }
        }
        else
        {
            if(Controlador.dialogo == 4)
            {
                GenerarDialogo();
            }
            GenerarPista(PistaRecta);
        }
    }

    void GenerarDialogo()
    {
        Instantiate(PistaRecta, transform.position, transform.rotation);
        Instantiate(Dialogos[Controlador.dialogo], transform.position, transform.rotation);
        Controlador.dialogo++;
        Destroy(this);

    }
}
