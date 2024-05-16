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
                RightCrushed();
                inicio = false;
                yield return null;
            }

            while (Vector3.Distance(transform.position, startPoint.position) >= 0.1f)
            {
                LeftCrusher();
                yield return null;
            }
            transform.position = startPoint.position;
            if (transform.position == startPoint.position)
            {
                inicio = true;
            }
        }
    }

    private void LeftCrusher()
    {
        rb.MovePosition(transform.position + Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void RightCrushed()
    {
        rb.MovePosition(transform.position + Vector3.right * 120 * Time.deltaTime);
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
    }
}
