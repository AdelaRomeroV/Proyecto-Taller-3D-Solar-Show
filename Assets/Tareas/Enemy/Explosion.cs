using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float TimeCounter = 3;


    [Header("Ataque")]
    public float checkRadius;
    public LayerMask whatIsPlayer;

    public GameObject fxExplosion;

    private void Update()
    {
        if (TimeCounter <= 0)
        {
            Bomba();
        }
    
        if (detection())
        {
        TimeCounter -= Time.deltaTime;
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
        Instantiate(fxExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
