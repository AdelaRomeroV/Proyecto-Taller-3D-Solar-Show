using UnityEngine;

public class Bumpers : MonoBehaviour
{
    public float pushForce;
    private Mov player;

    private void Awake()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) { player = playerObject.GetComponent<Mov>(); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody autoRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (autoRigidbody != null)
            {
                Vector3 pushDirection = -collision.transform.forward;
                pushDirection.Normalize();
                autoRigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);

                player.velocidadActual = 0;
                player.onStun = true;
                Invoke("OffStun", 0.75f);
            }
        }
    }

    void OffStun()
    {
        player.onStun = false;
    }
}
