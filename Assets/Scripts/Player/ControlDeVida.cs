using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDeVida : MonoBehaviour
{
    public bool GetDamage = false;
    private Turbo turboPlayer;
    public AudioClip audioDamage;
    private AudioSource audioSource;

    private void Awake()
    {
        turboPlayer = GetComponent<Turbo>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (GetDamage == true && audioSource != null)
        {
            audioSource.clip = audioDamage;
            audioSource.Play();
            audioSource.pitch = 3;
            Invoke("ApagarAudio", 0.5f);
        }
        GetDamage = false;
    }

    void ApagarAudio()
    {
        audioSource.Pause();
        audioSource.pitch = -0.5f;
        audioSource.clip = null; // Esto no se si es necesario o sobra, pero el metodo era por el audio me parecio un poco largo //aqui se ponia el clip del audio y se reproduce en el unity pero te complicaste la vida no lo sabes :v
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Pared"))
        {
            turboPlayer.GestionarEnergia(20);
            GetDamage = true;
            turboPlayer.TurboActive = false;
        }
        if (collision.gameObject.CompareTag("Hazards"))
        {
            CountPeligro turbo = collision.gameObject.GetComponent<CountPeligro>();
            if(turbo != null) { turboPlayer.GestionarEnergia(turbo.count); }
            GetDamage = true;
            turboPlayer.TurboActive = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AtaqueEnemigo"))
        {
            turboPlayer.GestionarEnergia(2);
            GetDamage = true;
            turboPlayer.TurboActive = false;
        }
        if (other.CompareTag("Hazards"))
        {
            CountPeligro turbo = other.gameObject.GetComponent<CountPeligro>();
            if (turbo != null) { turboPlayer.GestionarEnergia(turbo.count); }
            GetDamage = true;
            turboPlayer.TurboActive = false;
        }
    }
}
