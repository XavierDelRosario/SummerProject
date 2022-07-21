using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerState : MonoBehaviour
{
    //Listens to player inputs and determines player state.
    #region Fields
    [SerializeField] public LayerMask groundLayer;
    private float distanceToGround = 0.1f;

    private CapsuleCollider col;
    private PlayerInputActions playerInputActions;
    private InputAction movement;

    //States controlled by input
    private bool isCasting;
    private bool isFiring;
    private bool isJumping;
    private bool isRolling;
    private Vector2 inputDirection;

    private bool isInvincible;
    #endregion
    #region Properties
    public bool IsCasting 
    {   get 
        {
            return isCasting;
        }
    }
    public bool IsFiring 
    {    get 
        { 
            return isFiring; 
        }
    }
    public bool IsJumping
    {
        get
        { 
            return isJumping;
        }
    }
    public bool IsRolling
    {
        get
        {
            return isRolling;
        }
    }
    public Vector2 InputDirection
    {
        get
        {
            return inputDirection;
        }
    }
    public bool IsInvincible
    {
        get
        {
            return isInvincible;
        }
    }
    #endregion
    #region LifeCycle Methods
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        col = GetComponent<CapsuleCollider>();
    }
    private void OnEnable()
    {
        playerInputActions.Player.Enable();

        movement = playerInputActions.Player.Move;

        playerInputActions.Player.Jump.started += Jump;
        playerInputActions.Player.Jump.canceled += Jump;
        
        playerInputActions.Player.Fire.started += Fire;
        playerInputActions.Player.Fire.canceled += Fire;

        playerInputActions.Player.Cast.performed += Cast;
        playerInputActions.Player.Cast.canceled += Cast;

        playerInputActions.Player.Roll.performed += Roll;
        playerInputActions.Player.Roll.canceled += Roll;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();

        playerInputActions.Player.Jump.started -= Jump;
        playerInputActions.Player.Jump.canceled -= Jump;

        playerInputActions.Player.Fire.started -= Fire;
        playerInputActions.Player.Fire.canceled -= Fire;

        playerInputActions.Player.Jump.started -= Cast;
        playerInputActions.Player.Jump.canceled -= Cast;

        playerInputActions.Player.Roll.performed -= Roll;
        playerInputActions.Player.Roll.canceled -= Roll;
    }
    private void Update()
    {
        inputDirection = movement.ReadValue<Vector2>();
    }
    #endregion
    #region Methods
    #region Input
    private void Jump(InputAction.CallbackContext context)
    {
        isJumping = context.ReadValueAsButton();
    }
    private void Fire(InputAction.CallbackContext context)
    {
        isFiring = context.ReadValueAsButton();
    }
    private void Cast(InputAction.CallbackContext context)
    {
        isCasting = context.ReadValueAsButton();
    }
    private void Roll(InputAction.CallbackContext context)
    {
        isRolling = context.ReadValueAsButton();
    }
    #endregion
    /// <summary>
    /// Checks if the player is touching ground
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }
    public void EnableInvincibility()
    {
        isInvincible = true;
    }
    public void DisableInvincibility()
    {
        isInvincible = false;
    }
    #endregion
}


