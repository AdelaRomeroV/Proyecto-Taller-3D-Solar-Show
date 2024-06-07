using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedPuasA : MonoBehaviour
{
    public Transform startPoint;
    public float moveSpeed;
    private Rigidbody rb;
    private bool isFalling = true;
    private Mov player;
    public bool inicio;
    public bool anguloDeGiro;
    public float moveSpeedDePrevencion;
    public float moveSpeedDeCaida;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) { player = playerObject.GetComponent<Mov>(); }
    }

    public void IniciarCorutina()
    {
        StartCoroutine(CrushRoutine());
    }

    private IEnumerator CrushRoutine()
    {


        if (anguloDeGiro)
        {
            yield return new WaitForSeconds(1f);
            isFalling = true;
            float timeFalling = 0f;
            while (isFalling)
            {
                if (timeFalling >= 1f)
                {
                    RightCrushed(moveSpeedDeCaida);
                }
                else
                {
                    RightCrushed(moveSpeedDePrevencion);
                }

                inicio = false;
                timeFalling += Time.deltaTime;
                yield return null;
            }

            while (!inicio)
            {
                LeftCrusher();
                yield return null;
            }
        }
        else
        {
            yield return new WaitForSeconds(1f);
            isFalling = true;
            float timeFalling = 0f;
            while (isFalling)
            {
                if (timeFalling >= 1f)
                {
                    MoveBackward(moveSpeedDeCaida);
                }
                else
                {
                    MoveBackward(moveSpeedDePrevencion);
                }
                inicio = false;
                timeFalling += Time.deltaTime;
                yield return null;
            }

            while (!inicio)
            {
                MoveForward();
                yield return null;
            }
        }
    }

    private void LeftCrusher()
    {
        rb.MovePosition(transform.position + Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void RightCrushed(float move)
    {
        rb.MovePosition(transform.position + Vector3.right * move * Time.deltaTime);
    }

    private void MoveForward()
    {
        rb.MovePosition(transform.position + Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void MoveBackward(float move)
    {
        rb.MovePosition(transform.position + Vector3.back * move * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            {
                player.velocidadActual = 0;
                player.rb.velocity = Vector3.zero;
                player.onStun = true;
                player.Invoke("OffStun", 1f);
                StartCoroutine(DesactivarColision());
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            inicio = true;
        }
        if (other.CompareTag("Hazards") || other.CompareTag("Player"))
        {
            isFalling = false;
        }
    }
    private IEnumerator DesactivarColision()
    {
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
        yield return new WaitForSeconds(2f);
        collider.isTrigger = false;
    }
}
