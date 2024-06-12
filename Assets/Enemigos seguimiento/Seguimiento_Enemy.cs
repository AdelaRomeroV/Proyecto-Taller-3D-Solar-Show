using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Seguimiento_Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] Transform Player;

    void Update()
    {
        enemy.SetDestination(Player.position);   
    }
}
