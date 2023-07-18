using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SwipeDetection swipeDetection;

    private Rigidbody2D rb;

    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (swipeDetection.temp == true)
            rb.gravityScale = -100;

        if (swipeDetection.temp ==  false)
            rb.gravityScale = 100;


    }

}
