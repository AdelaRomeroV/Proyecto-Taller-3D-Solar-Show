using UnityEngine;

public class EnemiesTutorial : MonoBehaviour
{
    [SerializeField] GameObject ExplosionPartycles;
    [SerializeField] bool IsRight;

    EnemiesControllerTutorial controlador;
    private void Start()
    {
        controlador = GameObject.Find("EnemiesController").GetComponent<EnemiesControllerTutorial>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pared"))
        {
            if (IsRight) controlador.RightAlive = false;
            else controlador.LeftAlive = false;

            Instantiate(ExplosionPartycles, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }

    public void destroyShip()
    {
        if (IsRight) controlador.RightAlive = false;
        else controlador.LeftAlive = false;

        controlador.navesDestruidas++;
        Instantiate(ExplosionPartycles, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
