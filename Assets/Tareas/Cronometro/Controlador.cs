using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controlador : MonoBehaviour
{
    [SerializeField] private float limiteTiempo;
    private float tiempoActual;
    private bool tiempoActivado = false;

    //public TemporizadorUI temporizador;
    // Start is called before the first frame update
    void Start()
    {
        ActivarTemporizador();

      //  temporizador= GameObject.Find("Temporizador").GetComponent<TemporizadorUI>();
        //temporizador.UpdateText();
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

       // temporizador.UpdateText(tiempoActual);

        if(tiempoActual <= 0)
        {

            CambiarTemporizador(false);
            SceneManager.LoadScene("Gameover");
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
