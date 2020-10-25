using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float jumpHeight = 3;
    public float gravity = 19.87f;
    public float stepDown = 0.1f;
    public float airControl = 2.5f;
    public float jumpDamp = 0.5f;
    public float groundSpeed = 1.2f;

    public float pushPower = 2.0f;

    CharacterController controller;
    Animator animator;

    Vector2 input;
    bool isRunning = false;
    Vector3 rootMotion;
    Vector3 velocity;
    bool isJumping = false;

    void Start() {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        input = Vector3.zero;
        isRunning = Input.GetButton("Sprint");

        input.y = Input.GetAxis("Vertical");
        input.x = Input.GetAxis("Horizontal");

        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.y);
        animator.SetBool("Running", isRunning);

        if (Input.GetButton("Jump")) {
            Jump();
        }
    }

    void FixedUpdate() {
        if (isJumping) {
            UpdateInAir();
            return;
        }

        UpdateOnGround();
    }

    void OnAnimatorMove() {
        rootMotion += animator.deltaPosition;
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3f)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        body.velocity = pushDir * pushPower;
    }

    private void UpdateOnGround() {
        Vector3 stepForwardAmount = rootMotion * groundSpeed;
        Vector3 stepDownAmount = Vector3.down * stepDown;

        controller.Move(stepForwardAmount + stepDownAmount);
        rootMotion = Vector3.zero;

        if (!controller.isGrounded) {
            SetInAir(0);
        }
    }

    private void UpdateInAir() {
        velocity.y -= gravity * Time.fixedDeltaTime;
        Vector3 displacement = velocity * Time.fixedDeltaTime;
        displacement += CalculateAirControl();

        controller.Move(displacement);
        isJumping = !controller.isGrounded;
        rootMotion = Vector3.zero;
        animator.SetBool("Jumping", isJumping);
    }

    private void Jump() {
        if (isJumping) {
            return;
        }
        float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
        SetInAir(jumpVelocity);
    }

    private void SetInAir(float jumpVelocity) {
        isJumping = true;
        velocity = animator.velocity * jumpDamp * groundSpeed;
        velocity.y = jumpVelocity;
        animator.SetBool("Jumping", true);
    }

    private Vector3 CalculateAirControl() {
        return ((transform.forward * input.y) + (transform.right * input.x)) * (airControl / 100);
    }
}
