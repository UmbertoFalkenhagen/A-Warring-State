using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerInput _playerInput;
    public AnimatorManager animatorManager;
    public PlayerLocomotion locomotionmanager;

    public Vector2 movementinput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;
    
    public bool isRunning = false;
    public bool isJumping = false;

    public Vector2 cameraInput;
    public float cameraInputHorizontal;
    public float cameraInputVertical;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.CharacterControls.Movement.performed += ctx => movementinput = ctx.ReadValue<Vector2>();
        animatorManager = GetComponent<AnimatorManager>();
        _playerInput.CharacterControls.CameraMovement.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();
        _playerInput.CharacterControls.Run.performed +=
            ctx => ChangeRunningState(ctx.ReadValueAsButton()); //ChangeRunningState(ctx.ReadValueAsButton());
        _playerInput.CharacterControls.Jump.performed += _ => ChangeJumpingState();
    }
    

    public void HandleAllInput()
    {
       HandleMovementInput(); 
    }

    public void HandleMovementInput()
    {
        verticalInput = movementinput.y;
        horizontalInput = movementinput.x;

        cameraInputHorizontal = cameraInput.x;
        cameraInputVertical = cameraInput.y;
        
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        //animatorManager.UpdateAnimatorValues(0, moveAmount);
        animatorManager.AdjustMovementToCurrentInput();
    }

    private void ChangeJumpingState()
    {
        //if (locomotionmanager.isGrounded)
        {
            isJumping = true;
            Debug.Log("jumped");
        }
        //else
        {
            //Debug.Log("You're mid-air");
        }
        
    }
    private void ChangeRunningState(bool buttonState)
    {
        if (isRunning)
        {
            isRunning = !buttonState;
        }
        else
        {
            isRunning = buttonState;
        }
        
    }
    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }
}
