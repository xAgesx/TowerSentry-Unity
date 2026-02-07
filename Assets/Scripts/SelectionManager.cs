using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour {
    private HoverDisplay currentHover;
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
            HoverDisplay unit = hit.collider.GetComponent<HoverDisplay>();

            if (unit != currentHover) {
                if (currentHover != null) {
                    currentHover.SetOverlayVisible(false);
                    currentHover.SetStatsVisible(false);
                }
                currentHover = unit;
                if (currentHover != null) {
                    currentHover.SetOverlayVisible(true);
                    currentHover.SetStatsVisible(true);

                }
            }
        } else {
            if (currentHover != null) {
                currentHover.SetOverlayVisible(false);
                currentHover.SetStatsVisible(false);

                currentHover = null;
            }
        }
    }
}