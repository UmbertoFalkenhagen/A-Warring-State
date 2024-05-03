using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public InputManager inputManager;
    public Animator animator;
    public int horizontal;
    public int vertical;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
        
    }

    public void AdjustMovementToCurrentInput()
    {
        if (inputManager.moveAmount > 0)
        {
            animator.SetBool("isSneaking", true);
            if (inputManager.isRunning)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            animator.SetBool("isSneaking", false);
            animator.SetBool("isRunning", false);
        }

        if (inputManager.isJumping)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }
    
    /*public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement) //only needed for blendtree animations
    {
        //Animation Snapping
        float snappedHorizontal;
        float snappedVertical;

        #region SnappedHorizontal
        if (horizontalMovement > 0 && horizontalMovement < 0.8f)
        {
            snappedHorizontal = 0.5f;
        } else if (horizontalMovement >= 0.8f)
        {
            snappedHorizontal = 1f;
        } else if (horizontalMovement < 0 && horizontalMovement > -0.8f)
        {
            snappedHorizontal = -0.5f;
        } else if (horizontalMovement <= -0.8f)
        {
            snappedHorizontal = -1f;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion
        #region SnappedVertical
        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        } else if (verticalMovement >= 0.55f)
        {
            snappedVertical = 1f;
        } else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snappedVertical = -0.5f;
        } else if (verticalMovement <= -0.55f)
        {
            snappedVertical = -1f;
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion
        
        
        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }*/
}
