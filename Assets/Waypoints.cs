using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    Devorador list;
    private void Awake()
    {
        list = GameObject.Find("Devorador").GetComponent<Devorador>();
        if (list != null ) { list.waypoints.Add(gameObject.transform); }
    }
}
