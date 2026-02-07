using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private Entity stats;

    private float HP;
    public EntitiesManager em;
    void Awake() {
        HP = stats.maxHP;
        em = FindAnyObjectByType<EntitiesManager>();
    }

    public void TakeDamage(float damage) {
        HP -= damage;
        Debug.Log(gameObject.name + " HP: " + HP);

        if (HP <= 0f) {
            Die();
        }
    }

    void Die() {
        Debug.Log(gameObject.name + " died");
        FindAnyObjectByType<GameManager>().EnemyDied();
        em.enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}
