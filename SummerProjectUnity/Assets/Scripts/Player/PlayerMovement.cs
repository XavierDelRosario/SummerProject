using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement script with turning and jumping.
    #region Fields 

    private PlayerState playerState;
    private PlayerStats playerStats;
    private Rigidbody rb;
    private Transform cam;

    private float moveSpeed;
    private float jumpVeloctiy = 10f;
    private float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;
    private bool isJumping;
    private bool isCasting;
    #endregion
    private void Awake()
    {
        playerState = GetComponent<PlayerState>();
        playerStats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody>();
        cam = GameObject.Find("Main_Camera").transform;
    }
    void FixedUpdate()
    {
        moveSpeed = playerStats.MoveSpeed;
        isJumping = playerState.IsJumping;
        isCasting = playerState.IsCasting;
        Move();
        Jump();
    }
    #region Methods
    /// <summary>
    /// Moves the player based on the camera direction.
    /// </summary>
    private void Move()
    {

        float vInput = playerState.InputDirection.y;
        float hInput = playerState.InputDirection.x;
        Vector3 direction = new Vector3(hInput, 0f, vInput).normalized;

        if (isCasting)
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            rb.MovePosition(transform.position + transform.forward * vInput * moveSpeed * Time.fixedDeltaTime + transform.right * hInput * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                rb.MovePosition(this.transform.position + moveDir * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    /// <summary>
    /// Adds jump force if the given jump input and player is grounded
    /// </summary>
    private void Jump()
    {
        if (isJumping && playerState.IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpVeloctiy, ForceMode.Impulse);
        }
    }
    #endregion
}
