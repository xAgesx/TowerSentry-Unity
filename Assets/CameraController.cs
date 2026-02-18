using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour {
    [Header("Movement")]
    public float moveSpeed = 20f;
    public float dragSpeed = 0.1f;

    [Header("Zoom Settings")]
    public float zoomSpeed = 0.05f;
    public float minHeight = 2f;
    public float maxHeight = 20f;

    [Header("Angle Settings")]
    public float minAngle = 15f;
    public float maxAngle = 65f;

    public PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction zoomAction;
    private InputAction dragAction;
    private InputAction lookAction;

    public float currentZoom = 0.5f;

    void Awake() {

        moveAction = playerInput.actions["Move"];
        zoomAction = playerInput.actions["Zoom"];
        dragAction = playerInput.actions["Drag"];
        lookAction = playerInput.actions["Look"];
    }

    void Update() {
        HandleMovement();
        HandleZoom();
    }

    void HandleMovement() {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0, input.y);

        if (dragAction.IsPressed()) {
            Vector2 mouseDelta = lookAction.ReadValue<Vector2>();
            direction.x -= mouseDelta.x * dragSpeed;
            direction.z -= mouseDelta.y * dragSpeed;
        }

        transform.Translate(moveSpeed * Time.deltaTime * direction, Space.World);
    }

    void HandleZoom() {
        float scrollValue = zoomAction.ReadValue<Vector2>().y;

        if (Mathf.Abs(scrollValue) > 0.01f ) {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                Vector3 direction = hit.point - transform.position;
                float zoomAmount = (scrollValue > 0) ? zoomSpeed : -zoomSpeed;

                if(zoomAmount < 0 && transform.rotation.eulerAngles.x >= maxAngle) return;

                Vector3 newPosition = transform.position + direction * zoomAmount;
                newPosition.y = Mathf.Clamp(newPosition.y, minHeight, maxHeight);

                transform.position = newPosition;

                float t = (transform.position.y - minHeight) / (maxHeight - minHeight);
                transform.rotation = Quaternion.Euler(Mathf.Lerp(20, 60, t), transform.eulerAngles.y, 0);
            }
        }
    }
}