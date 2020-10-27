using UnityEngine;

public class RaycastWeapon : MonoBehaviour {

    public bool isFiring = false;
    public ParticleSystem muzzleFlash;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;
    public Transform raycastOrigin;
    public Transform raycastDestination;

    Ray ray;
    RaycastHit hitInfo;

    public void StartFiring() {
        isFiring = true;
        muzzleFlash.Emit(20);

        ray.origin = raycastOrigin.position;
        ray.direction = raycastDestination.position - raycastOrigin.position;

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if (Physics.Raycast(ray, out hitInfo)) {
            // Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 30.0f);
            tracer.transform.position = hitInfo.point;

            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);
        }
    }

    public void StopFiring() {
        isFiring = false;
    }
}
