using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumpers : MonoBehaviour
{
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
            if(player != null)
            {
                Rigidbody autoRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                if (autoRigidbody != null)
                {
                    Vector3 pushDirection = -collision.transform.forward;
                    pushDirection.Normalize();
                    autoRigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);

                    player.velocidadActual = 0;
                    player.onStun = true;
                    Invoke("OffStun", 0.75f);
                }
            }
        }
    }

    void OffStun()
    {
        player.onStun = false;
    }
}
