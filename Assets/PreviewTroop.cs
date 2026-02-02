
using UnityEngine;
using UnityEngine.InputSystem;

public class PreviewTroop : MonoBehaviour {
    [Header("Prefabs&Materials")]
    public Material previewMaterial;
    public Material errorPreviewMaterial;
    public GameObject previewPrefab;


    [Header("Properties")]
    public float range = 15f;
    public Vector3 baseOffset ;
    public LayerMask mask;

    [Header("Comps")]
    public TroopSpawn troopSpawn;
    void Start() {
        troopSpawn = GameObject.FindFirstObjectByType<EntitiesManager>().GetComponent<TroopSpawn>();
    }
    void Update() {
        if(!troopSpawn.isPreview)Destroy(gameObject);
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask)) {
        transform.position = hit.point + baseOffset;

        float distance = Vector3.Distance(transform.position, Vector3.zero);

        if (distance > range) {
            GetComponent<Renderer>().material = errorPreviewMaterial;
        } else {
            GetComponent<Renderer>().material = previewMaterial;
        }
    }
        
    }
}
