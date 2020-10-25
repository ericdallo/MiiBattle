using UnityEngine;

public class PlayerAiming : MonoBehaviour {

    public float turnSpeed = 15;
    Camera mainCamera;

    void Start() {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate() {
        float rotationYCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, rotationYCamera, 0), turnSpeed * Time.deltaTime);
    }
}
