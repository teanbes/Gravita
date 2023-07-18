using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static InputManager;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{

    #region Events
    [HideInInspector] public delegate void StartTouch(Vector2 position, float time);
    [HideInInspector] public event StartTouch OnStartTouch;

    [HideInInspector] public delegate void EndTouch(Vector2 position, float time);
    [HideInInspector] public event EndTouch OnEndTouch;
    #endregion

    private PlayerInput playerInput;
    private Camera mainCamera;
    
    protected override void Awake()
    {
        base.Awake();
        playerInput = new PlayerInput();
        mainCamera = Camera.main;

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


    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null)
        {
            OnStartTouch(Utils.ScreenToWorld(mainCamera, playerInput.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
        }
    }

    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch(Utils.ScreenToWorld(mainCamera, playerInput.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.time);
        }
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera, playerInput.Touch.PrimaryPosition.ReadValue<Vector2>());
    }


}
