using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificacionParedesC : MonoBehaviour
{
    private ParedPuasA paredA;
    private ParedPuasB paredB;

    private void Start()
    {
        paredA = GetComponentInChildren<ParedPuasA>();
        paredB = GetComponentInChildren<ParedPuasB>();

        StartCoroutine(RepetirVerificacion());
    }

    IEnumerator RepetirVerificacion()
    {
        if (paredA.inicio && paredB.inicio)
        {
            paredA.IniciarCorutina();
            paredB.IniciarCorutina();
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(RepetirVerificacion());
    }
}
