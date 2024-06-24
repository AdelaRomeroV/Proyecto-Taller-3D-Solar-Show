using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TemporizadorUI : MonoBehaviour
{
    public TextMeshProUGUI temporizadorText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        temporizadorText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void UpdateText(float value)
    {
        temporizadorText.text = value.ToString("0");
    }
}
