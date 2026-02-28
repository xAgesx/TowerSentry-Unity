using UnityEngine;
[CreateAssetMenu(fileName = "StatUpgrade", menuName = "Scriptable Objects/UnitStatUpgrade")]
public class UnitStatUpgrade : Upgrades {
    public Entity entity;
    public override void ApplyEffect() {
        switch (stat) {
            case statType.damage : entity.damage += statIncrease;
                break;
            case statType.maxHP : entity.maxHP += statIncrease;
                break;
            case statType.movementSpeed : entity.movementSpeed += statIncrease;
                break;
            case statType.attackRate : entity.attackRate += statIncrease;
                break;
            case statType.attackRange : entity.attackRange += statIncrease;
                break;
            
        }
    }
}
