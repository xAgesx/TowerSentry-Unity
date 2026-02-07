using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TroopLogic : EntityLogic {
    [Header("Troop Properties")]
    public NavMeshAgent agent;
    public Transform target;
    public float nextAttackTime;
    [Header("References")]
    public EntitiesManager em;
    private float attackTimer;

    void Start() {
        HP = stats.maxHP;
        target = null;
        try {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = stats.movementSpeed;
            agent.stoppingDistance = stats.attackRange - 0.2f;
        } catch (Exception e) {
            Debug.Log("Error with MavMeshAgent : " + e.Message);
        }
        try {
            em = FindAnyObjectByType<EntitiesManager>();
        } catch (Exception e) {
            Debug.Log("Error with EntitiesManager : " + e.Message);
        }
    }

    void Update() {
        target = CheckForClosestEnemy();

        if (target == null)
        {
            agent.isStopped = true;
            Debug.Log("Idle");
        }
        else {
            Move(target.transform.position);
        }
            

        if(target != null)DrawTargetLine();
    }
    void Move(Vector3 pos) {

        
        
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > stats.attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
        else
        {
            agent.isStopped = true;
            AttackCurrentTarget();
        }
        
    }
    Transform CheckForClosestEnemy() {

        if (em.enemies.Count == 0) {
            return null;
        } else {
            GameObject closestEnemy = em.enemies[0];
            float minDistance = Vector3.Distance(transform.position, em.enemies[0].transform.position);
            foreach (GameObject entity in em.enemies) {
                float distance = Vector3.Distance(transform.position, entity.transform.position);
                if (distance < minDistance) {
                    minDistance = distance;
                    closestEnemy = entity;
                }
            }
            return closestEnemy.transform;
        }
    }


    void AttackCurrentTarget()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer > 0f)
            return;

        if (target == null)
            return;

        // Attack enemy
        if (target.CompareTag("Enemy"))
        {
            EnemyLogic enemy = target.GetComponent<EnemyLogic>();
            if (enemy != null)
            {
                enemy.TakeDamage(stats.damage);
                Debug.Log("Unit is hitting");
            }
                

        }

        attackTimer = 1f / stats.attackRate;
    }


    public void TakeDamage(float damage) {

        HP -= damage;
        Debug.Log("Unit HP: " + HP);

        if (HP <= 0f)
        {
            Destroy(gameObject);
        }

    }

    public void DrawTargetLine() {
        Debug.DrawLine(transform.position,target.transform.position);
    }
}
