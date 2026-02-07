using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;


public class HoverDisplay : MonoBehaviour {
    [Header("UI Components")]
    public GameObject healthCanvas;
    public GameObject statsWindow;
    private GameObject statsUI;
    public TextMeshProUGUI hpText;

    private TroopLogic healthScript;
    

    void Awake() {
        healthScript = GetComponent<TroopLogic>();
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
        if(healthScript.troopData.img != null) entityIMG.sprite = healthScript.troopData.img;

        
        statsUI.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.troopData.maxHP.ToString();
        statsUI.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.troopData.damage.ToString();
        statsUI.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.troopData.movementSpeed.ToString();
        statsUI.transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.troopData.attackRate.ToString();
        statsUI.transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.troopData.attackRange.ToString();
        statsUI.transform.GetChild(5).GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.troopData.maxHP.ToString();
        

    }
    void UpdateUI() {
        if (healthScript != null && hpText != null) {

            float percent = (healthScript.HP / healthScript.troopData.maxHP) * 100;
            hpText.text = $"{Mathf.RoundToInt(percent)}%";
        }
    }
}