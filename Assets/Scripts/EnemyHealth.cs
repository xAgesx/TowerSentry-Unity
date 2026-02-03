using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private EnemyStats stats;

    private float currentHP;
    public EntitiesManager em;
    void Awake() {
        currentHP = stats.maxHP;
        em = FindAnyObjectByType<EntitiesManager>();
    }

    public void TakeDamage(float damage) {
        currentHP -= damage;
        Debug.Log(gameObject.name + " HP: " + currentHP);

        if (currentHP <= 0f) {
            Die();
        }
    }

    void Die() {
        Debug.Log(gameObject.name + " died");
        em.enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}
