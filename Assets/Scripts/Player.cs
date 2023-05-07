using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Rigidbody2D theRb;
    /*[SerializeField]
    private float timer;*/

    [Header("Move Info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;


    private bool canDoubleJump;
    private bool PlayerUnlocked;

    [Header("Move Info")]
    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideTime;
    [SerializeField] private float slideCoolDownTime;
    private bool isSliding;
    private float slideTimerCounter;
    private float slideCoolDownCounter;

    [Header("Collision Info")]
    
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float cellingCheckDistance;
    [SerializeField] private LayerMask whatisGround;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Vector2 wallCheckSize;
    private bool wallDeteced;



    private bool isGrounded;
    private bool isCelling;


    // Start is called before the first frame update
    void Start()
    {
        theRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Debug.Log(wallDeteced);
    }

    // Update is called once per frame
    void Update()
    {
        slideTimerCounter -= Time.deltaTime;
        slideCoolDownCounter -= Time.deltaTime;

        AnimatorController();

        if (PlayerUnlocked)
        {
            Movement();
        }

        CheckInput();
        CheckForSlide();
        CheckGrounded();
        CheckWallAhead();
        CheckCeilling();

        if (isGrounded)
        {
            canDoubleJump = true;
        }
    }

    private void CheckForSlide()
    {
        if (slideTimerCounter <0 && !isCelling)
        {
            isSliding = false;
        }
    }
    private void Movement()
    {
        if(!wallDeteced)
        {
            return;
        }    

        if (isSliding)
        {
            theRb.velocity = new Vector2(slideSpeed, theRb.velocity.y);
        } else
        {
            theRb.velocity = new Vector2(moveSpeed, theRb.velocity.y);
        }
    }


    private void SlideButton()
    {
        if ( theRb.velocity.x != 0 && slideCoolDownCounter < 0)
        {
            isSliding = true;
            slideTimerCounter = slideTime;
            slideCoolDownCounter = slideCoolDownTime;
        }
        

    }
    private void JumpButton()
    {
        if(isSliding)
        {
            return;
        }
        if (isGrounded)
        {
            theRb.velocity = new Vector2(theRb.velocity.x, jumpForce);
        }    
        else if (canDoubleJump)
        {
            canDoubleJump = false;
            theRb.velocity = new Vector2(theRb.velocity.x, doubleJumpForce);
        }
    }
    private void CheckInput()
    {   
        if (Input.GetButtonDown("Fire2"))
        {
            PlayerUnlocked = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SlideButton();
        }
    }
    private void AnimatorController()
    {
        // Using BlendTree for this one
        anim.SetBool("canDoubleJump", canDoubleJump);
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetBool("IsSliding", isSliding);
        anim.SetFloat("xVelocity", theRb.velocity.x);
        anim.SetFloat("yVelocity", theRb.velocity.y);
    }    
    private void CheckGrounded()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatisGround);
    }
    private void CheckWallAhead()
    {
        wallDeteced = Physics2D.BoxCast(wallCheck.position, wallCheckSize, 0, Vector2.zero, 0, whatisGround);
    }    
    private void CheckCeilling()
    {
        isCelling = Physics2D.Raycast(transform.position, Vector2.up, cellingCheckDistance, whatisGround);
    }    


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + cellingCheckDistance));
        Gizmos.DrawWireCube(wallCheck.position, wallCheckSize);
    }    
}
