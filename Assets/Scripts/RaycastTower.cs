using UnityEngine;

public class RaycastTower : MonoBehaviour
{
    public float range = 5f;
    public float damage = 10f;
    public float attacksPerSecond = 1f;
    public LayerMask detectionLayers;

    private float attackTimer;

    void Awake()
    {
        detectionLayers = LayerMask.GetMask("Enemy");
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            TryAttack();
            attackTimer = 1f / attacksPerSecond;
        }
    }

    void TryAttack()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            range,
            detectionLayers
        );

        if (hits.Length == 0)
            return;

        EnemyHealth enemy = hits[0].GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
