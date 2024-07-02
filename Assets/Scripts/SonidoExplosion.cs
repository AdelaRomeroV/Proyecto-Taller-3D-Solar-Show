using UnityEngine;

public class SonidoExplosion : MonoBehaviour
{ 
    [SerializeField]private AudioSource audioSource;

    private void Awake()
    {
        Sonido();
    }
    public void Sonido()
    {
        audioSource.Play();
    }
}
