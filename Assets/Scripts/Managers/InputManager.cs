using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static InputManager;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{

    #region Swipe Detection Events
    [HideInInspector] public delegate void StartTouch(Vector2 position, float time);
    [HideInInspector] public event StartTouch OnStartTouch;

    [HideInInspector] public delegate void EndTouch(Vector2 position, float time);
    [HideInInspector] public event EndTouch OnEndTouch;
    #endregion

    [SerializeField] private PlayerController playerController;

    [HideInInspector] public PlayerInput playerInput;
    [SerializeField] private Camera mainCamera;

    private static InputManager _instance = null;

    public static InputManager instance
    {
        get => _instance;
    }

    private void Awake()
    {
        playerInput = new PlayerInput();
        mainCamera = Camera.main;
      //
      //  if (_instance)
      //  {
      //      Destroy(gameObject);
      //      return;
      //  }
      //
      //  _instance = this;
      //  DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Start()
    {
        playerInput.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx); 
        playerInput.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        
    }

    // Calculates the starting posicion for swipe detection
    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null)
        {
            OnStartTouch(Utils.ScreenToWorld(mainCamera, playerInput.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
        }
    }

    // Calculates the end posicion for swipe detection
    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch(Utils.ScreenToWorld(mainCamera, playerInput.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
        }
    }

    // Returns the touch position
    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera, playerInput.Touch.PrimaryPosition.ReadValue<Vector2>());
    }

   

}
