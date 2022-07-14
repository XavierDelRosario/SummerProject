using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //Movement script with turning and jumping.
   #region Fields
    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float jumpVeloctiy = 10f;
    [SerializeField] private float distanceToGround = 0.1f;
    [SerializeField] public LayerMask groundLayer;

    private CapsuleCollider col;
    private Rigidbody rb;
    private PlayerCast playerCast;
    private Transform cam;

    private float hInput;
    private float vInput;
    private float turnSmoothVelocity;
    private bool isJumping;
    private bool isCasting;
    #endregion
    private void Start()
    {
        playerCast = this.GetComponent<PlayerCast>();
        rb = this.GetComponent<Rigidbody>();
        col = this.GetComponent<CapsuleCollider>();        
        cam = GameObject.Find("Main_Camera").transform;
    }
    private void Update()
    {
        isJumping |= Input.GetKeyDown(KeyCode.Space);
        isCasting = playerCast.IsCasting;
    }
    void FixedUpdate()
    {
        Move();
        Jump();
    }
    #region Methods
    /*
     Moves the player based on the camera direction.
     */
    private void Move()
    {
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(hInput, 0f, vInput).normalized;
        if (isCasting)
        {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                
                rb.MovePosition(this.transform.position + this.transform.forward * vInput  * moveSpeed * Time.fixedDeltaTime + this.transform.right * hInput * moveSpeed * Time.fixedDeltaTime);
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
    /*
     Adds jump force if the given jump input and player is grounded
     */
    private void Jump()
    {        
        if (isJumping && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpVeloctiy, ForceMode.Impulse);
        }
        isJumping = false;
    }
    /*
     Returns true if the player is touching the ground. Checks for collision at the bottom of the player.
     */
    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }
    #endregion
}
