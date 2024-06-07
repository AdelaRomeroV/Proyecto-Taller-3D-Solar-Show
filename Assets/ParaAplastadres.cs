using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaAplastadres : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Hazards"))
        {
            Aplastador a = other.gameObject.GetComponent<Aplastador>();
            if (a != null) { a.isFalling = false; }
            }
    }
}
