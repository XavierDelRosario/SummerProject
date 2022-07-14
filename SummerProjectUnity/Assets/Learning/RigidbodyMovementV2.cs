using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMovementV2 : MonoBehaviour
{
    //Movement like Elden Ring. Move direction depends on Camera. Can Jump
    public float MoveSpeed = 100f;
    public float turnSmoothTime = 0.1f;
    public float JumpVeloctiy = 10f;

    public float DistanceToGround = 0.1f;

    public LayerMask GroundLayer;

    private CapsuleCollider col;

    private Rigidbody rb;
    private Transform cam;

    private float hInput;
    private float vInput;
    private float turnSmoothVelocity;
    private bool isJumping;
    // Update is called once per frame

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        cam = GameObject.Find("Main_Camera").transform;
        col = this.GetComponent<CapsuleCollider>();
    }
    private void Update()
    {
        isJumping |= Input.GetKeyDown(KeyCode.Space);
    }
    void FixedUpdate()
    {
        //moving the player
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(hInput, 0f, vInput).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            rb.MovePosition(this.transform.position + moveDir * MoveSpeed * Time.fixedDeltaTime);
        }

        //jumping     
        if (isJumping && IsGrounded())
        {
            rb.AddForce(Vector3.up * JumpVeloctiy, ForceMode.Impulse);

        }
        isJumping = false;
    }



    /*
     Returns if the player is touching the ground. Checks for collision at the bottom of the player.
     */
    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(col.bounds.center, capsuleBottom, DistanceToGround, GroundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }
}
