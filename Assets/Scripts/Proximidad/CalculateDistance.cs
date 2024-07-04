using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CalculateDistance : MonoBehaviour
{
    public Transform target;
    public string targetName;

    public float distance;

    private void Update()
    {
        if (targetName != null && GameObject.Find(targetName) != null)
        {
            target = GameObject.Find(targetName).GetComponent<Transform>();
        }

        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target.position);
        }
    }
}
