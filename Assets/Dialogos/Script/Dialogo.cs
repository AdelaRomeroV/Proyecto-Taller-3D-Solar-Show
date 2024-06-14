using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public struct Dialogue
{
    public Sprite sprite;
    public string CharName;
    [TextArea(4, 6)] public string line;
}
public class Dialogo : MonoBehaviour
{
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineaIndex;

    private float typingTime = 0.02f;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private Image Imagen;
    [SerializeField] bool CanStopTime;
    [SerializeField] private Dialogue[] dialogueLine;
    [SerializeField] GameObject LastInstructions;
    [SerializeField] GameObject CurrentInstructions;
    [SerializeField] UnityEvent OntriggerEnter;


    public bool FinalDialogue = false;
    public bool turboDialogue = false;
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
        if (CanStopTime) Time.timeScale = 0.8f; //afecta en el movimiento del player

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

        yield return new WaitForSecondsRealtime(1f);
        Imagen.sprite = dialogueLine[lineaIndex].sprite;
        NameText.text = dialogueLine[lineaIndex].CharName;
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
        yield return new WaitForSecondsRealtime(2.5f);
        dialoguePanel.SetActive(false);
        Time.timeScale = 1f;

        if (turboDialogue)
        {
            Turbo turbo = GameObject.FindGameObjectWithTag("Player").GetComponent<Turbo>();
            turbo.enabled = true;
        }

        if (FinalDialogue)
        {
            ControladorTutorial controlador = GameObject.Find("Controlador").GetComponent<ControladorTutorial>();
            controlador.PasarEscena = true;
        }

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

                if(OntriggerEnter != null)
                OntriggerEnter.Invoke();
            }
        }
    }

}
