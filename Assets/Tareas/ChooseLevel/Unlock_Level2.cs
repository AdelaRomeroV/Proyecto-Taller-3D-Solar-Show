using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unlock_Level2 : BottonBlock
{
    // Start is called before the first frame update
    void Awake()
    { 
        button.onClick.AddListener(ChooseLevel2);
    }

    void ChooseLevel2()
    {
        if (levels == 2)
        {
            SceneManager.LoadScene("Level_02");
        }
    }
}