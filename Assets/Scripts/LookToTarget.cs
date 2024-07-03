using UnityEngine;

public class LookToTarget : MonoBehaviour
{
    [SerializeField] Transform target;

    private void Update()
    {
        if (target != null)
        LookTarget();
    }

    void LookTarget()
    {
        Vector3 direction = target.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 2.5f * Time.deltaTime);
    }
}
