using UnityEngine;

public class CrosshairTarget : MonoBehaviour {

    Camera mainCamera;
    RaycastHit hitInfo;
    Ray ray;

    void Start() {
        mainCamera = Camera.main;
    }

    void Update() {
        ray.origin = mainCamera.transform.position;
        ray.direction = mainCamera.transform.forward;
        Physics.Raycast(ray, out hitInfo);
        transform.position = hitInfo.point;
    }
}
