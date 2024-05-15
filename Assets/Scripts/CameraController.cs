using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Mov mov;
    [SerializeField] Turbo turbo;

    public Vector3 PosInicial;
    Vector3 ActualPos;
    private void Start()
    {
        PosInicial = transform.localPosition;
    }

    private void Update()
    {
        float z = Mathf.Lerp(transform.localPosition.z, ActualPos.z, 1 * Time.deltaTime);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z );


        if (mov.velocidadActual >= mov.velocidadMaxima)
        {
            ActualPos = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.9f);
        }
        else if (turbo.TurboActivo || mov.boostActivado)
        {
            ActualPos = new Vector3(transform.localPosition.x, transform.localPosition.y, -1.1f);
        }
        else
        {
            ActualPos = PosInicial;
        }
    }
}
