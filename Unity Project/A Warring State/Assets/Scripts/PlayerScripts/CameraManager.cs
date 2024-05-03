using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public InputManager inputmanager;
    
    public Transform targetTransform;
    public Transform cameraPivot;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 2f;
    public float cameraPivotSpeed = 2f;

    public float lookAngle;
    public float pivotAngle;
    public float minimumPivotAngle = -35f;
    public float maximumPivotAngle = 35f;
    
    

    private void Awake()
    {
        
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position,
            ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        lookAngle = lookAngle + (inputmanager.cameraInputHorizontal * cameraLookSpeed * Time.deltaTime);
        pivotAngle = pivotAngle + (inputmanager.cameraInputVertical  * cameraPivotSpeed * Time.deltaTime);
        pivotAngle = Mathf.Clamp01(pivotAngle);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }
}
