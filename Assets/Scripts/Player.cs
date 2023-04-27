using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Rigidbody2D theRb;

    [Header("Move Info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private int jumpForce;

    private bool PlayerUnlocked;

    [Header("Collision Info")]
    
    [SerializeField]
    private float groundCheckDistance;
    [SerializeField]
    private LayerMask whatisGround;


    private bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        theRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        AnimatorController();

        if (PlayerUnlocked)
        {
            theRb.velocity = new Vector2(moveSpeed, theRb.velocity.y);
            
        }

        CheckGrounded();
        CheckInput();
    }


    private void CheckGrounded()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatisGround);
    }

    private void AnimatorController()
    {
        anim.SetBool("IsGrounded", isGrounded);
        
        anim.SetFloat("xVelocity", theRb.velocity.x);
        anim.SetFloat("yVelocity", theRb.velocity.y);
    }    
    private void CheckInput()
    {   
           
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            theRb.velocity = new Vector2(theRb.velocity.x, jumpForce);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            PlayerUnlocked = true;
        }    
    }    

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
    }    
}
