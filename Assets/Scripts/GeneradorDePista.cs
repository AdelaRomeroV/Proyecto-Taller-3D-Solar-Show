using UnityEngine;

public class GeneradorDePista : MonoBehaviour
{
    public Transform Jugador;

    public virtual void GenerarPista(GameObject Pista)
    {
        if (Vector3.Distance(Jugador.position, transform.position) <= 220)
        {
            Instantiate(Pista, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    
}
