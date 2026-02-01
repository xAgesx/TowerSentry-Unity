using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic: MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private EnemyStats stats;

    private NavMeshAgent agent;
    private Transform tower;
    private RaycastTower towerHealth;
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
        if (tower == null)
            return;

        float distance = Vector3.Distance(transform.position, tower.position);

        if (distance <= stats.attackRange)
        {
            agent.isStopped = true;
            AttackTower();
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(tower.position);
        }
    }

    void AttackTower()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            towerHealth.TakeDamage(stats.damage);
            attackTimer = 1f / stats.damagePerSecond;
        }
    }
}
