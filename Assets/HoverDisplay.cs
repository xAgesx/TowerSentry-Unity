using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;


public class HoverDisplay : MonoBehaviour {
    [Header("UI Components")]
    public GameObject healthCanvas;
    public GameObject statsWindow;
    public List<GameObject> statsUI;
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
    public void SetStatsVisible(bool visible) {
        if(statsWindow.activeSelf == visible) return;
        
        statsWindow.SetActive(visible);

        
        var entityIMG = statsWindow.transform.GetChild(0).GetComponent<Image>();
        if(healthScript.troopData.img != null) entityIMG.sprite = healthScript.troopData.img;

        
        statsUI[0].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.troopData.maxHP.ToString();
        statsUI[1].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.troopData.damage.ToString();
        statsUI[2].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.troopData.movementSpeed.ToString();
        statsUI[3].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.troopData.attackRate.ToString();
        statsUI[4].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.troopData.attackRange.ToString();
        statsUI[5].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = healthScript.troopData.maxHP.ToString();
        

    }
    void UpdateUI() {
        if (healthScript != null && hpText != null) {

            float percent = (healthScript.HP / healthScript.troopData.maxHP) * 100;
            hpText.text = $"{Mathf.RoundToInt(percent)}%";
        }
    }
}