using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    [SerializeField] string enemyDestination;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Awake()
    {
       player= GameObject.FindGameObjectWithTag(enemyDestination).transform;
    }
    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
            transform.position = Vector3.MoveTowards(transform.position, player.position, 30 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag(enemyDestination))
        {
            Explode();
        }
    }

    private void Explode()
    {
        Destroy(gameObject); 
    }

}
