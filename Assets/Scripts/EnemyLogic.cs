using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic: MonoBehaviour
{
    [Header("Stats")]
    [Header("Stats")]
    [SerializeField] private EnemyStats stats;

    [Header("Targeting")]
    public float aggroRange = 6f;
    public LayerMask unitLayer;

    private NavMeshAgent agent;
    private Transform tower;
    private RaycastTower towerHealth;

    private Transform currentTarget;
    private float attackTimer;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        GameObject towerObj = GameObject.FindGameObjectWithTag("Tower");
        if (towerObj == null)
        {
            Debug.LogError("No GameObject tagged 'Tower' found");
            enabled = false;
            return;
        }

        tower = towerObj.transform;
        towerHealth = tower.GetComponent<RaycastTower>();

        agent.stoppingDistance = stats.attackRange;
        agent.SetDestination(tower.position);
    }

    void Update()
    {
        SelectTarget();
        MoveAndAttack();
    }

    // ---------------- TARGET SELECTION ----------------

    void SelectTarget()
    {
        Collider[] unitsInRange = Physics.OverlapSphere(
            transform.position,
            aggroRange,
            unitLayer
        );

        if (unitsInRange.Length > 0)
        {
            currentTarget = GetClosestTarget(unitsInRange);
        }
        else
        {
            currentTarget = tower;
        }
    }

    Transform GetClosestTarget(Collider[] targets)
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider col in targets)
        {
            float dist = Vector3.Distance(transform.position, col.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = col.transform;
            }
        }

        return closest;
    }

    // ---------------- MOVEMENT + ATTACK ----------------

    void MoveAndAttack()
    {
        if (currentTarget == null)
            return;

        float distance = Vector3.Distance(transform.position, currentTarget.position);

        if (distance > stats.attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(currentTarget.position);
        }
        else
        {
            agent.isStopped = true;
            AttackCurrentTarget();
        }
    }

    void AttackCurrentTarget()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer > 0f)
            return;

        // Attack UNIT
        if (currentTarget.CompareTag("Unit"))
        {
            TroopLogic unitHealth = currentTarget.GetComponent<TroopLogic>();
            if (unitHealth != null)
                unitHealth.TakeDamage(stats.damage);
        }
        // Attack TOWER
        else if (currentTarget.CompareTag("Tower"))
        {
            towerHealth.TakeDamage(stats.damage);
        }

        attackTimer = 1f / stats.damagePerSecond;
    }

    // ---------------- DEBUG ----------------

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);
    }
}
