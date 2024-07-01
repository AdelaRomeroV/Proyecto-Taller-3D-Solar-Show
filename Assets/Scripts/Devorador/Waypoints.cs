using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    Devorador list;
    private void Start()
    {
        GameObject devorador = GameObject.Find("Devorador");
        if (devorador != null ) 
        {
            list = devorador.GetComponent<Devorador>();
            list.waypoints.Add(gameObject.transform);
        }
    }
}
