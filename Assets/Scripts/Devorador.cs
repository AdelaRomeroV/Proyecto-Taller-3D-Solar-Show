using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devorador : MonoBehaviour
{
    [SerializeField] public List<Transform> waypoints = new List<Transform>();
    public float speedActual;
    public float speedmin;
    public float speedMax;

    private int currentWaypointIndex = 0;
    public bool reachedEnd = false;

    public float checkRadius;
    public LayerMask whatIsPlayer;
    public CinemachineVirtualCamera shakje;

    public GameObject prefabMetoritos;
    public Transform player;

    public float intervalAtack;
    public float intervalAnti;

    private void Start()
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
            speedActual = speedmin;
        }
        else
        {
            speedActual = speedMax;
        }
    }

    private void MoveToWaypoint()
    {
        if (waypoints == null || waypoints.Count == 0) return;

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speedActual * Time.deltaTime);

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
        yield return new WaitForSeconds(intervalAtack);
        while (true)
        {
            shakje.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 2;
            shakje.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 2;

            yield return new WaitForSeconds(intervalAnti);
            if (player != null) { Instantiate(prefabMetoritos, new Vector3(player.position.x, player.position.y + 20, player.position.z), Quaternion.identity); }

            shakje.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
            shakje.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;

            yield return new WaitForSeconds(intervalAtack);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
