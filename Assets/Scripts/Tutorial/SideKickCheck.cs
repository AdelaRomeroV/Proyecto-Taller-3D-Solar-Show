using UnityEngine;

public class SideKickCheck : MonoBehaviour
{
    EnemiesTutorial e;
    [SerializeField] LayerMask enemigo;

    private void Update()
    {
        Collider[] collision = Physics.OverlapSphere(transform.position, 3.5f, enemigo);

        if (collision.Length > 0)
        {
            e = collision[0].GetComponent<EnemiesTutorial>();
            if (e != null)
            {
                e.destroyShip();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 3.5f);
    }
}
