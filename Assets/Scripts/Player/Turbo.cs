using System;
using System.Collections.Generic;
using UnityEngine;
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

    [Header("SideKick Variables")]
    [SerializeField] GameObject RightBox;
    [SerializeField] GameObject LeftBox;
    public bool isKicking;

    public bool RightAtacking;
    public bool LeftAttaking;
    [NonSerialized] public bool CanAttackRight = true;
    [NonSerialized] public bool CanAttackLeft = true;

    [Header("Audio")]
    public AudioClip audioTurbo;
    public AudioClip audioExplosion;
    public AudioClip audioAtaque;
    private AudioSource audioSource;

    [Header("Queues")]
    Queue<KeyCode> Turbo_Buffer = new Queue<KeyCode>();

    private void Start()
    {
        LifeControl = GetComponent<ControlDeVida>();
        mov = GetComponent<Mov>();

        if(EnergyBar  != null)
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
        SideKick();
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

            //Instantiate(efecto, transform.position);
            Destroy(gameObject);
        }
        if (CurrentEnergy >= 100) { CurrentEnergy = 100; }
    }

    public void GestionarEnergia(float a)
    {
        CurrentEnergy = CurrentEnergy - a;
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
            }
        }
        else if (LifeControl != null && LifeControl.GetDamage)
        {
            CurrentEnergy -= 0.08f;
        }
    }

    public void SideKick()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isKicking && CurrentEnergy > 20 && CanAttackLeft)
        {
            audioSource.clip = audioAtaque; 
            audioSource.Play();

            RightBox.SetActive(true);

            isKicking = true;
            CurrentEnergy -= 10;
            RightAtacking = true;

            Invoke("DisableKicking", 0.55f);
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isKicking && CurrentEnergy > 20 && CanAttackRight)
        {
            audioSource.clip = audioAtaque; 
            audioSource.Play();

            LeftBox.SetActive(true);

            isKicking = true;
            CurrentEnergy -= 10;
            LeftAttaking = true;

            Invoke("DisableKicking", 0.55f);
        }
    }

    public void DisableKicking()
    {
        isKicking = false;
        RightBox.SetActive(false);
        LeftBox.SetActive(false);
        LeftAttaking = false;
        RightAtacking = false;
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
            if (turbo != null) { CurrentEnergy += turbo.count; }
        }
    }

    void InputBuffer()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Turbo_Buffer.Enqueue(KeyCode.W);
            Invoke("TurboDequeue", 0.5f);
        }

        if(Turbo_Buffer.Count == 2 && !mov.Drifiting && !LifeControl.GetDamage)
        {
            TurboActive = true;
        }
    }

    void TurboDequeue()
    {
        Turbo_Buffer.Dequeue();
    }
}
