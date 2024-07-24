using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Turbo : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] Image EnergyBar;
    ControlDeVida LifeControl;
    Mov mov;
    
    [Header("Enegy Bar")]
    public bool TurboActive = false;
    [SerializeField] [Range(0,100)] public float CurrentEnergy = 100;
    public bool Charging;
    [NonSerialized] public bool canUseTurbo = true;

    [Header("Audio")]
    public AudioClip audioTurbo;
    public AudioClip audioExplosion;
    public AudioClip audioAtaque;
    private AudioSource audioSource;

    [Header("Queues")]
    Queue<KeyCode> Turbo_Buffer = new Queue<KeyCode>();

    [SerializeField] Renderer cocheRenderer;
    private bool activedParpate = true;
    PlayerAnimations animated;

    [SerializeField] GameObject metodo;

    private void Start()
    {
        LifeControl = GetComponent<ControlDeVida>();
        mov = GetComponent<Mov>();
        animated = GetComponent<PlayerAnimations>();

        if (EnergyBar  != null)
        {
            EnergyBar.fillAmount = CurrentEnergy / 100;
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        BarSmoothness();
        UsingTurbo();
        ReloadBar();
        Energia();
        InputBuffer();
    }

    private void BarSmoothness()
    {
        if (EnergyBar != null)
            EnergyBar.fillAmount = Mathf.Lerp(EnergyBar.fillAmount, CurrentEnergy / 100, 5 * Time.deltaTime);
    }

    private void Energia()
    {
        if (CurrentEnergy <= 0) 
        {
            if(EnergyBar != null) { EnergyBar.fillAmount = 0; }

            if (!audioSource.isPlaying)
            {
                audioSource.clip = audioExplosion;
                audioSource.Play();
            }
            CurrentEnergy = 0;

            animated.Dead();
            Instantiate(metodo);
            Destroy(gameObject);
        }
        if (CurrentEnergy >= 100) { CurrentEnergy = 100; }
    }

    public void GestionarEnergia(float a)
    {
        CurrentEnergy = CurrentEnergy - a;
        if (CurrentEnergy < 25 && activedParpate) { StartCoroutine(Parpadeo(Color.red, 0.5f)); }
        else if (CurrentEnergy > 25) { activedParpate = true; }
    }

    void UsingTurbo()
    {
        if (canUseTurbo)
        {
            if (TurboActive)
            {
                audioSource.clip = audioTurbo;
                audioSource.Play();

                GestionarEnergia(0.25f);
                if (CurrentEnergy <= 0)
                {
                    CurrentEnergy = 0;
                }
            }
            /*else if (Input.GetKey(KeyCode.W) && TurboActive)*/
            {
                //GestionarEnergia(0.25f);
                //if (CurrentEnergy <= 0)
                //{
                //    CurrentEnergy = 0;
                //}
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || !canUseTurbo)
        {

            TurboActive = false;

        }
    }

    void ReloadBar() //Delay para recargar la barra
    {
        if (!TurboActive && CurrentEnergy < 100 && LifeControl != null && !LifeControl.GetDamage) 
        {
            if (Charging)
            {
                CurrentEnergy += 0.15f;
                StartCoroutine(Recarga(Color.white, 0.5f));
            }
        }
        else if (LifeControl != null && LifeControl.GetDamage)
        {
            CurrentEnergy -= 0.08f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Peligro"))
        {
            Charging = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Peligro"))
        {
            Charging = false;
        }
        if (other.CompareTag("HazardsPeligro"))
        {
            CountPeligro turbo = other.GetComponent<CountPeligro>();
            if (turbo != null) 
            { 
                CurrentEnergy += turbo.count;
                StartCoroutine(Recarga(Color.white, 0.5f));
            }
        }
    }

    void InputBuffer()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Turbo_Buffer.Enqueue(KeyCode.W);
            Invoke("TurboDequeue", 0.5f);
        }

        if(LifeControl != null)
        {
            if(Turbo_Buffer.Count >= 2 && !mov.Drifiting && !LifeControl.GetDamage)
            {
                TurboActive = true;
                Invoke("DeactiveTurbo", 1f);
            }
        }
        else
        {
            if (Turbo_Buffer.Count >= 2 && !mov.Drifiting)
            {
                TurboActive = true;
                Invoke("DeactiveTurbo", 1f);
            }
        }
    }

    void TurboDequeue()
    {
        if(Turbo_Buffer.Count > 0)
        Turbo_Buffer.Dequeue();
    }

    void DeactiveTurbo()
    {
        if(Turbo_Buffer.Count > 0) Turbo_Buffer.Clear();

        TurboActive = false;
    }
    IEnumerator Parpadeo(Color emissionColor, float time)
    {
        cocheRenderer.material.SetColor("_EmissionColor", emissionColor);
        cocheRenderer.material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(time);
        cocheRenderer.material.DisableKeyword("_EMISSION");
        yield return new WaitForSeconds(time);
        cocheRenderer.material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(time);
        cocheRenderer.material.DisableKeyword("_EMISSION");
        activedParpate = false;
    }

    IEnumerator Recarga(Color emissionColor, float time)
    {
        cocheRenderer.material.SetColor("_EmissionColor", emissionColor);
        cocheRenderer.material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(time);
        cocheRenderer.material.DisableKeyword("_EMISSION");
        yield return new WaitForSeconds(time);
        activedParpate = false;
    }

}
