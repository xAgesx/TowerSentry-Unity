using UnityEngine;

[CreateAssetMenu(fileName = "Entity", menuName = "Scriptable Objects/Entity")]
public class Entity : ScriptableObject
{

    [Header("Properties")]
    public string type;
    public float maxHP;
    public float movementSpeed;
    public float damage;
    public float attackRate;
    public float attackRange;
    public GameObject prefab;
    public int cost;
    public Sprite img;
}
