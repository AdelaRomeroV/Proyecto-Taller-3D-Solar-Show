using UnityEngine;

public class EndLevelCollider : MonoBehaviour
{
    EndLevel obj;

    private void Start()
    {
        obj = GameObject.Find("EndLevel").GetComponent<EndLevel>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            obj.ChangeLevel = true;
        }
    }
}
