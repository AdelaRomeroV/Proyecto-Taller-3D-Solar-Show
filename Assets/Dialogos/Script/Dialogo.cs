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
    private bool didDialogueStart;
    private int lineaIndex;

    private float typingTime = 0.02f;
    [Header("Cuadro de Texto")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private Image Imagen;

    [Header("Dialogos")]
    [SerializeField] private Dialogue[] dialogueLine;
    [SerializeField] GameObject LastInstructions;
    [SerializeField] GameObject CurrentInstructions;

    [Header("Eventos")]
    [SerializeField] UnityEvent OntriggerEnter;
    [SerializeField] UnityEvent DialogueEndEvent;

    [Header("Variables")]
    [SerializeField] float TimeScale;
    [SerializeField] bool CanChangeTime;
    [SerializeField] bool canDisableCanva;

    [Header("Sonidos")]
    public AudioClip niftyVoice;
    public AudioClip Qwarkvoice;
    public AudioSource Robotvoice;
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

    public void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineaIndex = 0;

        if(LastInstructions != null) LastInstructions.SetActive(false);
        if (CanChangeTime) Time.timeScale = TimeScale; //afecta en el movimiento del player

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
        if (lineaIndex != 0) yield return new WaitForSecondsRealtime(1f);

        Imagen.sprite = dialogueLine[lineaIndex].sprite;
        NameText.text = dialogueLine[lineaIndex].CharName;

        if(dialogueLine[lineaIndex].CharName == "Nifty")
        {
            Robotvoice.clip = niftyVoice;

            Robotvoice.Play();
        }

        if(dialogueLine[lineaIndex].CharName == "Qwark")
        {
            Robotvoice.clip = Qwarkvoice;

            Robotvoice.Play();
        }

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

        if(canDisableCanva) dialoguePanel.SetActive(false);

        Time.timeScale = 1f;

        if (DialogueEndEvent != null) DialogueEndEvent.Invoke();
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
