using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class Devorador : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    public float speed; 
    private int currentWaypointIndex = 0;
    public bool reachedEnd = false;

    public float checkRadius;
    public LayerMask whatIsPlayer;
    public CinemachineVirtualCamera shakje;

    public GameObject prefabMetoritos;
    public Transform player;

    private void Awake()
    {
        StartCoroutine(Ataque());
    }

    private void Update()
    {
        if (!reachedEnd)
        {
            MoveToWaypoint();
        }
        if (detection())
        {
            speed = 20;
        }
        else
        {
            speed = 50;
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
    IEnumerator Ataque()
    {
        yield return new WaitForSeconds(5f);
        while (true)
        {
            shakje.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
            shakje.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1;

            yield return new WaitForSeconds(2f);
            if(player != null) { Instantiate(prefabMetoritos, new Vector3(player.position.x, player.position.y + 10, player.position.z), Quaternion.identity); }         

            shakje.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
            shakje.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;

            yield return new WaitForSeconds(5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")) 
        {
            Destroy(collision.gameObject);
        }
    }

    private bool detection()
    {
        return Physics.CheckSphere(transform.position, checkRadius, whatIsPlayer);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
