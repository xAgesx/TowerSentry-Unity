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
    void Start() {
        entitiesManager = GetComponent<EntitiesManager>();
    }
    void Update() {
        if (previewObj == null) return;


        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Debug.Log("Spawned Troop");
            if (getDistance(previewObj.transform.position, Vector3.zero) <= range) {
                Instantiate(entitiesManager.troopPrefabs[0], previewObj.transform.position, quaternion.identity);
            }
        } else if (Mouse.current.rightButton.wasPressedThisFrame) {
            isPreview = false;
        }
    }
    public void PreviewUnit() {
        isPreview = !isPreview;
        if (isPreview) {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            previewObj = Instantiate(previewPrefab, worldPos + new Vector3(0, -35, 0), quaternion.identity);
            previewObj.GetComponent<Renderer>().material = previewMaterial;
        }

    }
    public float getDistance(Vector3 pos1, Vector3 pos2) {
        return Vector3.Distance(pos1, pos2);

    }

}
