using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciarTemporizador : MonoBehaviour
{
    [SerializeField] private Controlador controlador;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            controlador.ActivarTemporizador();
        }
    }
}
