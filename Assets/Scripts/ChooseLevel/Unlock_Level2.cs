using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Unlock_Level2 : BottonBlock
{
    [SerializeField] Sprite miniatura;
    public GameManager gameManager;
    Image Image;
    // Start is called before the first frame update
    void Awake()
    {
        Image = GetComponent<Image>();
        gameManager = FindObjectOfType<GameManager>();
        button.onClick.AddListener(ChooseLevel2);
        if (gameManager.levels <= 2) { Image.sprite = miniatura; }
    }

    void ChooseLevel2()
    {
        
        if (gameManager.levels >=2)
        {
            SceneManager.LoadScene("Level_Boss");
        }
    }
}
