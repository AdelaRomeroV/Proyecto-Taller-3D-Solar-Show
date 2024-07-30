using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{        
    private Material material;
    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }
   
    void Update()
    {
        CheckAngle();
    }

    void CheckAngle()
    {
        Transform Checkpoint = ListaDeCheckpoints.Instance.GetCurrentCheckpoint().transform;
        float angulo = Vector3.Dot(Checkpoint.forward, transform.right);
        if(angulo<-0.8)
        {
            material.color = Color.red;
        }
        else
        {
            material.color = Color.white;
        }
        //Vector3.Angle(transform.forward, ListaDeCheckpoints.Instance.GetCurrentCheckpoint().transform.up);
        //if (Vector3.Angle(transform.forward,ListaDeCheckpoints.Instance.GetCurrentCheckpoint().transform.up)>90)
        //{
        //    GetComponent<MeshRenderer>().material = wrongDirection;

        //}
        //else
        //{
        //   // GetComponent<MeshRenderer>().material = normal;
        //}
    }
}