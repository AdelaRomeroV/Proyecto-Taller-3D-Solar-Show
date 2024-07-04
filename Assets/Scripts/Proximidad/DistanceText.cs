using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;

public class DistanceText : MonoBehaviour
{
    [SerializeField] CalculateDistance distanceScript;
    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (distanceScript == null) return;

        if(distanceScript.target.name != distanceScript.targetName )
        {
            text.text = "Calculando";
        }
        else
        {
            int meters = (int)distanceScript.distance;
            text.text = $"{meters}m";
        }
    }
}
