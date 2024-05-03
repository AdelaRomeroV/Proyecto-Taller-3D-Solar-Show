using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KartController : MonoBehaviour
{
    [Header("Kart")]
    public Rigidbody sphere;
    public Transform kartNormal;

    [Header("S")]
    private float speed, currentSpeed;
    private float rotate, currentRotate;
    private int driftDirection;
    public float acceleration = 30f;
    public float steering = 80f;
    public float gravity = 10f;
    public bool drifting;

    void Update()
    {
        transform.position = sphere.transform.position - new Vector3(0, -2f,0);

        if (Input.GetKey(KeyCode.W))
        {
            speed = acceleration;
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            int dir = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
            float amount = Mathf.Abs(Input.GetAxis("Horizontal"));
            Steer(dir, amount);
        }

        if (Input.GetButtonDown("Jump") && !drifting && Input.GetAxis("Horizontal") != 0)
        {
            drifting = true;
            driftDirection = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
        }

        if (drifting)
        {
            float control = (driftDirection == 1) ? Remap(Input.GetAxis("Horizontal"), -1, 1, 0, 2) : Remap(Input.GetAxis("Horizontal"), -1, 1, 2, 0);
            Steer(driftDirection, control);
        }

        if (Input.GetButtonUp("Jump") && drifting)
        {
            Boost();
        }

        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f); speed = 0f;
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f); rotate = 0f;
    }
    private void FixedUpdate()
    {
        if (!drifting)
            sphere.AddForce(kartNormal.transform.right * currentSpeed, ForceMode.Acceleration);
        else
            sphere.AddForce(transform.forward * currentSpeed, ForceMode.Acceleration);

        sphere.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);

    }

    public void Steer(int direction, float amount)
    {
        rotate = (steering * direction) * amount;
    }
    public void Boost()
    {
        drifting = false;

    }

    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
