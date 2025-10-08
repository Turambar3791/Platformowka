using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // poruszanie siê
    private float speed = 8;
    private Rigidbody2D rb;

    // flipowanie
    private bool isFacingRight = true;
    private SpriteRenderer sprite;

    // skakanie
    [Header("Skakanie")]
    [SerializeField] private float jumpHight = 12;
    [SerializeField] private float jumpTime = 0.5f;
    private float jumpTimeCounter;

    // czas kojota
    [Header("Czas kojota")]
    [SerializeField] private float coyoteTime = 0.5f;
    private float coyoteTimeCounter;

    // kolizje
    [Header("Kolizje")]
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform wallCheckLeft;
    [SerializeField] Transform wallCheckRight;
    private BoxCollider2D groundCheckColl;
    private BoxCollider2D wallCheckLeftColl;
    private BoxCollider2D wallCheckRightColl;

    // dash
    [Header("Dash")]
    [SerializeField] private float dashForce = 2f;
    [SerializeField] private float dashTime = 1f;
    private float dashTimeCounter;
    [SerializeField] private float dashCooldown = 1f;
    private float dashCooldownCounter;

    // pauzowanie
    private bool isPaused = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheckColl = groundCheck.GetComponent<BoxCollider2D>();
        wallCheckLeftColl = wallCheckLeft.GetComponent<BoxCollider2D>();
        wallCheckRightColl = wallCheckRight.GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // pauzowanie i odpauzowywanie
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1;
                isPaused = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // poruszanie siê prawo-lewo
        float x = Input.GetAxisRaw("Horizontal");
        // cancelowanie ruchu w œcianê podczas wallJumpa
        if ((x < 0 && IsTouchingWallOnTheLeft()) || (x > 0 && IsTouchingWallOnTheRight()))
        {
            x = 0;
        }
        rb.linearVelocity = new Vector2(x * speed, rb.linearVelocity.y);

        // flip
        if (x < 0 && isFacingRight)
        {
            Flip();
        }
        else if (x > 0 && !isFacingRight)
        {
            Flip();
        }

        // czas kojota
        if (!IsGrounded())
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        else
        {
            coyoteTimeCounter = coyoteTime;
        }

        // skakanie
        if (Input.GetKeyDown(KeyCode.C))
        {
            jumpTimeCounter = jumpTime;
        }

        if (jumpTimeCounter > 0)
        {
            jumpTimeCounter -= Time.deltaTime;

            if (coyoteTimeCounter > 0f || IsGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHight);
                jumpTimeCounter = 0;
            }
        }

        // zeœlizgiwanie siê ze œcian
        if (IsTouchingWallOnTheLeft() || IsTouchingWallOnTheRight())
        {
            jumpTimeCounter = 0;
            rb.gravityScale = 6;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }
        else
        {
            rb.gravityScale = 10;
        }

        // wallJump
        if (IsTouchingWallOnTheLeft() && Input.GetKeyDown(KeyCode.C))
        {
            rb.gravityScale = 10;
            rb.linearVelocity = new Vector2(1, 1) * jumpHight;
        }

        if (IsTouchingWallOnTheRight() && Input.GetKeyDown(KeyCode.C))
        {
            rb.gravityScale = 10;
            rb.linearVelocity = new Vector2(-1, 1) * jumpHight;
        }


        // dash
        if (Input.GetKeyDown(KeyCode.X) && dashCooldownCounter <= 0)
        {
            dashTimeCounter = dashTime;
            dashCooldownCounter = dashCooldown;
        }

        if (dashTimeCounter > 0)
        {
            if (isFacingRight)
            {
                rb.linearVelocity = new Vector2(dashForce, 0);
            }
            else
            {
                rb.linearVelocity = new Vector2(-dashForce, 0);
            }
            dashTimeCounter -= Time.deltaTime;
        } 
        
        if (dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime;
        }
    }

    private bool IsGrounded()
    { 
        return groundCheckColl.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private bool IsTouchingWallOnTheLeft()
    {
        return wallCheckLeftColl.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private bool IsTouchingWallOnTheRight()
    { 
        return wallCheckRightColl.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private void Flip()
    { 
        sprite.flipX = isFacingRight;
        isFacingRight = !isFacingRight;
    }
}
