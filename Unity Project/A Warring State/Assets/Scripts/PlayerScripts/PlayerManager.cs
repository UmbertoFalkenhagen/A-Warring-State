using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager _inputManager;
    private CameraManager _cameraManager;
    private PlayerLocomotion _playerLocomotion;
    
    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerLocomotion = GetComponent<PlayerLocomotion>();
        _cameraManager = FindObjectOfType<CameraManager>();
    }

    private void Update()
    {
        _inputManager.HandleAllInput();
    }

    private void FixedUpdate()
    {
        
        _playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        //_cameraManager.HandleAllCameraMovement();
    }
    
    /* The code to create the player consisting of the InputManager, PlayerManager,
     PlayerLocomotion and the AnimatorManager comes from different tutorials: 
     https://www.youtube.com/playlist?list=PLD_vBJjpCwJsqpD8QRPNPMfVUpPFLVGg4
     https://www.youtube.com/playlist?list=PLwyUzJb_FNeTQwyGujWRLqnfKpV-cj-eO
     https://www.youtube.com/watch?v=4HpC--2iowE
     */
}
