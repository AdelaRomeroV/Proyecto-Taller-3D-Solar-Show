using UnityEngine;

public class Complete_LevelBoss : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndLevel end = GameObject.Find("EndLevel").GetComponent<EndLevel>();
            end.ChangeLevel = true;
        }
    }
}
