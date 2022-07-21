using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement script with turning and jumping.
    #region Fields 
    [SerializeField] private float jumpVeloctiy = 10f;
    [SerializeField] private float rollSpeed = 10f;
    [SerializeField] private float rollDelaySeconds = 1f;
    [SerializeField] private float invincibilitySeconds = .75f;

    private PlayerState playerState;
    private PlayerStats playerStats;

    private Rigidbody rb;
    private Transform cam;
    private float moveSpeed;
    
    private float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;
    private Vector3 moveDirection;
    private bool isJumping;
    private bool isCasting;
    private bool isRolling;
    private bool canRoll = true;
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
        isRolling = playerState.IsRolling;
        Move();
        Jump();
        Roll();
    }
    #region Methods

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
    private void Move()
    {
        float vInput = playerState.InputDirection.y;
        float hInput = playerState.InputDirection.x;
        Vector3 direction = new Vector3(hInput, 0f, vInput).normalized;

        if (isCasting)
        {
            //Player moves while always looking forward where the camera is looking.
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            rb.MovePosition(transform.position + transform.forward * vInput * moveSpeed * Time.fixedDeltaTime + transform.right * hInput * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            //Player rotates to the direction they are going.
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
            }
        }
    }
    /// <summary>
    /// Roll the player granting invincibility for a short time.
    /// </summary>
    private void Roll()
    {
        if (isRolling && playerState.IsGrounded() && canRoll)
        {
            rb.AddForce(moveDirection * rollSpeed, ForceMode.Impulse);

            playerState.EnableInvincibility();
            canRoll = false;

            Invoke(nameof(DisableInvincibility), invincibilitySeconds);
            Invoke(nameof(EnableRoll), rollDelaySeconds);
        }
    }
    private void EnableRoll()
    {
        canRoll = true;
    }
    private void DisableInvincibility()
    {
        playerState.DisableInvincibility();
    }
    #endregion
}
