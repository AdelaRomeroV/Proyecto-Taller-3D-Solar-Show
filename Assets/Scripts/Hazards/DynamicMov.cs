using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class DynamicMov : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    private Transform currentPoint;
    [SerializeField] private float speed = 20;

    private void Start()
    {
        currentPoint = pointB;
    }

    void Update()
    {
        MovBumpers();
    }

    private void MovBumpers()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform) // convertir corrutina
        {
            currentPoint = pointA.transform;
        }
        if (Vector3.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
    }
}
