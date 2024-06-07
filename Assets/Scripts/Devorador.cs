using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devorador : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
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
        if (waypoints == null || waypoints.Count == 0) return;

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;

            if (currentWaypointIndex == 0)
            {
                reachedEnd = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")) 
        {
            Destroy(collision.gameObject);
        }
    }
}
