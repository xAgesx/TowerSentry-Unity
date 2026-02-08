using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : EntityLogic {
    [Header("Targeting")]
    public float aggroRange = 6f;
    public LayerMask unitLayer;

    private NavMeshAgent agent;
    private Transform tower;
    private RaycastTower towerHealth;

    private Transform currentTarget;
    private float attackTimer;
    public EntitiesManager em;

    public Animator animator;
    public bool isDead = false;

    void Awake() {
        HP = stats.maxHP;
        em = FindAnyObjectByType<EntitiesManager>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start() {
        GameObject towerObj = GameObject.FindGameObjectWithTag("Tower");
        if (towerObj == null) {
            Debug.LogError("No GameObject tagged 'Tower' found");
            enabled = false;
            return;
        }

        tower = towerObj.transform;
        towerHealth = tower.GetComponent<RaycastTower>();

        agent.stoppingDistance = stats.attackRange;
        agent.SetDestination(tower.position);
    }

    void Update() {
        SelectTarget();
        MoveAndAttack();
    }

    // ---------------- TARGET SELECTION ----------------

    void SelectTarget() {
        Collider[] unitsInRange = Physics.OverlapSphere(
            transform.position,
            aggroRange,
            unitLayer
        );

        if (unitsInRange.Length > 0) {
            currentTarget = GetClosestTarget(unitsInRange);
        } else {
            currentTarget = tower;
        }
    }

    Transform GetClosestTarget(Collider[] targets) {
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider col in targets) {
            float dist = Vector3.Distance(transform.position, col.transform.position);
            if (dist < minDistance) {
                minDistance = dist;
                closest = col.transform;
            }
        }

        return closest;
    }

    // ---------------- MOVEMENT + ATTACK ----------------

    void MoveAndAttack() {
        if (currentTarget == null)
            return;

        float distance = Vector3.Distance(transform.position, currentTarget.position);

        if (distance > stats.attackRange) {
            agent.isStopped = false;
            agent.SetDestination(currentTarget.position);
        } else {
            agent.isStopped = true;
            animator.SetTrigger("Attack");
            AttackCurrentTarget();
        }
    }

    
    void AttackCurrentTarget() {
        attackTimer -= Time.deltaTime;

        if (attackTimer > 0f)
            return;

        // Attack UNIT
        if (currentTarget.CompareTag("Unit")) {
            TroopLogic unitHealth = currentTarget.GetComponent<TroopLogic>();
            if (unitHealth != null)
                unitHealth.TakeDamage(stats.damage);
        }
        // Attack TOWER
        else if (currentTarget.CompareTag("Tower")) {
            towerHealth.TakeDamage(stats.damage);
        }

        attackTimer = 1f / stats.attackRate;
    }

    // ---------------- DEBUG ----------------

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);
    }

    // -------------- Health Management -----------------

    public void TakeDamage(float damage) {
        HP -= damage;
        Debug.Log(gameObject.name + " HP: " + HP);

        if (HP <= 0f && !isDead) {
            Die();
        }
    }

    void Die() {
        Debug.Log(gameObject.name + " died");
        FindAnyObjectByType<GameManager>().EnemyDied();
        em.enemies.Remove(gameObject);
        isDead= true;
        animator.SetBool("Dead", true);
        Destroy(gameObject,2f);

    }
}
