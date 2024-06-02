using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeneradorDePista : MonoBehaviour
{
    public Transform Jugador;

    public void GenerarPista(GameObject Pista)
    {
        if (Vector3.Distance(Jugador.position, transform.position) <= 300)
        {
            Instantiate(Pista, transform.position, transform.rotation);
            Destroy(this);
        }
    }

    
}
