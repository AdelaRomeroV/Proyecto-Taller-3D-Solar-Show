using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDeVida : MonoBehaviour
{
    public bool recibioDaño = false;

    private void FixedUpdate()
    {
        recibioDaño = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Pared"))
        {
            recibioDaño = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AtaqueEnemigo"))
        {
            recibioDaño = true;
        }
    }
}
