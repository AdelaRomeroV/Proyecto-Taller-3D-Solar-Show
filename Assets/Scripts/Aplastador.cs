using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aplastador : MonoBehaviour
{
    public Transform startPoint;
    public float moveSpeed;
    public float moveSpeedDeCaida;
    private Rigidbody rb;
    public bool isFalling = true;
    private Mov player;
    public bool inicio;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Mov>();
        StartCoroutine(CrushRoutine());
    }

    private IEnumerator CrushRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            isFalling = true;
            while (isFalling)
            {
                DowCrusher();
                inicio = false;
                yield return null;
            }

            while (!inicio)
            {
                UpCrushed();
                yield return null;
            }
        }
    }

    private void DowCrusher()
    {
        rb.MovePosition(transform.position + Vector3.down * moveSpeedDeCaida * Time.deltaTime);
    }

    private void UpCrushed()
    {
        rb.MovePosition(transform.position + Vector3.up * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
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
