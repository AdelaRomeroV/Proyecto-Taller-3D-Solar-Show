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
    [SerializeField] List<GameObject> Instrucciones = new List<GameObject>();

    int CurrentIns = -1;

    void Update()
    {
        if (didDialogueStart)
        {

            if (dialogueText.text == dialogueLine[lineaIndex].line)
            {
                NextDialogueLine();
            }
            //else
            //{ 
            //    StopAllCoroutines(); 
            //    dialogueText.text = dialogueLine[lineaIndex].line;
            //}
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineaIndex = 0;
        Time.timeScale = 0f; //afecta en el movimiento del player

        CurrentIns++;

        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineaIndex++;
        if (lineaIndex < dialogueLine.Length)
        {
            StartCoroutine(ShowLine());
        }

        else
        {
            StartCoroutine(LastDialogue());
        }
    }

    private IEnumerator ShowLine()
    {
        yield return new WaitForSecondsRealtime(1);
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLine[lineaIndex].line)
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private IEnumerator LastDialogue()
    {
        didDialogueStart = false;
        yield return new WaitForSecondsRealtime(1);
        dialoguePanel.SetActive(false);
        Instrucciones[CurrentIns].SetActive(true);
        Time.timeScale = 1f;
        Destroy(this);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
        }
    }

}
