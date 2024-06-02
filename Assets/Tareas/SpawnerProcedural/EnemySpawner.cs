using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject enemyspawner;
    public int Counter;

    public int positionX;
    public int positionZ;

    public GameObject[] ItemSpawner;
    // Start is called before the first frame update
    void Start()
    {
        enemyspawner = GameObject.FindWithTag("Enemy");
        StartCoroutine(Spawner());
    }

    void Update()
    {
    }

    IEnumerator Spawner()
    {
        while (Counter < 5)
        {
            positionX = Random.Range(-5, 6);
            positionZ = Random.Range(-4, 9);
            int randomIndex = Random.Range(0, ItemSpawner.Length);
            if (gameObject.CompareTag("Enemy"))
            {
                Instantiate(ItemSpawner[0], new Vector3(positionX, 1, positionZ), Quaternion.identity);
            }
            else
            {
                Instantiate(ItemSpawner[randomIndex], new Vector3(positionX, 1, positionZ), Quaternion.identity);
            }

            yield return new WaitForSeconds(1);
            Counter++;
        }

    }
}
