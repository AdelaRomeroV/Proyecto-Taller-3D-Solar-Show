using UnityEngine;
using UnityEngine.SceneManagement;

public class Complete_LevelBoss : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            SceneManager.LoadScene("Victory");
        }
    }
}
