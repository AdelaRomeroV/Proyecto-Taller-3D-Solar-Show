using System;
using UnityEngine;
using UnityEngine.UI;

public class Turbo : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] Image EnergyBar;
    ControlDeVida LifeControl;

    
    [Header("Enegy Bar")]
    public bool TurboActive = false;
    [SerializeField] [Range(0,100)] public float CurrentEnergy = 100;
    public bool Charging;

    [Header("SideKick Variables")]
    [SerializeField] GameObject RightBox;
    [SerializeField] GameObject LeftBox;
    public bool isKicking;
    public bool RightAtack;
    public bool LeftAtack;
    public bool AttackOn;

    private void Start()
    {
        LifeControl = GetComponent<ControlDeVida>();

        if(EnergyBar  != null)
        {
            EnergyBar.fillAmount = CurrentEnergy / 100;
        }
    }

    private void Update()
    {
        BarSmoothness();
        UsingTurbo();
        ReloadBar();
        SideKick();
        Energia();
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
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TurboActive = true;
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                GestionarEnergia(0.25f);
                if (CurrentEnergy <= 0)
                {
                    CurrentEnergy = 0;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            TurboActive = false;
        }
    }

    void ReloadBar() //Delay para recargar la barra
    {
        if (!TurboActive && CurrentEnergy < 100 && !LifeControl.GetDamage) 
        {
            if (Charging)
            {
                CurrentEnergy += 0.15f;
            }
        }
        else if (LifeControl.GetDamage)
        {
            CurrentEnergy -= 0.08f;
        }
    }

    public void SideKick()
    {
        if(AttackOn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !isKicking && CurrentEnergy > 20)
            {
                RightBox.SetActive(true);
                isKicking = true;
                CurrentEnergy -= 10;
                RightAtack = true;

                Invoke("DisableKicking", 0.55f);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1) && !isKicking && CurrentEnergy > 20)
            {
                LeftBox.SetActive(true);
                isKicking = true;
                CurrentEnergy -= 10;
                LeftAtack = true;

                Invoke("DisableKicking", 0.55f);
            }
        }
    }

    public void DisableKicking()
    {
        isKicking = false;
        RightBox.SetActive(false);
        LeftBox.SetActive(false);
        LeftAtack = false;
        RightAtack = false;
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
}
