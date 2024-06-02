using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard_Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> Hazards = new List<GameObject>();

    private void Start()
    {
        int opt = Random.Range(0, Hazards.Count);

        Instantiate(Hazards[opt], transform);
    }
}
