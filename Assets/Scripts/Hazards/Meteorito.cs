using UnityEngine;

public class Meteorito : MonoBehaviour
{
    private Mov player;
    private Rigidbody meteorRb;
    public float mixSpeed = 50;
    public float maxSpeed = 250;
    [SerializeField] private GameObject exit;

    private void Awake()
    {
        meteorRb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        meteorRb.velocity += Vector3.down * Random.Range(mixSpeed, maxSpeed); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Suelo"))
        {
            Destroy(gameObject, 2);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Mov>();
            if (player != null)
            {
                player.onStun = true;
                player.Invoke("OffStun", 0.75f);
                Destroy(gameObject);
            }
        }
    }
    private void OnDestroy()
    {
        Instantiate(exit,transform.position, transform.rotation);
    }
}
