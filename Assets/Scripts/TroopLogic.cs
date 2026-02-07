using System;
using UnityEngine;
using UnityEngine.AI;

public class TroopLogic : EntityLogic {
    [Header("Troop Properties")]
    public NavMeshAgent agent;
    public Transform target;
    public float nextAttackTime;
    [Header("References")]
    public EntitiesManager em;
    
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
        if (target == null) {
            agent.isStopped = true;
            Debug.Log("Idle");
        } else {
            Move(target.transform.position);
        }

        if(target != null) DrawTargetLine();
    }
    void Move(Vector3 pos) {
        agent.isStopped = false;
        agent.SetDestination(target.position);
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
