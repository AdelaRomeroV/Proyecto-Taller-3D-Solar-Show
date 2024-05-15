using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorito : MonoBehaviour
{
    private Mov player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Mov>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Suelo"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            player.velocidadActual = 0;
            player.onStun = true;
            player.Invoke("OffStun", 0.25f);
            Destroy(gameObject);
        }
    }
}
