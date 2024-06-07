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
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) { player = playerObject.GetComponent<Mov>(); }
        StartCoroutine(CrushRoutine());
    }

    private IEnumerator CrushRoutine()
    {
        while (true)
        {
            float a = Random.Range(1f, 2.5f);
            yield return new WaitForSeconds(a);

            isFalling = true;
            float timeFalling = 0f;
            while (isFalling)
            {
                if (timeFalling >= 0.5f)
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

    /*private void OnCollisionEnter(Collision collision)
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
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Suelo"))
        {
            isFalling = false;
        }
        if (other.CompareTag("Player"))
        {
            isFalling = false;

            player.velocidadActual = 0;
            player.rb.velocity = Vector3.zero;
            player.onStun = true;
            player.Invoke("OffStun", 1f);
        }
        if (other.CompareTag("Point"))
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
