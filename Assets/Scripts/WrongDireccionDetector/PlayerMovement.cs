using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{        
    private Material material;

    //public AlertaUI alertaUI;
    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
      // alertaUI = GetComponent<AlertaUI>();
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
           // alertaUI.UpdateText("Dirección Contraria");
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