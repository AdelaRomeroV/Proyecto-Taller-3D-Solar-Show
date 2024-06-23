using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TemporizadorUI : MonoBehaviour
{
    public TMP_Text temporizadorText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        temporizadorText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    public void UpdateText(int value)
    {
        temporizadorText.text = ""+ value;
    }
}
