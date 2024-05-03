using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    public Animator characteranimator;

    private int isSneakingHash;

    private int isRunningHash;

    private PlayerInput input;

    private Vector2 currentMovement;
    private bool movementPressed;
    private bool runPressed;

    private void Awake()
    {
        input = new PlayerInput();

        input.CharacterControls.Movement.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = ctx.ReadValue<Vector2>().magnitude != 0;
            Debug.Log(movementPressed);
        };
        input.CharacterControls.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();
    }

    // Start is called before the first frame update
    void Start()
    {
        isSneakingHash = Animator.StringToHash("isSneaking");
        isRunningHash = Animator.StringToHash("isRunning");

    }

    // Update is called once per frame
    void Update()
    {
        
        HandleMovement();
    }

    void HandleRotation()
    {
        Vector3 currentPosition = transform.position;
    }
    void HandleMovement()
    {
        
        bool isRunning = characteranimator.GetBool(isRunningHash);
        bool isSneaking = characteranimator.GetBool(isSneakingHash);

        if (movementPressed && !isSneaking)
        {
            characteranimator.SetBool(isSneakingHash, true);
        }

        if (!movementPressed && isSneaking)
        {
            characteranimator.SetBool(isSneakingHash, false);
        }

        if ((movementPressed && runPressed) && !isRunning)
        {
            characteranimator.SetBool(isRunningHash, true);
        }

        if ((!movementPressed || !runPressed) && isRunning)
        {
            characteranimator.SetBool(isRunningHash, false);
        }
    }

    private void OnEnable()
    {
        input.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        input.CharacterControls.Disable();
    }
}
