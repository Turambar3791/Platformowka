using UnityEngine;

public class Player : MonoBehaviour
{
    // powciskane klawisze
    private bool jumpKey = false;
    private bool dashKey = false;

    // poruszanie siê
    [Header("Poruszanie siê")]
    [SerializeField] private float speed = 8;
    private Rigidbody2D rb;

    // flipowanie
    private bool isFacingRight = true;
    private SpriteRenderer sprite;

    // skakanie
    [Header("Skakanie")]
    [SerializeField] private float jumpHight = 12;
    [SerializeField] private float jumpTime = 0.5f;
    private float jumpTimeCounter;
    private int maxJumps = 2;
    private int remainingJumps;

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
    [Header("Pauzowanie")]
    private bool isPaused = false;
    [SerializeField] private Canvas pauseScreen;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheckColl = groundCheck.GetComponent<BoxCollider2D>();
        wallCheckLeftColl = wallCheckLeft.GetComponent<BoxCollider2D>();
        wallCheckRightColl = wallCheckRight.GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        pauseScreen.enabled = false;
        Time.timeScale = 1;

        if (SaveLoadSystem.Instance.data.is2ndLevelCompleted)
        {
            maxJumps = 2;
        }
        else
        {
            maxJumps = 1;
        }
    }

    private void Update()
    {
        // pauzowanie i odpauzowywanie
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }

        // naciskanie klawiszy
        if (Input.GetKeyDown(KeyCode.C) && (IsGrounded() || coyoteTimeCounter > 0f || remainingJumps > 0)) 
        {
            jumpKey = true;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            dashKey = true;
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
        rb.linearVelocity = new Vector2(x * speed * Time.deltaTime, rb.linearVelocity.y);

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
            remainingJumps = maxJumps;
        }

        // zeœlizgiwanie siê ze œcian
        if (IsTouchingWallOnTheLeft() || IsTouchingWallOnTheRight())
        {
            jumpTimeCounter = 0;
            rb.gravityScale = 6;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            remainingJumps += 1;
        }
        else
        {
            rb.gravityScale = 10;
        }

        // wallJump
        if (jumpKey)
        {
            if (IsTouchingWallOnTheLeft())
            {
                rb.gravityScale = 10;
                rb.linearVelocity = new Vector2(1, 1) * jumpHight * Time.deltaTime;
                remainingJumps = maxJumps - 1;
                jumpKey = false;
                return;
            }

            if (IsTouchingWallOnTheRight())
            {
                rb.gravityScale = 10;
                rb.linearVelocity = new Vector2(-1, 1) * jumpHight * Time.deltaTime;
                remainingJumps = maxJumps - 1;
                jumpKey = false;
                return;
            }
        }

        // skakanie
        if (jumpKey)
        {
            jumpTimeCounter = jumpTime;
        }

        if (jumpTimeCounter > 0)
        {
            jumpTimeCounter -= Time.deltaTime;

            if (coyoteTimeCounter > 0f || IsGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHight * Time.deltaTime);
                remainingJumps = maxJumps - 1;
                jumpTimeCounter = 0;
            }
            else if (remainingJumps > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHight * Time.deltaTime);
                remainingJumps--;
            }

            jumpKey = false;
        }

        // dash
        if (dashKey && dashCooldownCounter <= 0)
        {
            dashTimeCounter = dashTime;
            dashCooldownCounter = dashCooldown;
        }

        if (dashTimeCounter > 0)
        {
            if (isFacingRight)
            {
                rb.linearVelocity = new Vector2(dashForce * Time.deltaTime, 0);
            }
            else
            {
                rb.linearVelocity = new Vector2(-dashForce * Time.deltaTime, 0);
            }
            dashKey = false;
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

    public void Pause()
    {
        Time.timeScale = 0;
        pauseScreen.enabled = true;
        isPaused = true;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pauseScreen.enabled = false;
        isPaused = false;
    }
}
