using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimationManager : MonoBehaviour
{
    public Guard guardManager;
    public Animator animator;

    public void AdjustAnimationtoMovement(bool turningState)
    {
        
            animator.SetBool("isTurning", turningState);
        
        
    }
}
