using UnityEngine;
[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/Upgrades")]
public class Upgrades : ScriptableObject {
    public string upgradeName;
    public int goldCost;
    public Sprite icon;
    public UpgradeType type;

    public enum UpgradeType { UnlockUnit, TowerStat, UnitStat }
}
