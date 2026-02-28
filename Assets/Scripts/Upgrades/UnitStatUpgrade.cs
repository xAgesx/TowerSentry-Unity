using UnityEngine;
[CreateAssetMenu(fileName = "StatUpgrade", menuName = "Scriptable Objects/UnitStatUpgrade")]
public class UnitStatUpgrade : Upgrades {
    public Entity entity;
    public override void ApplyEffect() {
        switch (stat) {
            case statType.damage : entity.damage += statIncrease;
                break;
        }
    }
}
