using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlVolumen : MonoBehaviour
{
    public Slider slider;
    public int sliderValue;

    void Start()
    {
        // Se obtiene el valor guardado del volumen del audio de PlayerPrefs.

        // Si no hay un valor guardado, se usa 0.5f como valor por defecto

        //El PlayerPrefs se utiliza para almacenar y recuperar datos

        slider.value = PlayerPrefs.GetFloat("VolumenAudio", 0.5f);

        // Se establece el volumen del AudioListener al valor del control deslizante.
        AudioListener.volume = slider.value;
    }

    public void ChangeSlider(int val)
    {
        sliderValue = val;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
     
        // AudioListener captura todos los sonidos emitidos por los AudioSource en la escena y los envía al sistema de audio del dispositivo del usuario.
        AudioListener.volume = slider.value;
    }
}
