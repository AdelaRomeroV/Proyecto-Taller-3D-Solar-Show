using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // pendiente de revision: 0
    public GameObject meteorPrefab; 
    public float spawnInterval = 2f; 
    public Vector3 spawnSize = new Vector3(30f, 5f, 30f);

    private void Start()
    {
        SpawnMeteorites();
    }

    private void SpawnMeteorites()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(
            Random.Range(-spawnSize.x / 2f, spawnSize.x / 2f),
            Random.Range(-spawnSize.y / 2f, spawnSize.y / 2f),
            Random.Range(-spawnSize.z / 2f, spawnSize.z / 2f));
            Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 lowerCorner = transform.position - spawnSize / 2f;
        Gizmos.DrawWireCube(lowerCorner + spawnSize / 2f, spawnSize);
    }
}
