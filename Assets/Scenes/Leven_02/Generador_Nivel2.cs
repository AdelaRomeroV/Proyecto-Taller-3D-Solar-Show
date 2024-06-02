using System.Collections.Generic;
using UnityEngine;

public class Generador_Nivel2 : GeneradorDePista
{
    [SerializeField] GameObject LineaDeMeta;
    [SerializeField] List<GameObject> Pistas = new List<GameObject>();

    Controlador_Nivel2 controlador;

    private void Start()
    {
        controlador = GameObject.Find("Controlador").GetComponent<Controlador_Nivel2>();
        Jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if(controlador.PistasGeneradas < controlador.MaxPistas)
        {
            int random = Random.Range(0, Pistas.Count);
            Instantiate(Pistas[random], transform.position, transform.rotation);
            controlador.PistasGeneradas++;
        } 
        else if (controlador.PistasGeneradas == controlador.MaxPistas)
        {
            Instantiate(LineaDeMeta, transform.position, transform.rotation);
            controlador.PistasGeneradas++;
        }

        Destroy(this);
    }


}
