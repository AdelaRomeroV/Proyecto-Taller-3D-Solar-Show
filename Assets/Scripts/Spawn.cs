using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // pendiente de revision: 0
    public GameObject meteorPrefab; 
    public float spawnInterval = 2f; 
    public Vector3 spawnSize = new Vector3(10f, 5f, 10f);

    private void Start()
    {
        StartCoroutine(SpawnMeteorites());
    }

    private IEnumerator SpawnMeteorites()
    {
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-spawnSize.x / 2f, spawnSize.x / 2f),
                Random.Range(-spawnSize.y / 2f, spawnSize.y / 2f),
                Random.Range(-spawnSize.z / 2f, spawnSize.z / 2f));
                GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

                Rigidbody meteorRb = meteor.GetComponent<Rigidbody>();
                if (meteorRb != null)
                {
                    meteorRb.velocity += Vector3.down * (Random.Range(10, 50));
                }
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 lowerCorner = transform.position - spawnSize / 2f;
        Gizmos.DrawWireCube(lowerCorner + spawnSize / 2f, spawnSize);
    }
}
