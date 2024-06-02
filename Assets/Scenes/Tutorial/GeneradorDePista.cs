using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeneradorDePista : MonoBehaviour
{
    [Header ("Zonas de pista")]
    [SerializeField] GameObject PistaRecta_ConSpawner;
    [SerializeField] GameObject Zona_de_Derrape;
    [SerializeField] GameObject Zona_de_Hazard;

    [Header ("Dependencias")]
    ControladorTutorial Controlador;
    Transform Jugador;

    private void Start()
    {
        Controlador = GameObject.Find("Controlador").GetComponent<ControladorTutorial>();
        Jugador = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (!Controlador.Completo_MovimientoBasico)
        {
            GenerarPista(PistaRecta_ConSpawner);
        }
        else if (!Controlador.Completo_Derrape)
        {
            GenerarPista(Zona_de_Derrape);
        }
    }

    void GenerarPista(GameObject Pista)
    {
        if (Vector3.Distance(Jugador.position, transform.position) <= 300)
        {
            Instantiate(Pista, transform.position, transform.rotation);
            Destroy(this);
        }
    }
}
