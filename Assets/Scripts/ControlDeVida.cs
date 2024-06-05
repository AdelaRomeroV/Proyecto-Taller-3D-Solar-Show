using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDeVida : MonoBehaviour
{
    public bool GetDamage = false;
    private Turbo turboPlayer;

    private void Awake()
    {
        turboPlayer = GetComponent<Turbo>();
    }

    private void FixedUpdate()
    {
        GetDamage = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Pared"))
        {
            turboPlayer.GestionarEnergia(20);
            GetDamage = true;
        }
        if (collision.gameObject.CompareTag("Hazards"))
        {
            CountPeligro turbo = collision.gameObject.GetComponent<CountPeligro>();
            if(turbo != null) { turboPlayer.GestionarEnergia(turbo.count); }
            GetDamage = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AtaqueEnemigo"))
        {
            turboPlayer.GestionarEnergia(2);
            GetDamage = true;
        }
    }
}
