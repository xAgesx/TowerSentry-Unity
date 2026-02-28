using UnityEngine;

public class ShopManager : MonoBehaviour {
    public int currentGold;

    public void BuyUpgrade(Upgrades data) {
        currentGold = GameManager.instance.Gold;
        if (currentGold >= data.goldCost) {
            currentGold -= data.goldCost;
            ApplyEffect(data);
            Debug.Log("Bought: " + data.upgradeName);
        } else {
            Debug.Log("Not enough gold!");
        }
    }

    void ApplyEffect(Upgrades data) {
        data.ApplyEffect();
    }
}