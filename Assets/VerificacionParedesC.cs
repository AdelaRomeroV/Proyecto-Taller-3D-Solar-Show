using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificacionParedesC : MonoBehaviour
{
    private ParedPuasA paredA;
    private ParedPuasB paredB;

    public bool cerrarpuerta;

    private void Start()
    {
        paredA = GetComponentInChildren<ParedPuasA>();
        paredB = GetComponentInChildren<ParedPuasB>();
    }

    private void Update()
    {
        if (paredA.inicio && paredB.inicio)
        {
            cerrarpuerta = true;
        }
    }
}
