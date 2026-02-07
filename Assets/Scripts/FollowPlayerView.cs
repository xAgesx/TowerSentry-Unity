using Unity.Mathematics;
using UnityEngine;

public class FollowPlayerView : MonoBehaviour {
    void Update() {
        transform.LookAt(Camera.main.transform.forward + transform.position );
    }
}
