using UnityEngine;

public class CountPeligro : MonoBehaviour
{
    public int count;
    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            boxCollider.enabled = false;
            Invoke("Actived", 5f);
        }
    }

    private void Actived()
    {
        boxCollider.enabled = true; 
    }
}
