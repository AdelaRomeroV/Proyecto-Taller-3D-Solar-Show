using UnityEngine;

public class Acelerador : MonoBehaviour
{
    private Mov playerMovement;
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
}
