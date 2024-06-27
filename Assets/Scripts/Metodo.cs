using UnityEngine;
using UnityEngine.SceneManagement;

public class Metodo : MonoBehaviour
{
    private void Awake()
    {
        Invoke("Dead", 1f);
    }

    void Dead()
    {
        SceneManager.LoadScene("Game Over");
    }
}
