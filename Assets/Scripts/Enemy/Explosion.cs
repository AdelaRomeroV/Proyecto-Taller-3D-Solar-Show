using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float TimeCounter = 3;


    [Header("Ataque")]
    public float checkRadius;
    public LayerMask whatIsPlayer;

    public GameObject fxExplosion;

    [SerializeField] private Renderer cocheRenderer;

    private void Start()
    {
        Invoke("Bomba",20f);
    }

    [SerializeField] private GameObject fxExplosionPrefab;
    private void Update()
    {
        if (TimeCounter <= 0)
        {
            Bomba();
        }
    
        if (detection())
        {
        TimeCounter -= Time.deltaTime;
            StartCoroutine(Parpadeo());
        }
    }

    private bool detection()
    {
        return Physics.CheckSphere(transform.position, checkRadius, whatIsPlayer);

    }
    public void Muerte()
    {
        Instantiate(fxExplosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    public void Bomba()
    {
        Instantiate(fxExplosionPrefab, transform.position, transform.rotation);
       // SonidoExplosion a = fxExplosionPrefab.GetComponent<SonidoExplosion>();
       // a.Sonido();

        Instantiate(fxExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    IEnumerator Parpadeo()
    {
        cocheRenderer.material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.5f);
        cocheRenderer.material.DisableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.5f);
        cocheRenderer.material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.5f);
        cocheRenderer.material.DisableKeyword("_EMISSION");
    }
}
