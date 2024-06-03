using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent Following;

    private void Start()
    {
        Following = GetComponent<NavMeshAgent>();
        
    }
    private void Update()
    {
        Following.SetDestination(player.position);
    }
}
