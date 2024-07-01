using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnB : MonoBehaviour
{
    // pendiente de revision: 0
    public GameObject[] meteorPrefab;
    public float spawnInterval;

    private void Start()
    {
        StartCoroutine(SpawnMeteorites());
    }

    private IEnumerator SpawnMeteorites()
    {
        while (true)
        {
            GameObject randomMeteor = meteorPrefab[Random.Range(0, meteorPrefab.Length)];
            Instantiate(randomMeteor, gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
