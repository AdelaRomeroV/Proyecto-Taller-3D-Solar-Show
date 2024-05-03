using System;
using UnityEngine;
using UnityEngine.UI;

public class Turbo : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] Image BarraDeEnergia;
    ControlDeVida controlVida;

    
    [Header("Enegy Bar")]
    public bool TurboActivo = false;
    [SerializeField] [Range(0,100)] float EnergiaActual = 100;
    public bool reload;

    [Header("SideKick Variables")]
    [SerializeField] GameObject RightBox;
    [SerializeField] GameObject LeftBox;
    public bool isKicking;


    private void Start()
    {
        controlVida = GetComponent<ControlDeVida>();

        BarraDeEnergia.fillAmount = EnergiaActual/100;
    }

    private void Update()
    {
        //Efecto de suavizado para la barra
        BarraDeEnergia.fillAmount = Mathf.Lerp(BarraDeEnergia.fillAmount, EnergiaActual/100, 2 * Time.deltaTime);


        UsingTurbo();
        ReloadBar();
        GestionarEnergia();
        SideKick();
    }

    private void GestionarEnergia()
    {
        if (controlVida.recibioDaño)
        {
            EnergiaActual = EnergiaActual - 5;
        }
    }

    void UsingTurbo()
    {
        if (Input.GetKey(KeyCode.Space) && EnergiaActual > 0)
        {
            EnergiaActual -= 0.3f;
            TurboActivo = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space) || EnergiaActual <= 0)
        {
            TurboActivo = false;
        }
    }

    void ReloadBar() //Delay para recargar la barra
    {
        if (!TurboActivo && EnergiaActual < 100 && !controlVida.recibioDaño) 
        {
            if (reload)
            {
                EnergiaActual += 0.15f;
            }
        }
        else if (controlVida.recibioDaño)
        {
            EnergiaActual -= 0.08f;
        }
    }

    public void SideKick()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !isKicking && EnergiaActual > 20)
        {
            RightBox.SetActive(true);
            isKicking = true;
            EnergiaActual -= 20;

            Invoke("DisableKicking", 0.25f);
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1) && !isKicking && EnergiaActual > 20)
        {
            LeftBox.SetActive(true);
            isKicking = true;
            EnergiaActual -= 20;

            Invoke("DisableKicking", 0.25f);
        }
    }

    public void DisableKicking()
    {
        isKicking = false;
        RightBox.SetActive(false);
        LeftBox.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Peligro"))
        {
            reload = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Peligro"))
        {
            reload = false;
        }
    }
}
