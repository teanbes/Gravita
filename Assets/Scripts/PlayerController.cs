using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SwipeDetection swipeDetection;
    
    [Header("Player Data")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private float playerSpeed; // this is temporary the player wont move like this

    [Header("Ground Check Data")]
    [SerializeField] private float collisionCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask ceilingMask;

    private bool isGrounded;
    private bool isCeiling;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
               
    }

    private void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, collisionCheckDistance, groundMask);
        isCeiling = Physics2D.Raycast(transform.position, Vector2.up, collisionCheckDistance, ceilingMask);

        float hInput = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(hInput, 0,0).normalized;
        transform.position += movement * playerSpeed * Time.deltaTime;
      
        
        // If player is in the ground
        if (isGrounded)
        {
            
            animator.SetBool("IsDash", false);// Stop Dash anim
            sr.flipY = false; // Flip player to walk on the floor

            // if swip down play dash anim, and change gravity
            if (swipeDetection.isSwipeUp == true)
            {
                animator.SetBool("IsDash", true);
                rb.gravityScale = -gravity;

            }
        }

        // If player is in the ceiling
        if (isCeiling)
        {
            animator.SetBool("IsDash", false);// Stop Dash anim
            sr.flipY = true;// Flip player to walk on the ceiling

            // if swip up play dash anim, and change gravity
            if (swipeDetection.isSwipeUp == false) 
            {
                animator.SetBool("IsDash", true);
                rb.gravityScale = gravity;
            }

        }
    }

    public void Jump()
    {
        rb.gravityScale = gravity * 0.04f;
        rb.AddForce(jumpForce * Vector2.up);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - collisionCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + collisionCheckDistance));

    }

}
