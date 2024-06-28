using System.Collections.Generic;
using UnityEngine;

public class Acelerador : MonoBehaviour
{
    private Mov playerMovement;

    [SerializeField] List<GameObject> Effects = new List<GameObject>();
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerMovement = other.gameObject.GetComponent<Mov>();
            if(playerMovement != null)
            {
                playerMovement.Boost();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovement = other.gameObject.GetComponent<Mov>();
            if (playerMovement != null)
            {
                Invoke("ApagarTur", 2f);
            }
        }
    }
    private void ApagarTur()
    {
        playerMovement.ReiniciarVelocidad();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in Effects)
            {
                GameObject a = Instantiate(obj, other.transform.position, other.transform.rotation);
                a.transform.parent = other.transform;
            }
        }
    }
}
