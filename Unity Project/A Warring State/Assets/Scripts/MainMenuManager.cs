using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public PlayerInput playerInput;


    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.MainMenu.StartLevel.performed +=
            _ => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        playerInput.MainMenu.Quit.performed += _ => QuitApplication();
    }

    private void QuitApplication()
    {
        Debug.Log("Exit application");
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnEnable()
    {
        playerInput.MainMenu.Enable();
    }

    private void OnDisable()
    {
        playerInput.MainMenu.Disable();
    }
}
