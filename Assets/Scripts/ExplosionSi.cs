using System.Collections;
using UnityEngine;

public class ExplosionSi : MonoBehaviour
{
    [Header("Ataque")]
    public float checkRadius;
    public LayerMask whatIsPlayer;

    public GameObject fxExplosion;

    [SerializeField] private GameObject fxExplosionPrefab;
    private void Update()
    {    
        if (detection())
        {
            Bomba();
        }
    }

    private bool detection()
    {
        return Physics.CheckSphere(transform.position, checkRadius, whatIsPlayer);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    public void Bomba()
    {
        Instantiate(fxExplosionPrefab, transform.position, transform.rotation);
        SonidoExplosion a= fxExplosionPrefab.GetComponent<SonidoExplosion>();
        a.Sonido();

        Instantiate(fxExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
