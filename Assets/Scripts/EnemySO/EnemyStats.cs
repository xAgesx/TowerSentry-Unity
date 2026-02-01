using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [Header("Health")]
    public float maxHP = 100f;

    [Header("Attack")]
    public float damage = 10f;
    public float damagePerSecond = 1f;
    public float attackRange = 1.5f;
}
