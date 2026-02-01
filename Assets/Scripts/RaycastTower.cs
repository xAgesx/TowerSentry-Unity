using UnityEngine;
using System.Collections;

public class RaycastTower : MonoBehaviour
{
    [Header("Attack")]
    public float range = 5f;
    public float damage = 10f;
    public float attacksPerSecond = 1f;
    private float attackTimer;
    public LayerMask detectionLayers;

    [Header("Particles")]
    public ParticleSystem shootEffect;   // PREFAB or child template
    public float particleSpeed = 10f;

    [Header("Lazer")]
    public LineRenderer laser;
    public float laserDuration = 0.1f;

    [Header("Health point")]
    public float maxHP = 200f;
    private float currentHP;

    void Awake()
    {
        detectionLayers = LayerMask.GetMask("Enemy");

        if (shootEffect == null)
            shootEffect = GetComponentInChildren<ParticleSystem>();
        currentHP = maxHP;
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

    // Tower Attack

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

        ShootLaser(enemy.transform);
    }

    // Shooting lazer

    void ShootLaser(Transform target)
    {
        StopAllCoroutines();
        StartCoroutine(LaserRoutine(target));
    }

    IEnumerator LaserRoutine(Transform target)
    {
        laser.enabled = true;

        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, target.position);

        yield return new WaitForSeconds(laserDuration);

        laser.enabled = false;
    }

    // Tower Taking dmg + dies

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        Debug.Log("Tower HP: " + currentHP);

        if (currentHP <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Tower destroyed");
        Destroy(gameObject);
    }


    /// <summary>
    /// Particle system for later
    /// </summary>

    /*void ShootParticle(Transform target)
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
    }*/

    // Draw the range of the tower
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
