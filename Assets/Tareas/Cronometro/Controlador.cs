using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controlador : MonoBehaviour
{
    [SerializeField] private float limiteTiempo;
    private float tiempoActual;
    private bool tiempoActivado = false;

    public TemporizadorUI temporizador;
    void Start()
    {
        ActivarTemporizador();

        temporizador= GameObject.Find("Temporizador").GetComponent<TemporizadorUI>();
        temporizador.UpdateText(limiteTiempo);
    }

    // Update is called once per frame
    void Update()
    {
        if(tiempoActivado)
        {
            CambiarContador();
        }
    }


    private void CambiarContador()
    {
        tiempoActual-= Time.deltaTime;

        temporizador.UpdateText(tiempoActual);


        if(tiempoActual <= 0)
        {
            //other.GetComponent<ControlDeVida>().GetDamage = true;
            //other.GetComponent<Turbo>().GestionarEnergia(100);

            CambiarTemporizador(false);
            SceneManager.LoadScene("Game Over");
        }
    }
    private void CambiarTemporizador(bool estado)
    {
        tiempoActivado= estado;
    }
    public void ActivarTemporizador()
    {
        tiempoActual = limiteTiempo;
        CambiarTemporizador(true);
    }
    public void DesactivarTemporizador()
    {
        CambiarTemporizador(false);
    }
}
