using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapCounter : MonoBehaviour 
{
    public TMP_Text Laptext;
    // Start is called before the first frame update
    void Awake()
    {
        Laptext=GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    public void UpdateText(int value)
    {
        Laptext.text = "Lap: " + value;
    }
}
