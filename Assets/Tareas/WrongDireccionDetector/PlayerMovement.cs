using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    [SerializeField] private Material normal;
    [SerializeField] private Material wrong;
    [SerializeField] private float speed;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckAngle();
        Move();
    }

    void CheckAngle()
    {
        Debug.Log(Vector3.Angle(transform.forward, ListaDeCheckpoints.Instance.GetCurrentCheckpoint().transform.up));
        if (Vector3.Angle(transform.forward,ListaDeCheckpoints.Instance.GetCurrentCheckpoint().transform.up)>90)
        {
            GetComponent<MeshRenderer>().material = wrong;
        }
        else
        {
            GetComponent<MeshRenderer>().material = normal;
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.up * horizontal * Time.deltaTime * 90);
        rb.velocity = new Vector3(transform.forward.x* vertical * speed, rb.velocity.y,transform.forward.z* vertical * speed);
    }
}