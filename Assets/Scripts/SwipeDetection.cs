using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    
    private InputManager inputManager;

    [SerializeField] private float minDistance = 0.2f;
    [SerializeField] private float maxTime = 1f;
    [SerializeField] private GameObject trail;
    [SerializeField] private PlayerController playerController;

    [SerializeField, Range(0f, 1f)] 
    private float directionThreshold = 0.9f;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    private Coroutine coroutine;

    [SerializeField] public bool isSwipeUp;//********************************* ojo

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    // Swipe start position and time
    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
        trail.SetActive(true);
        trail.transform.position = position;
        coroutine =  StartCoroutine("Trail");
    }

    // Handles the trail renderer for visual notification
    private IEnumerator Trail()
    {
        while (true) 
        {
            trail.transform.position = inputManager.PrimaryPosition();
            yield return null;
        }
    }

    // Swipe end position and time
    private void SwipeEnd(Vector2 position, float time)
    {
        trail.SetActive(false);
        StopCoroutine(coroutine);
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    // Detects if is a swipe or not
    public void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minDistance && (endTime - startTime) <= maxTime)
        {
            Debug.Log("Swipe Detected");
            Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }

        // Detection for tap on touchscreen
        bool isTap = Vector3.Distance(startPosition, endPosition) < minDistance && (endTime - startTime) <= maxTime;
        if(isTap) 
        {
            Debug.Log("Tap Detected");
            playerController.Jump();
            return;
        }
    }

    // Calculating the swipe direction
    private void SwipeDirection(Vector2 direction)
    {
        if(Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            isSwipeUp = true;
            Debug.Log("Swipe Up");
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            isSwipeUp = false;
            Debug.Log("Swipe down");
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            Debug.Log("Swipe Left");
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            Debug.Log("Swipe Right");
        }
    }
}
