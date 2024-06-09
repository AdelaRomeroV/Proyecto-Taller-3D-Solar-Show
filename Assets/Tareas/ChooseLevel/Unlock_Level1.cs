using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unlock_Level1 : BottonBlock
{
    public GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        button.onClick.AddListener(ChooseLevel1);
    }
    void ChooseLevel1()
    {
        if (true )//gameManager.levels >= 1)
        {
            SceneManager.LoadScene("Level_01");
        }
    }
}
