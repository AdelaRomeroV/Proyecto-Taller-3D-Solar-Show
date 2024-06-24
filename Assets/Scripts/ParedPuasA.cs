using System.Collections;
using UnityEngine;

public class ParedPuasA : MonoBehaviour
{
    public Transform target;
    public Transform targetregreso;

    public float moveSpeed;
    public bool isFalling = true;
    private Mov player;
    public bool inicio;
    public float moveSpeedDePrevencion;
    public float moveSpeedDeCaida;

    [SerializeField] ParticleSystem Particulas;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) { player = playerObject.GetComponent<Mov>(); }
    }

    public void IniciarCorutina()
    {
        StartCoroutine(CrushRoutine());
    }

    private void Update()
    {
        if (transform.position == targetregreso.position) { inicio=true; }
        if (transform.position == target.position) { isFalling = false; }
    }

    private IEnumerator CrushRoutine()
    {
        yield return new WaitForSeconds(1f);
        isFalling = true;
        float timeFalling = 0f;
        while (isFalling)
        {
            if (timeFalling >= 1f)
            {
                Forward(moveSpeedDeCaida);
            }
            else
            {
                Forward(moveSpeedDePrevencion);
            }

            inicio = false;
            timeFalling += Time.deltaTime;
            yield return null;
        }

        while (!inicio)
        {
            Backward();
            yield return null;
        }
    }

    private void Backward()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetregreso.position, moveSpeed * Time.deltaTime);
    }

    private void Forward(float move)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, move * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            {
                player.velocidadActual = 0;
                player.rb.velocity = Vector3.zero;
                player.onStun = true;
                player.Invoke("OffStun", 1f);
                StartCoroutine(DesactivarColision());
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hazards") || other.CompareTag("Player"))
        {
            isFalling = false;
            Particulas.Play();
        }
    }

    private IEnumerator DesactivarColision()
    {
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
        yield return new WaitForSeconds(2f);
        collider.isTrigger = false;
    }
}
