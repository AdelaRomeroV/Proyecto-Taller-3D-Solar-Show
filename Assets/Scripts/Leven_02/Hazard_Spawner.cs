using System.Collections.Generic;
using UnityEngine;

public class Hazard_Spawner : MonoBehaviour
{
    List<GameObject> Hazards = new List<GameObject>();
    [SerializeField] List<GameObject> HazardsVa;

    private void Start()
    {
        Hazards = GameObject.Find("Controlador").GetComponent<Controlador_Nivel2>().Hazards;

        int opt = Random.Range(0, Hazards.Count);

        switch (Hazards[opt])
        {
            case var hazard when hazard == Hazards[6]:
                Instantiate(HazardsVa[0], transform);
                break;
            case var hazard when hazard == Hazards[7]:
                Instantiate(HazardsVa[1], transform);
                break;
            default:
                Instantiate(Hazards[opt], transform);
                break;
        }
    }
}
