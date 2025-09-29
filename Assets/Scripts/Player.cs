using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 moveDelta;
    private float fixTimeMovement = 0.25f;
    private float jumpHight = 25;
    private Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform wallCheckLeft;
    [SerializeField] Transform wallCheckRight;
    private BoxCollider2D groundCheckColl;
    private BoxCollider2D wallCheckLeftColl;
    private BoxCollider2D wallCheckRightColl;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheckColl = groundCheck.GetComponent<BoxCollider2D>();
        wallCheckLeftColl = wallCheckLeft.GetComponent<BoxCollider2D>();
        wallCheckRightColl = wallCheckRight.GetComponent<BoxCollider2D>();
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
        moveDelta = new Vector2(x, 0);
        transform.Translate(moveDelta * fixTimeMovement);
        
        // skakanie
        if (Input.GetKeyDown(KeyCode.C) && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHight);
        }

        // zeœlizgiwanie siê ze œcian
        if (IsTouchingWallOnTheLeft() || IsTouchingWallOnTheRight())
        {
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
            rb.linearVelocity = new Vector2((5 - rb.linearVelocity.x), jumpHight);
        }

    }

    private bool IsGrounded()
    { 
        return groundCheckColl.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private bool IsTouchingWallOnTheLeft()
    { 
        return wallCheckLeftColl.IsTouchingLayers(LayerMask.GetMask("Wall"));
    }

    private bool IsTouchingWallOnTheRight()
    {
        return wallCheckRightColl.IsTouchingLayers(LayerMask.GetMask("Wall"));
    }
}
