using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambioDeEscenas : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    public Button button;
    public string sceneName;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeScene);
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void ChangeScene()
    {
        if(gameManager != null) 
        {            
            SceneManager.LoadScene(gameManager.sceneName);
        }
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
}
