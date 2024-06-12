using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int levels;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void FinishRace()
    {
        levels++;
    }
    void Update()
    {

    }
}
