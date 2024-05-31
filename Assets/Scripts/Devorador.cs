using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devorador : MonoBehaviour
{
    public Transform[] waypoints; 
    public float speed; 
    private int currentWaypointIndex = 0;
    public bool reachedEnd = false;

    private void Update()
    {
        if (!reachedEnd)
        {
            MoveToWaypoint();
        }
    }

    private void MoveToWaypoint()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

            if (currentWaypointIndex == 0)
            {
                reachedEnd = true;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")) 
        {
            Destroy(collision.gameObject);
        }
    }
}
