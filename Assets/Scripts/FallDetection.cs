using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Transform checkpoint = ListaDeCheckpoints.Instance.GetLastCheckpoint().transform;
            other.transform.position = checkpoint.position- checkpoint.forward*10;

            other.transform.forward=checkpoint.forward;

            other.GetComponent<ControlDeVida>().GetDamage = true;
            other.GetComponent<Turbo>().GestionarEnergia(10);
        }
    }
}
