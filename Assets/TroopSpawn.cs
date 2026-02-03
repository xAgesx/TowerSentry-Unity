using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class TroopSpawn : MonoBehaviour {

    [Header("Prefabs&Materials")]
    public Material previewMaterial;
    public Material errorPreviewMaterial;
    public GameObject previewPrefab;

    [Header("Properties")]
    public float range = 15f;
    public bool isPreview = false;
    GameObject previewObj;
    EntitiesManager entitiesManager;
    public int selectedTroopIndex = -1;
    void Start() {
        entitiesManager = GetComponent<EntitiesManager>();
    }
    void Update() {
        if (previewObj == null) return;
        if(selectedTroopIndex == -1) return;

        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Debug.Log("Spawned Troop");
            if (getDistance(previewObj.transform.position, Vector3.zero) <= range) {
                Instantiate(entitiesManager.troopPrefabs[selectedTroopIndex], previewObj.transform.position, quaternion.identity);
            }
        } else if (Mouse.current.rightButton.wasPressedThisFrame) {
            isPreview = false;
            selectedTroopIndex = -1;
        }
    }
    public void PreviewUnit(int index) {
        if(selectedTroopIndex == index || selectedTroopIndex == -1)  isPreview = !isPreview;
        selectedTroopIndex = index;
        if (isPreview) {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            previewObj = Instantiate(previewPrefab, worldPos + new Vector3(0, -35, 0), quaternion.identity);
            previewObj.GetComponent<Renderer>().material = previewMaterial;
            previewObj.GetComponent<MeshFilter>().sharedMesh = entitiesManager.troopPrefabs[selectedTroopIndex].GetComponent<MeshFilter>().sharedMesh;
        }

    }
    public float getDistance(Vector3 pos1, Vector3 pos2) {
        return Vector3.Distance(pos1, pos2);

    }
    

}
