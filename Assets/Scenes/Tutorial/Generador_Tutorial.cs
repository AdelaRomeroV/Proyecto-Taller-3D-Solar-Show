using UnityEngine;

public class Generador_Tutorial : GeneradorDePista
{
    [Header("Zonas de pista")]
    [SerializeField] GameObject PistaRecta;
    [SerializeField] GameObject Zona_de_Derrape;
    [SerializeField] GameObject Zona_de_Hazard;

    [Header("Colliders con dialogo")]
    [SerializeField] GameObject Dialogo_Derrape;
    [SerializeField] GameObject Dialogo_Turbo;

    [Header("Dependencias")]
    ControladorTutorial Controlador;
    
    private void Start()
    {
        Controlador = GameObject.Find("Controlador").GetComponent<ControladorTutorial>();
        Jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        ControladorDePiezas();
    }

    void ControladorDePiezas()
    {
        if (!Controlador.Completo_MovimientoBasico)
        {
            GenerarPista(PistaRecta);
        }
        else if (!Controlador.Completo_Derrape) //Completo_MovimientoBasico = true
        {
            if (Controlador.dialogo == 1)
            {
                Instantiate(Dialogo_Derrape, Jugador.position, Jugador.rotation);
                Controlador.dialogo = 2;
            }

            GenerarPista(Zona_de_Derrape);
        }
        else if (!Controlador.Completo_Turbo)
        {
            if (Controlador.dialogo == 2)
            {
                Instantiate(Dialogo_Turbo, Jugador.position, Jugador.rotation);
                Controlador.dialogo = 3;
            }

            GenerarPista(PistaRecta);
        }
    }
}
