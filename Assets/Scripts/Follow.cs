using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Awake()
    {
       player= GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        agent.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        Destroy(gameObject); 
    }

}
