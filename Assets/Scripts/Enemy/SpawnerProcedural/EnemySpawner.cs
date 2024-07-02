using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int Counter;

    public GameObject[] EnemyPrefab;
    public Vector3 spawnSize;
    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boxCollider.enabled = false;
            Invoke("Actived", 20f);
        }
    }

    private void Actived()
    {
        boxCollider.enabled = true;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SpawnEnemies();
        }
    }
    private void SpawnEnemies()
    {
        int randomIndex = Random.Range(0, EnemyPrefab.Length);

        Vector3 spawnPosition = transform.position + new Vector3(
            Random.Range(-spawnSize.x / 2f, spawnSize.x / 2f),
            Random.Range(-spawnSize.y / 2f, spawnSize.y / 2f),
            Random.Range(-spawnSize.z / 2f, spawnSize.z / 2f));
        Instantiate(EnemyPrefab[randomIndex], spawnPosition, Quaternion.identity);
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 lowerCorner = transform.position - spawnSize / 2f;
        Gizmos.DrawWireCube(lowerCorner + spawnSize / 2f, spawnSize);
    }
}
