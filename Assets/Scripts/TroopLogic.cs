using System;
using UnityEngine;
using UnityEngine.AI;

public class TroopLogic : MonoBehaviour {
    [Header("Troop Properties")]
    public Entity troopData;
    public NavMeshAgent agent;
    public Transform target;
    public float nextAttackTime;
    public float HP;
    [Header("References")]
    public EntitiesManager em;
    void Start() {
        HP = troopData.maxHP;
        target = null;
        try {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = troopData.movementSpeed;
            agent.stoppingDistance = troopData.attackRange - 0.2f;
        }catch(Exception e) {
            Debug.Log("Error with MavMeshAgent : "+e.Message);
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
        
    }
    void Move(Vector3 pos) {
        agent.isStopped = false;
        agent.SetDestination(target.position);
    }
    Transform CheckForClosestEnemy() {
        if(em.enemies.Count == 0) {
            return null;
        } else {
            GameObject closestEnemy = em.enemies[0];
            float minDistance = Vector3.Distance(transform.position,em.enemies[0].transform.position);
            foreach(GameObject entity in em.enemies) {
                float distance = Vector3.Distance(transform.position,entity.transform.position);
               if(distance < minDistance) {
                    minDistance = distance;
                } 
            }
            return closestEnemy.transform;
        }
    }
}
