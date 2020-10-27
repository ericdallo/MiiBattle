using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAiming : MonoBehaviour {

    public float turnSpeed = 50;
    public float aimDuration = 0.15f;
    public Rig aimLayer;

    Camera mainCamera;
    RaycastWeapon weapon;

    void Start() {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weapon = GetComponentInChildren<RaycastWeapon>();
    }

    void FixedUpdate() {
        float rotationYCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, rotationYCamera, 0), turnSpeed * Time.deltaTime);


    }

    void Update() {
        if (Input.GetButton("Fire2")) {
            aimLayer.weight += Time.deltaTime / aimDuration;
        } else {
            aimLayer.weight -= Time.deltaTime / aimDuration;
        }

        if (Input.GetButton("Fire1")) {
            weapon.StartFiring();
        } else {
            weapon.StopFiring();
        }
    }
}
