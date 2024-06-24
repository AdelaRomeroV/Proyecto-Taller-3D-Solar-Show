using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoExplosion : MonoBehaviour
{ 
    [SerializeField]private AudioSource audioSource;

    public void Sonido()
    {
        audioSource.Play();
    }
}
