using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntitiesManager : MonoBehaviour {

    [Header("Entity lists")]
    public List<GameObject> enemies;
    public List<GameObject> troopPrefabs;
    public int TroopLimit;
    public int currentTroops;

    public TextMeshProUGUI currentTroopsUI;


    void Update() {
        currentTroopsUI.text = currentTroops + " / " + TroopLimit;
    }
}
