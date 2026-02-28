using UnityEngine;

public abstract class Upgrades : ScriptableObject {
    public string upgradeName;
    public int goldCost;
    public Sprite icon;
    public UpgradeType type;
    public statType stat;
    public float statIncrease;
    public enum statType {damage, attackRate, movementSpeed, attackRange, maxHP}
    public enum UpgradeType { UnlockUnit, TowerStat, UnitStat }
    public abstract void ApplyEffect();
}
