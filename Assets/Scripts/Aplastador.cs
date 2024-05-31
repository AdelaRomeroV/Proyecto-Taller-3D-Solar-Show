using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aplastador : MonoBehaviour
{
    public Transform startPoint;
    public float moveSpeed;
    public float moveSpeedDePrevcion;
    public float moveSpeedDeCaida;
    private Rigidbody rb;
    public bool isFalling = true;
    private Mov player;
    public bool inicio;

    public GameObject luz;

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
            float timeFalling = 0f;
            while (isFalling)
            {
                if (timeFalling >= 1f)
                {
                    DowCrusher(moveSpeedDeCaida);
                }
                else
                {
                    DowCrusher(moveSpeedDePrevcion);
                }
                luz.SetActive(true);
                inicio = false;
                timeFalling += Time.deltaTime;
                yield return null;
            }

            while (!inicio )
            {
                luz.SetActive(false);
                UpCrushed();
                yield return null;
            }
        }
    }

    private void DowCrusher(float move)
    {
        rb.MovePosition(transform.position + Vector3.down * move * Time.deltaTime);
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
            player.rb.velocity = Vector3.zero;
            player.onStun = true;
            player.Invoke("OffStun", 1f);
        }
        if (collision.gameObject.CompareTag("Point"))
        {
            inicio = true;
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
