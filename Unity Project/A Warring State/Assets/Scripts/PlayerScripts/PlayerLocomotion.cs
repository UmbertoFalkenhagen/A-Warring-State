using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    public CharacterController characterController;
    private InputManager _inputManager;
    private Vector3 moveDirection;
    public Transform cameraObject;
    //public Rigidbody playerRigidbody;
    
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.2f;
    public float gravity = -20f;
    public Vector3 movementvelocity;
    public bool isGrounded;
    public float jumpHeight = 1.5f;
    
    

    public float sneakingSpeed = 3f;
    public float sneakingheight = 1.2f;
    public float movementSpeed = 5f;
    public float runningSpeed = 8f;
    public float runningheight = 1.8f;
    public float rotationSpeed = 15f;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        //playerRigidbody = GetComponent<Rigidbody>();
        //cameraObject = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
        
    }

    private void Start()
    {
        //characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * _inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * _inputManager.horizontalInput;
        moveDirection.Normalize();
        //moveDirection.y = 0;

        /*if (_inputManager.moveAmount >= 0.5f)
        {
            moveDirection = moveDirection * runningSpeed;
        }
        else
        {
            moveDirection = moveDirection * sneakingSpeed;
        }*/
        
        

        if (isGrounded && movementvelocity.y < 0)
        {
            movementvelocity.y = -1f;
            
        }

        if (isGrounded)
        {
            if (_inputManager.isJumping)
            {
                movementvelocity.y = Mathf.Sqrt(-0.5f * jumpHeight * gravity);
            }

            _inputManager.isJumping = false;
        }

        if (_inputManager.isRunning)
        {
            moveDirection = moveDirection * runningSpeed;
            characterController.height = runningheight;
            characterController.center = new Vector3(0, runningheight / 2f, 0);
        }
        else
        {
            moveDirection = moveDirection * sneakingSpeed;
            characterController.height = sneakingheight;
            characterController.center = new Vector3(0, sneakingheight / 2f, 0);
        }

        
        movementvelocity.x = moveDirection.x;
        movementvelocity.y += gravity * Time.deltaTime;
        movementvelocity.z = moveDirection.z;
        
        //playerRigidbody.MovePosition(movementvelocity * Time.deltaTime);
        characterController.Move(movementvelocity * Time.deltaTime);

    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * _inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * _inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }
        
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
}
