using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed; //const
    private float lastInputBeforeZero = 1; //!!!
    private bool facingRight = true;

    [Header("Wall Slide")]
    public float wallSlideSpeed; //const
    private bool wallSliding = false; //local makes problems

    [Header("Jump")]
    public float jumpForce; //const

    [Header("Double Jump")]
    public float doubleJumpForce;
    private bool canDoubleJump = false;

    [Header("Wall Bounce/Jump")]
    public float wallBounceSpeed; //const
    public float wallBounceForce; //const
    public float wallBounceTime; //consr
    private bool wallBouncing = false; //local makes problems

    [Header("Dash")]
    public float dashSpeed; //const
    public float dashTime; //const
    private bool dashing = false;
    private bool canDash;

    [Header("Spike Jump")]
    public BoolValue spikeJumping;
    public float spikeJumpForce;
    public float spikeJumpTime;
    private bool isInAirAfterSpikeJumpDash;
    private bool isInAirAfterSpikeJumpDoubleJump;

    [Header("Knockback")]
    public float knockbackIntensityX;
    public float knockbackIntensityY;
    public float knockBackTime;
    [HideInInspector]public float knockbackCount;
    [HideInInspector]public bool knockbackRight;

    //Const
    private const float gravity = 3; //const
    private const float extraHeight = 0.1f; //const

    [Header("References")]
    public LayerMask platformLayerMask;
    private Rigidbody2D myRigidbody; 
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Knockback();
        MovementController();
        Jump();
        StartWallBounce();
        StartDash();
        DoubleJump();
        LastInputBeforeZero();
        SpikeJump();
    }

    private void FixedUpdate()
    {
        WallSlide();
    }

    private float InputHorizontal()
    {
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            return 1;
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            return -1;
        }
        else if (dashing)
        {
            return 0;
        }
        else if(knockbackCount > 0)
        {
            return 0;
        }
        else
        {
            return 0;
        }
    }

    void MovementController()
    {
        if(knockbackCount <= 0)
        {
            myRigidbody.velocity = new Vector2(InputHorizontal() * speed, myRigidbody.velocity.y);
            Flip();
        }
    }

    void Flip()
    {
        if(InputHorizontal() > 0 && !facingRight)
        {
            ProcessFlip();
        }
        else if(InputHorizontal() < 0 && facingRight)
        {
            ProcessFlip();
        }
    }

    void ProcessFlip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
        }

        if (Input.GetButtonUp("Jump") && myRigidbody.velocity.y > 0)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y * 0.1f);
        }

    }

    private void DoubleJump()
    {
        if (Input.GetButtonDown("Jump") && !IsGrounded() && !IsWalled() && (canDoubleJump || CanDoubleJumpAfterSpikeJump()))
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, doubleJumpForce);
            canDoubleJump = false;
        }

        if (IsGrounded() || wallSliding) //Have no idea why IsWallLefted() && InputHorizontal() != 0 doesn't work instead of wallsliding (Maybe some execution order???)
        {                                                       
            canDoubleJump = true;
        }
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && InputHorizontal() != 0) //!IsGrounded is used because if you stand near wall jump you shouldn't wallslide if there's no input
        {
            wallSliding = true;
        }

        WallSlidingCheck();
    }

    private void WallSlidingCheck()
    {
        if (wallSliding && IsWalled())
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, Mathf.Clamp(myRigidbody.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else if (!IsWalled())
        {
            wallSliding = false;
        }
    }

    private void StartWallBounce()
    {
        if(Input.GetButtonDown("Jump") && IsWalled() && InputHorizontal() != 0)
        {
            wallBouncing = true;
            StartCoroutine(EndWallBounceCo());
        }

        ProcessWalBounce();
    }

    private void ProcessWalBounce()
    {
        if (wallBouncing)
        {
            myRigidbody.velocity = new Vector2(wallBounceSpeed * -InputHorizontal(), wallBounceForce);
        }
    }

    private IEnumerator EndWallBounceCo()
    {
        yield return new WaitForSeconds(wallBounceTime);
        wallBouncing = false;
    }

    private void StartDash()
    {
        if(Input.GetButtonDown("Dash") && canDash && !IsWalled()) //You can't dash from walls
        {
            dashing = true;
            StartCoroutine(EndDashCo());
        }

        ChargeAndRechargeDash();
        ProcessDash();
    }

    private void ProcessDash()
    {
        if (dashing)
        {      
            TurnOffGravity();
            myRigidbody.velocity = new Vector2(dashSpeed * lastInputBeforeZero, 0);
        }
        else if(!dashing)
        {
            myRigidbody.gravityScale = gravity;
            TurnOnGravity();
        }
    }

    private void ChargeAndRechargeDash()
    {
        if (!IsGrounded() && Input.GetButtonDown("Dash")) //can be used only once in air before touching ground
        {
            canDash = false;
        }
        else if (IsGrounded() || IsWalled() || CanDashAfterSpikeJump()) //Dash will recharge on wall but you can't dash from wall (viz Dash())
        {
            canDash = true;
        }
    }

    private IEnumerator EndDashCo()
    {
        yield return new WaitForSeconds(dashTime);
        dashing = false;
    }

    void SpikeJump()
    {
        if(spikeJumping.currentValue && !IsGrounded())
        {
            myRigidbody.velocity = new Vector2(0, 10);
            StartCoroutine(SpikeJumpCo(spikeJumpTime));
            spikeJumping.currentValue = false;
            isInAirAfterSpikeJumpDash = true;
            isInAirAfterSpikeJumpDoubleJump = true;
        }
        else if(spikeJumping.currentValue && IsGrounded())
        {
            isInAirAfterSpikeJumpDash = false;
        }

        if((IsGrounded() || IsWalled() || Input.GetButtonDown("Dash")) && isInAirAfterSpikeJumpDash)
        {
            isInAirAfterSpikeJumpDash = false;
        }

        if((IsGrounded() || IsWalled() || Input.GetButtonDown("Jump")) && isInAirAfterSpikeJumpDoubleJump)
        {
            isInAirAfterSpikeJumpDoubleJump = false;
        }
    }

    private IEnumerator SpikeJumpCo(float spikeJumpTime)
    {
        yield return new WaitForSeconds(spikeJumpTime);
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y);
    }

    private bool CanDashAfterSpikeJump()
    {
        return isInAirAfterSpikeJumpDash;
    }

    private bool CanDoubleJumpAfterSpikeJump()
    {
        return isInAirAfterSpikeJumpDoubleJump;
    }

    private void TurnOffGravity()
    {
        myRigidbody.gravityScale = 0;
    }

    private void TurnOnGravity()
    {
        myRigidbody.gravityScale = gravity;
    }

    private void LastInputBeforeZero()
    {
        if (InputHorizontal() != 0)
        {
            lastInputBeforeZero = InputHorizontal();
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, extraHeight, platformLayerMask);
        return raycastHit.collider != null;
    }

    private bool IsWallLefted()
    {
        RaycastHit2D raycastHitLeft = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, extraHeight, platformLayerMask);
        return raycastHitLeft.collider != null;
    }

    private bool IsWallRighted()
    {
        RaycastHit2D raycastHitRight = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, extraHeight, platformLayerMask);
        return raycastHitRight.collider != null;
    }

    private bool IsWalled()
    {
        return IsWallRighted() || IsWallLefted();
    }

    private void Knockback()
    {
        if(knockbackCount > 0)
        {
            if(knockbackRight)
            {
                myRigidbody.velocity = new Vector2(-knockbackIntensityX, knockbackIntensityY);
            }
            if(!knockbackRight)
            {
                myRigidbody.velocity = new Vector2(knockbackIntensityX, knockbackIntensityY);
            }
            knockbackCount -= Time.deltaTime;
        }
    }
}