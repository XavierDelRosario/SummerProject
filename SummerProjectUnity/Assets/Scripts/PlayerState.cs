using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerState : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private InputAction movement;
   
    public bool isCasting { get; private set; }
    public bool isFiring { get; private set; }
    public bool isMoving { get; private set; }
    public bool isJumping { get; private set; }
    public Vector2 inputDirection { get; private set; }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
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
    private void Update()
    {
        inputDirection = movement.ReadValue<Vector2>();
    }
}


