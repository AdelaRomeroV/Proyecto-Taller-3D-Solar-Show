using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CheckDriftCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Mov mov = other.GetComponent<Mov>();

            if (Input.GetKey(KeyCode.LeftShift) && mov.estaDerrapando)
            {
                GameObject.Find("Controlador").GetComponent<ControladorTutorial>().derrape++;
            }
            Destroy(this);

        }
    }
}
