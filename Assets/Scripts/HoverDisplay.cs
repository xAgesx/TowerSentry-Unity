using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class HoverDisplay : MonoBehaviour {
    [Header("UI Components")]
    public GameObject healthCanvas;
    public GameObject statsWindow;
    private GameObject statsUI;
    public TextMeshProUGUI hpText;

    private EntityLogic healthScript;
    

    void Awake() {
        healthScript = GetComponent<EntityLogic>();
        statsWindow = GameManager.instance.statsUI;
        statsUI = statsWindow.transform.Find("Stats").gameObject;
    }

    public void SetOverlayVisible(bool visible) {
        if (healthCanvas.activeSelf == visible) return; 

        healthCanvas.SetActive(visible);
        if (visible) UpdateUI();
    }
    public void SetStatsVisible(bool visible) {
        if(statsWindow.activeSelf == visible) return;
        
        statsWindow.SetActive(visible);

        
        var entityIMG = statsWindow.transform.GetChild(0).GetComponent<Image>();
        if(healthScript.stats.img != null) entityIMG.sprite = healthScript.stats.img;

        
        statsUI.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.stats.maxHP.ToString();
        statsUI.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.stats.damage.ToString();
        statsUI.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.stats.movementSpeed.ToString();
        statsUI.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.stats.attackRate.ToString();
        statsUI.transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.stats.attackRange.ToString();
        statsUI.transform.GetChild(5).GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.stats.maxHP.ToString();
        

    }
    void UpdateUI() {
        if (healthScript != null && hpText != null) {

            float percent = (healthScript.HP / healthScript.stats.maxHP) * 100;
            hpText.text = $"{Mathf.RoundToInt(percent)}%";
        }
    }
}