using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerState : MonoBehaviour
{
    //Listens to player inputs and determines player state.
    #region Fields
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] private float distanceToGround = 0.1f;

    private CapsuleCollider col;
    private PlayerInputActions playerInputActions;
    private InputAction movement;
    private bool isCasting;
    private bool isFiring;
    private bool isJumping;
    private Vector2 inputDirection;
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
    public Vector2 InputDirection
    {
        get
        {
            return inputDirection;
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
    }
    private void Update()
    {
        inputDirection = movement.ReadValue<Vector2>();
    }
    #endregion
    #region Methods
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
    /// <summary>
    /// Adds jump force if the given jump input and player is grounded
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }
    #endregion
}


