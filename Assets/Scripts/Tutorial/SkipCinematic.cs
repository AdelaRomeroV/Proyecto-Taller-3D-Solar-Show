using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCinematic : MonoBehaviour
{
    [SerializeField] List<GameObject> TurnOffObjects = new List<GameObject>();
    [SerializeField] List<GameObject> TurnOnObjects = new List<GameObject>();
    [SerializeField] GameObject player;
    [SerializeField] Transform StartPoint;

    Queue<KeyCode> SpaceBuffer = new Queue<KeyCode>();
    bool canSkip;

    private void Update()
    {
        ImputBuffer();

        if (canSkip)
        {
            foreach (GameObject g in TurnOffObjects)
            {
                if (g.activeInHierarchy)
                g.SetActive(false);
            }

            foreach (GameObject g in TurnOnObjects)
            {
                g.SetActive(true);
            }

            if (StartPoint != null && player != null) player.transform.position = StartPoint.position;

            
        }
    }

    void ImputBuffer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceBuffer.Enqueue(KeyCode.Space);
            Invoke("dequeue", 1f);
        }

        if(SpaceBuffer.Count == 2)
        {
            canSkip = true;
        }
    }

    void dequeue()
    {
        SpaceBuffer.Dequeue();
    }
}
