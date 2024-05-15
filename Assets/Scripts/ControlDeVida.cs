using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDeVida : MonoBehaviour
{
    public bool recibioDaño = false;
    private Turbo turboPlayer;

    private void Awake()
    {
        turboPlayer = GetComponent<Turbo>();
    }

    private void FixedUpdate()
    {
        recibioDaño = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Pared"))
        {
            turboPlayer.GestionarEnergia(20);
            recibioDaño = true;
        }
        if (collision.gameObject.CompareTag("Hazards"))
        {
            turboPlayer.GestionarEnergia(10);
            recibioDaño = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AtaqueEnemigo"))
        {
            turboPlayer.GestionarEnergia(2);
            recibioDaño = true;
        }
    }
}
