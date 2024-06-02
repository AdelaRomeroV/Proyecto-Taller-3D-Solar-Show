using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogoCheck_Provisional : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            Debug.Log("DIALOGO");
        }
    }
}
