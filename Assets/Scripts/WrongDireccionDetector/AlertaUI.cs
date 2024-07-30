using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlertaUI : MonoBehaviour
{
    public TMP_Text Direccion;
    // Start is called before the first frame update
    void Awake()
    {
        Direccion = GetComponent<TMP_Text>();
    }
    public void UpdateText(string showtext)
    {
        Direccion.text = showtext;
    }
}
