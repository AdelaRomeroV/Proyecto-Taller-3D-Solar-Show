using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pared : MonoBehaviour
{
    //pendiente revisarlo : 0
    public float pushForce;
    private Mov player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Mov>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody autoRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (autoRigidbody != null)
            {
                Vector3 pushDirection = -collision.transform.forward;
                pushDirection.Normalize();
                autoRigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
                autoRigidbody.velocity = Vector3.zero;

                player.velocidadActual = 0;
                player.onStun = true;
                Invoke("OffStun", 0.5f);
            }
        }
    }

    void OffStun()
    {
        player.onStun = false;
    }
}
