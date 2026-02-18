using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour {
    private HoverDisplay currentHover;
    private Camera mainCam;
    public bool isVisible = false;
    private HoverDisplay selectedUnit;

    void Start() {
        mainCam = Camera.main;
    }

    void Update() {
        FollowMouseWithRay();
    }

    void FollowMouseWithRay() {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = mainCam.ScreenPointToRay(mousePos);
        bool hitSomething = Physics.Raycast(ray, out RaycastHit hit);

        HoverDisplay unitUnderMouse = hitSomething ? hit.collider.GetComponent<HoverDisplay>() : null;
        if (unitUnderMouse != currentHover) {
            // Hide old hover UI ONLY if it's not the selected unit
            if (currentHover != null && currentHover != selectedUnit) {
                currentHover.SetOverlayVisible(false);
                currentHover.SetStatsVisible(false);
            }

            currentHover = unitUnderMouse;

            // Show new hover UI
            if (currentHover != null) {
                currentHover.SetOverlayVisible(true);
                currentHover.SetStatsVisible(true);
            }
        }


        if (Mouse.current.leftButton.wasPressedThisFrame) {
            if (unitUnderMouse != null) {
                // Deselect old unit
                if (selectedUnit != null && selectedUnit != unitUnderMouse) {
                    selectedUnit.SetOutline(false);
                    selectedUnit.SetStatsVisible(false);
                }

                // Select new unit
                selectedUnit = unitUnderMouse;
                selectedUnit.SetOutline(true); 
                selectedUnit.SetStatsVisible(true);
            } else {
                // Clicked empty ground: hide everything
                if (selectedUnit != null) {
                    selectedUnit.SetOutline(false);
                    selectedUnit.SetStatsVisible(false);
                    selectedUnit = null;
                }
            }
        }
    }
}