using System.Collections;
using UnityEditor;
using UnityEngine;

public class ControladorCoche : MonoBehaviour
{
    public float moveSpeed;
    public float maxSpeed;
    public float drag;
    public float steerAngle;
    public float traction;

    private Vector3 moveForce;

    private void Update()
    {
        Movimiento();
        Direccion();
        Limit();
        Traccion();
    }

    private void Traccion()
    {
        moveForce = Vector3.Lerp(moveForce.normalized, transform.forward, traction * Time.deltaTime) * moveForce.magnitude;
    }

    private void Direccion()
    {
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * moveForce.magnitude * steerAngle * Time.deltaTime);
    }

    private void Limit()
    {
        moveForce *= drag;
        moveForce = Vector3.ClampMagnitude(moveForce, maxSpeed);
    }

    private void Movimiento()
    {
        moveForce = transform.forward * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position += moveForce * Time.deltaTime;
    }
}



