using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

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

   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
               
    }

    private void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, collisionCheckDistance, groundMask);
        isCeiling = Physics2D.Raycast(transform.position, Vector2.up, collisionCheckDistance, ceilingMask);

        float hInput = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(hInput, 0,0).normalized;
        transform.position += movement * playerSpeed * Time.deltaTime;
      
        

        if (isGrounded)
        {
            if (swipeDetection.isSwipeUp == true)
                rb.gravityScale = -gravity;
        }
        if (isCeiling)
        {
            if (swipeDetection.isSwipeUp == false)
                rb.gravityScale = gravity;
        }
    }

    public void Jump()
    {
        rb.AddForce(jumpForce * Vector2.up);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - collisionCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + collisionCheckDistance));

    }

}
