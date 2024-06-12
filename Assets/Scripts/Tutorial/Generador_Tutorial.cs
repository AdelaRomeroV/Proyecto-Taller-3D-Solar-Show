using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
                Debug.Log("DERRAPA EN LAS ESQUINAS 5 VECES");
                GenerarDialogo(0, 1);
                Destroy(this);
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
                Debug.Log("USA EL TURBO HASTA PASAR A LA SIGUIENTE FASE");
                GenerarDialogo(1, 2);
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
                Debug.Log("USA EL SIDE ATTACK HACIA AMBOS LADOS");
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
                Debug.Log("RECARGA ENERGIA USANDO LOS BUMPERS");
                GenerarDialogo(3, 4);
                Destroy(this);
            }
            else
            {
                GenerarPista(Zonas[3]);
            }
        }
        else
        {
            Debug.Log("Tutorial Terminado");
            GenerarPista(PistaRecta);
        }
    }

    void GenerarDialogo(int DialogoIndex, int next)
    {
        Instantiate(Dialogos[DialogoIndex], transform.position, transform.rotation);
        Controlador.dialogo = next;

    }
}
