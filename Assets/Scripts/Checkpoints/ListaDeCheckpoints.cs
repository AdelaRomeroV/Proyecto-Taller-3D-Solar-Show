using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ListaDeCheckpoints : MonoBehaviour
{ 
    private static ListaDeCheckpoints instance;
    //[SerializeField] private Material next;
    //[SerializeField] private Material normal;
    [SerializeField] private List<GameObject> Aros;
    [SerializeField] private int count;
    [SerializeField] public int laps;
    [SerializeField] private int lapsMax;

    private LapCounter lapcounter;
    [SerializeField] private string sceneName;

    [SerializeField] GameManager gameManager;

    public static ListaDeCheckpoints Instance {  get { return instance; } }
    public void Start()
    {
        if(GameObject.Find("LapCounterText") != null)
        {
            lapcounter = GameObject.Find("LapCounterText").GetComponent<LapCounter>();
        }

        if (lapcounter != null)
        {
            lapcounter.UpdateText(laps);
        }

    }

    private void Awake()
    {
        instance = this;
        gameManager = FindAnyObjectByType<GameManager>();
        //  Aros[0].GetComponent<MeshRenderer>().material = next;
    }

    public GameObject GetCurrentCheckpoint()
    {
        return Aros[0];
    }
    public GameObject GetLastCheckpoint()
    {
        return Aros[Aros.Count - 1];
    }

    public void Collected(GameObject obj)
    {
        if (obj == Aros[0])
        {
            Aros.Remove(obj);
           // obj.GetComponent<MeshRenderer>().material = normal;
            //Aros[0].GetComponent<MeshRenderer>().material = next;
            Aros.Add(obj);
            count++;
            if(Aros.Count == count) 
            {
                laps++;

                if(lapcounter != null) lapcounter.UpdateText(laps);

                count = 0;
                if(laps== lapsMax)
                {
                    if (gameManager != null)
                    {
                        if (gameManager.levels == 2) { gameManager.levels = 2; }
                        else { gameManager.FinishRace(); }
                    }
                    SceneManager.LoadScene(sceneName);
                }
            }
        }

    }

}
