using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int levels;
    [SerializeField] public string sceneName;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        DontDestroyOnLoad(gameObject);
    }
    public void FinishRace()
    {
        levels++;
    }

}