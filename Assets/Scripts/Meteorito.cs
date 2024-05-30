using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Meteorito : MonoBehaviour
{
    private Mov player;
    private Rigidbody meteorRb;

    private void Awake()
    {
        meteorRb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        meteorRb.velocity += Vector3.down * Random.Range(50, 100); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Suelo"))
        {
            Destroy(gameObject, 1);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Mov>();
            if (player != null)
            {
                player.velocidadActual = 0;
                player.onStun = true;
                player.Invoke("OffStun", 0.5f);
                Destroy(gameObject);
            }
        }
    }
}
