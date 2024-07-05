using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotaciónobject : MonoBehaviour
{
    public float speedRot;
    private void Update()
    {
        Orbita();
    }

    void Orbita()
    {
        this.transform.Rotate(new Vector3(0, speedRot, 0) * Time.deltaTime);
    }
}
