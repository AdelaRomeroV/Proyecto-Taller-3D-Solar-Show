using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Resolucion : MonoBehaviour
{
    public Toggle toggle;
    public TMP_Dropdown resolutionDropDown;
    Resolution[] resolutionsA;

    private void Start()
    {
        if (Screen.fullScreen)
        { toggle.isOn = true; }

        else { toggle.isOn = false; }

        ResolucionRev();
    }

    public void ActFullScreen(bool fScreen)
    {
        Screen.fullScreen = fScreen;
    }    


    public void ResolucionRev()
    {
        resolutionsA = Screen.resolutions;
        resolutionDropDown.ClearOptions();
        List<string> Opt = new List<string> ();
        int resolutionActual = 0;

        for (int i = 0; i < resolutionsA.Length; i++) 
        {
            string opcion = resolutionsA[i].width + "x" + resolutionsA[i].height;
            Opt.Add(opcion);

            if(Screen.fullScreen && resolutionsA[i].width == Screen.currentResolution.width && resolutionsA[i].height == Screen.currentResolution.height)
            {
                resolutionActual = i;
            }
        }

        resolutionDropDown.AddOptions(Opt);
        resolutionDropDown.value = resolutionActual;
        resolutionDropDown.RefreshShownValue();

    }

    public void CanvasResolutionChanged(int indiceResolucion)
    {
        
    }

}
