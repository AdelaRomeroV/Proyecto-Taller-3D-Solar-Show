using System.Collections;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CronometroIncio : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject boxText;
    [SerializeField] Mov player;
    [SerializeField] Turbo turbo;
    [SerializeField] ControlDeVida vida;
    [SerializeField] PlayerAnimations animations;
    public Controlador temporizador;

    public float startTime = 3f; 
    private float currentTime;

    [SerializeField] UnityEvent FinishEvent;

    private void Start()
    {
        currentTime = startTime;
        UpdateTimerText(currentTime);
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        while (currentTime > 0)
        {
            UpdateTimerText(currentTime);
            text.fontSize = 80;

            StartCoroutine(AnimateFontSize(80, 160, 0.2f));

            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        text.text = "Start";

        if(temporizador != null)
        {
            temporizador.ActivarTemporizador();
        }

        if (FinishEvent != null) FinishEvent.Invoke();
        Active();
        StartCoroutine(AnimateFontSize(80, 160, 0.2f));
        yield return new WaitForSeconds(1f);
        boxText.SetActive(false);

    }
    private IEnumerator AnimateFontSize(int fromSize, int toSize, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            text.fontSize = Mathf.Lerp(fromSize, toSize, elapsed / duration);
            yield return null;
        }
        text.fontSize = toSize;
    }

    private void UpdateTimerText(float time)
    {
        text.text = time.ToString("0");
    }

    private void Active()
    {
        player.enabled = true;
        turbo.enabled = true;
        animations.enabled = true;
        vida.enabled = true;
    }
}
