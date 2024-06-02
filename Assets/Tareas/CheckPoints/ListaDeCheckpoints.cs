using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaDeCheckpoints : MonoBehaviour
{ 
    private static ListaDeCheckpoints instance;
    [SerializeField] private Material next;
    [SerializeField] private Material normal;
    [SerializeField] private List<GameObject> Aros;
    [SerializeField] private int count;
    [SerializeField] private int laps;

    public static ListaDeCheckpoints Instance {  get { return instance; } }

    private void Awake()
    {
        instance = this;
        Aros[0].GetComponent<MeshRenderer>().material = next;
    }

    public GameObject GetCurrentCheckpoint()
    {
        return Aros[0];
    }

    public void Collected(GameObject obj)
    {
        if (obj == Aros[0])
        {
            Aros.Remove(obj);
            obj.GetComponent<MeshRenderer>().material = normal;
            Aros[0].GetComponent<MeshRenderer>().material = next;
            Aros.Add(obj);
            count++;
            if(Aros.Count == count) 
            {
                laps++;
                count = 0;
                if(laps==3)
                {

                    //Terminar el nivel
                }
            }
        }

    }
}
