using UnityEngine;
using System.Collections;

public class RaycastTower : MonoBehaviour
{
    public float range = 5f;
    public float damage = 10f;
    public float attacksPerSecond = 1f;
    public LayerMask detectionLayers;

    public ParticleSystem shootEffect;   // PREFAB or child template
    public float particleSpeed = 10f;

    private float attackTimer;

    void Awake()
    {
        detectionLayers = LayerMask.GetMask("Enemy");

        if (shootEffect == null)
            shootEffect = GetComponentInChildren<ParticleSystem>();
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
        if (enemy == null)
            return;

        enemy.TakeDamage(damage);

        ShootParticle(enemy.transform);
    }

    void ShootParticle(Transform target)
    {
        // Create a new particle instance
        ParticleSystem ps = Instantiate(
            shootEffect,
            shootEffect.transform.position,
            Quaternion.identity
        );

        ps.gameObject.SetActive(true);
        ps.transform.LookAt(target);
        ps.Play();

        StartCoroutine(MoveParticle(ps.transform, target.position));
    }

    IEnumerator MoveParticle(Transform particle, Vector3 targetPos)
    {
        while (particle != null &&
               Vector3.Distance(particle.position, targetPos) > 0.1f)
        {
            particle.position = Vector3.MoveTowards(
                particle.position,
                targetPos,
                particleSpeed * Time.deltaTime
            );

            yield return null;
        }

        if (particle != null)
            Destroy(particle.gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
