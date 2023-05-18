using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Rigidbody2D theRb;
    [SerializeField]
    private SpriteRenderer theSR;
    /*[SerializeField]
    private float timer;*/
    [HideInInspector] public bool PlayerUnlocked;
    [HideInInspector] public bool extraLife;
    private bool isDead;

    [Header("VFX")]
    [SerializeField] private ParticleSystem dustFx;


    [Header("KnockBack Info")]
    [SerializeField] private Vector2 knockbackDir;
    private bool isKnocked;
    private bool canBeKnocked = true;

    [Header("Speed Info")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedMultiplier;
    private float defaultSpeed;
    [Space]
    [SerializeField] private float milestoneIncreaser;
    private float defaultMilestoneIncrease;
    private float speedMilestones;

    [Header("Move Info")]
    [SerializeField] private float speedToSurvice = 18;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float doubleJumpForce;

    private bool readyToLand;
    private bool canDoubleJump;

    [Header("Slide Info")]
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

    [HideInInspector] public bool ledgeDetected;

    [Header("Ledge Info")]

    [SerializeField] private Vector2 offset1; // Pos Start climbing
    [SerializeField] private Vector2 offset2; // Pos After climbing


    private Vector2 climbPosBegin;
    private Vector2 climbPosOver;

    private bool canClimb;
    private bool canGrabLedge = true; // True so player can grab

    private bool isGrounded;
    private bool isCelling;



    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
        theRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // Use this if not the player don't get Color from PlayerRef
        GameManager.Instance.GetSavedColor(theSR);
        speedMilestones = milestoneIncreaser;
        defaultSpeed = moveSpeed;
        defaultMilestoneIncrease = milestoneIncreaser;
    }

    // Update is called once per frame
    void Update()
    {
        //Check Collision
        CheckGrounded();
        CheckWallAhead();
        CheckCelling();


        AnimatorController();
        SpeedController();

        slideTimerCounter -= Time.deltaTime;
        slideCoolDownCounter -= Time.deltaTime;
        extraLife = moveSpeed >= speedToSurvice;
        
        //Test Knockback
        /*if (Input.GetKeyDown(KeyCode.K))
        {
            Knockback();
        }*/

        //Test Die
        /*if (Input.GetKeyDown(KeyCode.J) && !isDead)
        {
            StartCoroutine(Die());
        }*/

        if (isDead)
        {
            return;
        }


        if (isKnocked)
        {
            return;
        }


        if (PlayerUnlocked)
        {
            Movement();
        }

        if (isGrounded)
        {
            canDoubleJump = true;
        }

        CheckInput();
        CheckForSlide();
        CheckForLedge();
        CheckForLanded();

        
    }

    private void CheckForLanded()
    {
        if (theRb.velocity.y < -5 && !isGrounded)
        {
            readyToLand = true;
        }
        if (readyToLand && isGrounded)
        {
            dustFx.Play();
            readyToLand = false;
        }
    }

    public void Damage()
    {
        if (extraLife)
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.BGM_SFX_FALLINGSPIKE_LAND);
            }
            Knockback();
        }
        else
        {
            // Remember to Start Coroutine <= Player cant die by Trap
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.BGM_SFX_FALLINGSPIKE_LAND);
            }
            StartCoroutine(Die());
        }

    }

    private IEnumerator Die()
    {
         
        canBeKnocked = false;
        isDead = true;
        theRb.velocity = knockbackDir;
        anim.SetBool("IsDead", true);
        Time.timeScale = 0.8f;
        yield return new WaitForSeconds(.5f);
        theRb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(.5f);
        GameManager.Instance.EndGame();
        
        
    }

    private IEnumerator Invincibility()
    {
        Color originalColor = theSR.color;
        Color darkenColor = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);

        canBeKnocked = false;
        theSR.color = darkenColor;
        yield return new WaitForSeconds(.1f);
        theSR.color = originalColor;
        yield return new WaitForSeconds(.12f);
        theSR.color = darkenColor;
        yield return new WaitForSeconds(.13f);
        theSR.color = originalColor;
        yield return new WaitForSeconds(.15f);
        theSR.color = darkenColor;
        yield return new WaitForSeconds(.15f);
        theSR.color = originalColor;
        yield return new WaitForSeconds(.2f);
        theSR.color = darkenColor;
        yield return new WaitForSeconds(.2f);
        theSR.color = originalColor;
        yield return new WaitForSeconds(.25f);
        theSR.color = darkenColor;
        yield return new WaitForSeconds(.25f);
        theSR.color = originalColor;


        canBeKnocked = true;
    }

    private void Knockback()
    {
        if (!canBeKnocked)
        {
            return;
        }
        StartCoroutine(Invincibility());
        isKnocked = true;
        theRb.velocity = knockbackDir;

    }

    // Using add event in Animation
    private void CancelKnockbacck() => isKnocked = false;
    private void CheckForLedge()
    {
        if (ledgeDetected && canGrabLedge)
        {
            canGrabLedge = false;

            /*//Fix animation roll while climbing
            theRb.gravityScale = 0;*/


            // Get pos of the ledge 
            Vector2 ledgePosition = GetComponentInChildren<LedgeDetection>().transform.position;

            // Set Pos before climb and after climb using ledgePos and offset 
            climbPosBegin = ledgePosition + offset1;
            climbPosOver = ledgePosition + offset2;


            canClimb = true;
        }

        if (canClimb)
        {
            transform.position = climbPosBegin;
        }
    }

    // Using add event in Animation
    private void LedgeClimbOver()
    {
        canClimb = false;

        /*//Change Gravity back to normal after climbing
        theRb.gravityScale = 3;*/

        transform.position = climbPosOver;
        isGrounded = true;
        Invoke("AllowLedgeGrab", 1f);

    }

    private void AllowLedgeGrab() => canGrabLedge = true;

    private void CheckForSlide()
    {
        if (slideTimerCounter < 0 && !isCelling)
        {
            isSliding = false;
        }
    }

    private void SpeedController()
    {
        if (moveSpeed == maxSpeed)
        {
            return;
        }

        if (transform.position.x > speedMilestones)
        {
            speedMilestones = speedMilestones + milestoneIncreaser;

            moveSpeed *= speedMultiplier;

            milestoneIncreaser = milestoneIncreaser * speedMultiplier;

            if (moveSpeed > maxSpeed)
            {
                moveSpeed = maxSpeed;
            }
        }
    }

    private void SpeedReset()
    {
        if (isSliding)
        {
            return;
        }
        moveSpeed = defaultSpeed;
        milestoneIncreaser = defaultMilestoneIncrease;

    }
    private void Movement()
    {
        if (wallDeteced)
        {
            SpeedReset();
            return;
        }

        if (isSliding)
        {
            theRb.velocity = new Vector2(slideSpeed, theRb.velocity.y);
        }
        else
        {
            theRb.velocity = new Vector2(moveSpeed, theRb.velocity.y);
        }
    }


    private void SlideButton()
    {
        if (isDead)
        {
            return; 
        }
        if (theRb.velocity.x != 0 && slideCoolDownCounter < 0)
        {
            dustFx.Play();
            isSliding = true;
            slideTimerCounter = slideTime;
            slideCoolDownCounter = slideCoolDownTime;
        }


    }
    private void JumpButton()
    {
        if (isSliding || isDead)
        {
            return;
        }

        RollFinished();

        if (isGrounded)
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.BGM_SFX_JUMP_01);
            }
            theRb.velocity = new Vector2(theRb.velocity.x, jumpForce);
            dustFx.Play();
        }
        else if (canDoubleJump)
        {
            canDoubleJump = false;
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.BGM_SFX_JUMP_02);
            }
            theRb.velocity = new Vector2(theRb.velocity.x, doubleJumpForce);
            dustFx.Play();
        }
    }
    private void CheckInput()
    {
        /*if (Input.GetButtonDown("Fire2"))
        {
            PlayerUnlocked = true;
        }*/

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
        anim.SetBool("canClimb", canClimb);
        anim.SetBool("IsKnocked", isKnocked);

        if (theRb.velocity.y < 10)
        {
            anim.SetBool("canRoll", true);
        }
    }

    // Using add event in Animation
    private void RollFinished() => anim.SetBool("canRoll", false);
    private void CheckGrounded()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatisGround);
    }
    private void CheckWallAhead()
    {
        wallDeteced = Physics2D.BoxCast(wallCheck.position, wallCheckSize, 0, Vector2.zero, 0, whatisGround);
    }
    private void CheckCelling()
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
