using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    //[SerializeField] private Material normal;
    [SerializeField] private Material wrongDirection;
    void Update()
    {
        CheckAngle();
    }

    void CheckAngle()
    {
        Vector3.Angle(transform.forward, ListaDeCheckpoints.Instance.GetCurrentCheckpoint().transform.up);
        if (Vector3.Angle(transform.forward,ListaDeCheckpoints.Instance.GetCurrentCheckpoint().transform.up)>90)
        {
            GetComponent<MeshRenderer>().material = wrongDirection;

        }
        else
        {
           // GetComponent<MeshRenderer>().material = normal;
        }
    }
}