using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAiming : MonoBehaviour {

    public float turnSpeed = 50;
    public float aimDuration = 0.1f;
    public Rig aimLayer;
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

    void Update () {
        if (Input.GetButton("Fire1") || Input.GetButton("Fire2")) {
            aimLayer.weight += Time.deltaTime / aimDuration;
        } else {
            aimLayer.weight -= Time.deltaTime / aimDuration;
        }
    }
}
