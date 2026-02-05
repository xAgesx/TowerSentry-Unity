using UnityEngine;
using TMPro;

public class HoverHealthDisplay : MonoBehaviour {
    [Header("UI Components")]
    public GameObject healthCanvas;
    public TextMeshProUGUI hpText;

    private TroopLogic healthScript;

    void Awake() {
        healthScript = GetComponent<TroopLogic>();
    }

    public void SetOverlayVisible(bool visible) {
        if (healthCanvas.activeSelf == visible) return; 

        healthCanvas.SetActive(visible);
        if (visible) UpdateUI();
    }

    void UpdateUI() {
        if (healthScript != null && hpText != null) {

            float percent = (healthScript.HP / healthScript.troopData.maxHP) * 100;
            hpText.text = $"{Mathf.RoundToInt(percent)}%";
        }
    }
}