using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public struct Dialogue
{
    public Sprite sprite;
    //public string name;
    [TextArea(4, 6)] public string line;
}
public class Dialogo : MonoBehaviour
{
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineaIndex;

    private float typingTime = 0.05f;

    [SerializeField] ControladorTutorial controlador;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] bool CanStopTime;
    [SerializeField] private Dialogue[] dialogueLine;
    [SerializeField] GameObject LastInstructions;
    [SerializeField] GameObject CurrentInstructions;

    private void Start()
    {
        controlador = GameObject.Find("Controlador").GetComponent<ControladorTutorial>();
    }

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
        LastInstructions.SetActive(false);
        if (CanStopTime) Time.timeScale = 0f; //afecta en el movimiento del player

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
        yield return new WaitForSecondsRealtime(0.2f);
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
        yield return new WaitForSecondsRealtime(2);
        dialoguePanel.SetActive(false);
        Time.timeScale = 1f;

        if(CurrentInstructions != null) CurrentInstructions.SetActive(true);
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
