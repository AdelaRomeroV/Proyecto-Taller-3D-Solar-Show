using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedPuasB : MonoBehaviour
{
    public Transform startPoint;
    public float moveSpeed;
    private Rigidbody rb;
    private bool isFalling = true;
    private Mov player;
    public bool inicio;
    public bool anguloDeGiro;
    public float moveSpeedDePrevcion;
    public float moveSpeedDeCaida;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Mov>();
    }

    public void IniciarCorutina()
    {
        StartCoroutine(CrushRoutine());
    }

    private IEnumerator CrushRoutine()
    {
        if (anguloDeGiro)
        {
            yield return new WaitForSeconds(1.5f);
            isFalling = true;
            float timeFalling = 0f;
            while (isFalling)
            {
                if (timeFalling >= 1f)
                {
                    LeftCrusher(moveSpeedDeCaida);
                }
                else
                {
                    LeftCrusher(moveSpeedDePrevcion);
                }

                inicio = false;
                timeFalling += Time.deltaTime;
                yield return null;
            }

            while (!inicio)
            {
                RightCrushed();
                yield return null;
            }
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            isFalling = true;

            float timeFalling = 0f;
            while (isFalling)
            {
                if (timeFalling >= 1f)
                {
                    MoveForward(moveSpeedDeCaida);
                }
                else
                {
                    MoveForward(moveSpeedDePrevcion);
                }

                inicio = false;
                timeFalling += Time.deltaTime;
                yield return null;
            }

            while (!inicio)
            {
                MoveBackward();
                yield return null;
            }
        }

    }

    private void LeftCrusher(float move)
    {
        rb.MovePosition(transform.position + Vector3.left * move * Time.deltaTime);
    }

    private void RightCrushed()
    {
        rb.MovePosition(transform.position + Vector3.right * moveSpeed * Time.deltaTime);
    }
    private void MoveForward(float move)
    {
        rb.MovePosition(transform.position + Vector3.forward * move * Time.deltaTime);
    }

    private void MoveBackward()
    {
        rb.MovePosition(transform.position + Vector3.back * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hazards"))
        {
            isFalling = false;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            isFalling = false;
            player.velocidadActual = 0;
            player.onStun = true;
            player.Invoke("OffStun", 1f);
        }
        if (collision.gameObject.CompareTag("Point"))
        {
            inicio = true;
        }
    }
} 
