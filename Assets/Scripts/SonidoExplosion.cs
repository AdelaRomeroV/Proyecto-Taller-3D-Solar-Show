using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoExplosion : MonoBehaviour
{ 
    [SerializeField]private AudioSource audio;

    public void Sonido()
    {
        audio.Play();
    }
}
