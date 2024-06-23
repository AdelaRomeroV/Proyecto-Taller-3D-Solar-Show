using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditos : MonoBehaviour
{
    [SerializeField] private Dialogo iniciar;
    private void Awake()
    {
        iniciar.StartDialogue();
    }
}
