using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aplastador : MonoBehaviour
{
    public Transform startPoint;
    public float moveSpeed;
    private Rigidbody rb;
    public bool isFalling = true;
    private Mov player;

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
                yield return null;
            }

            while (Vector3.Distance(transform.position, startPoint.position) >= 0.1f)
            {
                UpCrushed();
                yield return null;
            }

            transform.position = startPoint.position;
        }
    }

    private void DowCrusher()
    {
        rb.MovePosition(transform.position + Vector3.down * 240 * Time.deltaTime);
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
    }
}
