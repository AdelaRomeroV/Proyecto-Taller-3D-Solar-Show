using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camarografo_Orb : MonoBehaviour
{
    [SerializeField] private float VelocidadRotacion;
    [SerializeField] Transform pivote;
    private void Update()
    {
        this.transform.RotateAround(pivote.transform.position,Vector3.up, VelocidadRotacion * Time.deltaTime);
    }
}
