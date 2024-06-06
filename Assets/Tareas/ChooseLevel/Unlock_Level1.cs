using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unlock_Level1 : BottonBlock
{
    private void Awake()
    {
        button.onClick.AddListener(ChooseLevel1);
    }
    void ChooseLevel1()
    {
        if (levels >= 1)
        {
            SceneManager.LoadScene("Level_01");
        }
    }
}
