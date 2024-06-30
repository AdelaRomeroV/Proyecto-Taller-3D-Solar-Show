using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesControllerTutorial : MonoBehaviour
{
    [SerializeField] List<GameObject> Naves = new List<GameObject>();
    [SerializeField] List<Transform> PlayerPos = new List<Transform>();

    public int navesDestruidas = 0;

    public bool LeftAlive;
    public bool RightAlive;

    WaitForSeconds SpawnDelay = new WaitForSeconds(0.5f);
    private void Start()
    {
        StartCoroutine(SpawnLeft());
        StartCoroutine(SpawnRight());
    }

    IEnumerator SpawnRight()
    {
        RightAlive = true;
        yield return SpawnDelay;

        GameObject a = Instantiate(Naves[1], PlayerPos[1].position, PlayerPos[1].rotation);
        a.transform.parent = PlayerPos[1];
    }

    IEnumerator SpawnLeft()
    {
        LeftAlive = true;
        yield return SpawnDelay;

        GameObject a = Instantiate(Naves[0], PlayerPos[0].position, PlayerPos[0].rotation);
        a.transform.parent = PlayerPos[0];
    }

    private void Update()
    {
        if(navesDestruidas < 6)
        {
            if (!LeftAlive) StartCoroutine(SpawnLeft());
            if (!RightAlive) StartCoroutine(SpawnRight());
        }
        else if(navesDestruidas >= 6)
        {
            this.enabled = false;
        }
    }


}
