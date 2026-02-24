using UnityEngine;

public class ShopCardUI : MonoBehaviour {
    public Upgrades data;
    public TMPro.TextMeshProUGUI costText;
    public UnityEngine.UI.Image iconImage;

    void Start() {
        iconImage.sprite = data.icon;
        costText.text = data.goldCost.ToString();
    }

    public void OnClickBuy() {
        FindFirstObjectByType<ShopManager>().BuyUpgrade(data);
    }
}