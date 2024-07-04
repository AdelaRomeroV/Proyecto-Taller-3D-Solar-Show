using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProximityAlert : MonoBehaviour
{
    Image imagen;
    [SerializeField] CalculateDistance distanceScript;

    public float BlinkInterval;
    public bool canBlink;
    public float maxDistance;

    Coroutine blinkCoroutine;
    private void Start()
    {
        imagen = GetComponent<Image>();
        imagen.enabled = false;

        blinkCoroutine = StartCoroutine(Blink());
    }
    private void Update()
    {
        if (distanceScript == null) return;

        if(distanceScript.distance > maxDistance)
        {
            canBlink = false;
        }
        else
        {
            canBlink = true;
        }


        if (distanceScript.distance <= maxDistance)
        {
            SetBlinkInterval(20);
            imagen.color = Color.yellow;
        }
        else if (distanceScript.distance < maxDistance * 0.6f)
        {
            SetBlinkInterval(40);
            imagen.color = Color.red;
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {

            if (canBlink)
            {
                imagen.enabled = !imagen.enabled;
            }
            else
            {
                imagen.enabled = false;
            }

            yield return new WaitForSeconds(BlinkInterval);
        }
    }

    public void SetBlinkInterval(float newInterval)
    {
        BlinkInterval = newInterval;

        // Reiniciar la coroutine para aplicar el nuevo intervalo inmediatamente
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = StartCoroutine(Blink());
        }
    }
}
