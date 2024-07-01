using UnityEngine;

public class OndaExpansiva : MonoBehaviour
{
    public float velocidadCrecimiento = 5f;
    public float duracionExplosion = 1f;

    [SerializeField] float explosion;
    private float tiempoInicioExplosion;

    void Start()
    {
        tiempoInicioExplosion = Time.time;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Turbo a = other.GetComponent<Turbo>();
                 a.GestionarEnergia(explosion);
        }
    }

    void Update()
    {
        float tiempoTranscurrido = Time.time - tiempoInicioExplosion;
        float tamañoActual = tiempoTranscurrido * velocidadCrecimiento;

        transform.localScale = new Vector3(tamañoActual, tamañoActual, tamañoActual);

        if (tiempoTranscurrido >= duracionExplosion)
        {
            Destroy(gameObject);
        }
    }
}
