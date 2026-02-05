using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour {
    private HoverHealthDisplay currentHover;
    private Camera mainCam;

    void Start() {
        mainCam = Camera.main;
    }

    void Update() {
        FollowMouseWithRay();
    }

    void FollowMouseWithRay() {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = mainCam.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit)) {
            HoverHealthDisplay unit = hit.collider.GetComponent<HoverHealthDisplay>();

            if (unit != currentHover) {
                if (currentHover != null) currentHover.SetOverlayVisible(false);
                currentHover = unit;
                if (currentHover != null) currentHover.SetOverlayVisible(true);
            }
        } else {
            if (currentHover != null) {
                currentHover.SetOverlayVisible(false);
                currentHover = null;
            }
        }
    }
}