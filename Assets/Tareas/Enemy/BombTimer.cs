using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTimer : MonoBehaviour
{
    public float TimeCounter = 5;
    public GameObject explosion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeCounter -= Time.deltaTime;
        if(TimeCounter <=0)
        {
            AutoDestroy();
        }
    }
    void AutoDestroy()
    {
        if(explosion != null)
        {
            Instantiate(explosion,transform.position, Quaternion.identity); 

        }
        Destroy(gameObject);
    }
}
