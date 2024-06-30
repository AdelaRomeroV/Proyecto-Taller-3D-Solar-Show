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

    [SerializeField] float typingTime = 0.02f;
    [Header("Cuadro de Texto")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text NormalDialogueText;
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private Image Imagen;

    [Header("Dialogos")]
    [SerializeField] private Dialogue[] dialogueLine;

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

            if (NormalDialogueText.text == dialogueLine[lineaIndex].line)
            {
                NextDialogueLine();
            }
            //else
            //{ 
            //    StopAllCoroutines(); 
            //    dialogueText.text = dialogueLine[lineaIndex].line;
            //}
        }

        if (lineaIndex == dialogueLine.Length) DialogueEndEvent.Invoke();
    }

    public void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineaIndex = 0;

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

        NormalDialogueText.text = string.Empty;

        foreach (char ch in dialogueLine[lineaIndex].line)
        {
            NormalDialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private IEnumerator LastDialogue()
    {
        didDialogueStart = false;
        yield return new WaitForSecondsRealtime(2.5f);

        if(canDisableCanva) dialoguePanel.SetActive(false);

        Time.timeScale = 1f;

        Robotvoice.Stop();
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
