using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteTutorial : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(gameManager != null) 
            {
                if (gameManager.levels == 1) { gameManager.levels = 1; }
                else { gameManager.FinishRace(); }
            }
        }
    }
}
