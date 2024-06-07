using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard_Spawner : MonoBehaviour
{
    List<GameObject> Hazards = new List<GameObject>();

    private void Start()
    {
        Hazards = GameObject.Find("Controlador").GetComponent<Controlador_Nivel2>().Hazards;

        int opt = Random.Range(0, Hazards.Count);

        Instantiate(Hazards[opt], transform);
    }
}
