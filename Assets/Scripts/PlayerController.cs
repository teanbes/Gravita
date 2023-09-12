using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SwipeDetection swipeDetection;
    [SerializeField] public Animator animator;
    [SerializeField] public UIManager uiManager;
    [SerializeField] private GameObject tapToStartPanel;
    [SerializeField] public MovingLevelGenerator movingLevelGenerator;

    [Header("Player Data")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    
    [Header("Ground Check Data")]
    [SerializeField] private float collisionCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask ceilingMask;

    [Header("Slide Data")]
    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideTime = 0.5f;
    [SerializeField] private float slideCooldownTime = 2.0f;
    private float slideCooldownTracker;
    private float slideTimeCountdown;
    private bool isSliding;

    [Header("Death Data")]
    [SerializeField] private Parallax[] parallaxs;
    [SerializeField] private float levelSpeedOnDeath = 0;
    [HideInInspector] public bool isDead;
    [HideInInspector] private bool isGameOver;

    [Header("Projectile Data")]
    [SerializeField] private GameObject projectileSpawnPoint;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float fireRate = 2.0f;
    private float lastBulletFiredTime;

    [HideInInspector] public bool isStarted;
    private bool isGrounded;
    private bool isCeiling;
    private bool isShooting;

    [HideInInspector] public Rigidbody2D rb;
    private SpriteRenderer sr;
    private int levelSpeed = 7;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        movingLevelGenerator.objectSpeed = 0;
        isDead = false;
        isStarted = false;


        swipeDetection.SwipeUpEvent += OnSwipeUp;
        swipeDetection.SwipeDownEvent += OnSwipeDown;
        swipeDetection.SwipeForwardEvent += OnForwardSwipe;
        swipeDetection.TapEvent += OnTap;

    }

    private void OnDisable()
    {
        swipeDetection.SwipeUpEvent -= OnSwipeUp;
        swipeDetection.SwipeDownEvent -= OnSwipeDown;
        swipeDetection.SwipeForwardEvent -= OnForwardSwipe;
        swipeDetection.TapEvent -= OnTap;
    }

    private void Update()
    {
        lastBulletFiredTime += Time.deltaTime;

        if (!isStarted) 
        { 
            uiManager.PauseBackgorundMusic(); 
            return;
        }

        slideTimeCountdown -= Time.deltaTime;
        slideCooldownTracker -= Time.deltaTime;

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, collisionCheckDistance, groundMask);
        isCeiling = Physics2D.Raycast(transform.position, Vector2.up, collisionCheckDistance, ceilingMask);
        
        // If player is in the ground
        if (isGrounded && !isDead)
        {
            animator.SetBool("IsDash", false);// Stop Dash anim
            sr.flipY = false; // Flip player to walk on the floor
        }

        // If player is in the ceiling
        if (isCeiling && !isDead)
        {
            animator.SetBool("IsDash", false);// Stop Dash anim
            sr.flipY = true;// Flip player to walk on the ceiling
        }

        SlideCheck();

        if (isDead) 
        {
            StartCoroutine(Die());
           
        }

    }

    private void SlideCheck()
    {
        if (slideTimeCountdown < 0) 
        {
            isSliding = false;
            animator.SetBool("IsSliding", false);
        }
    }

    private void OnSwipeUp()
    {
        if (isGrounded && !isSliding && !isDead) 
        {
            AudioManager.Instance.Play("DashUp");
            animator.SetBool("IsDash", true);
            rb.gravityScale = -gravity;
        }
    }

    private void OnSwipeDown()
    {
        if (isCeiling && !isSliding && !isDead)
        {
            AudioManager.Instance.Play("DashDown");
            animator.SetBool("IsDash", true);
            rb.gravityScale = gravity;
        }
    }

    // need to change this to game manager
    public IEnumerator Die()
    {
        // play death sound
        animator.SetBool("IsDead", true);
        uiManager.PauseBackgorundMusic();

        movingLevelGenerator.objectSpeed = -3;
        
        foreach (Parallax P in parallaxs) 
        { 
            P.speed = levelSpeedOnDeath;
        }

        //rb.velocity = Vector3.left * 1.5f;

        yield return new WaitForSeconds(0.5f);
        //Time.timeScale = 0;
        movingLevelGenerator.objectSpeed = 0;
        rb.velocity = new Vector2(0, 0);
        GameOver();

    }


    private void GameOver()
    {
        SceneManager.LoadScene(2);
    }


    // Sliding on forward swipe
    private void OnForwardSwipe()
    {
        Debug.LogWarning("Dashing.....");

        if (!isSliding && slideCooldownTracker < 0 && !isDead && isGrounded || isCeiling   )
        {
            // play slide sound
            isSliding = true;
            slideTimeCountdown = slideTime;
            slideCooldownTracker = slideCooldownTime;
            animator.SetBool("IsSliding", true);

        }
    }

    private void OnTap()
    {
        if (!isStarted)
        {
            isStarted = true;
            tapToStartPanel.SetActive(false);
            animator.SetBool("IsRunning", true);
            movingLevelGenerator.objectSpeed = levelSpeed;
            uiManager.UnpauseBackgorundMusic();
            GameManager.instance.isGameStarted = true;
        }
        
        if (isStarted && lastBulletFiredTime >= fireRate) 
        {
            AudioManager.Instance.Play("Shoot");
            Projectile curProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
            lastBulletFiredTime = 0.0f;
        }



    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - collisionCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + collisionCheckDistance));

    }

}
