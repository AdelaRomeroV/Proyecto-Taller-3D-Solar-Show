using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct Dialogue
{
    public Sprite sprite;
    [TextArea(4, 6)] public string line;
}
public class Dialogo : MonoBehaviour
{
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineaIndex;

    private float typingTime = 0.05f;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Dialogue[] dialogueLine;

    void Update()
    {
        if(isPlayerInRange)
        {
            if (!didDialogueStart) 
            { 
                StartDialogue(); 
            }

            else if (dialogueText.text == dialogueLine[lineaIndex].line) 
            { 
                NextDialogueLine(); 
            }

            else
            { 
                StopAllCoroutines(); 
                dialogueText.text = dialogueLine[lineaIndex].line;
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineaIndex = 0;
        Time.timeScale = 3f;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
       lineaIndex++;
        if(lineaIndex < dialogueLine.Length)
        {
            StartCoroutine(ShowLine());
        }

        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;

        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach(char ch in dialogueLine[lineaIndex].line)
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

}
