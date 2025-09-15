using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 moveDelta;
    private float fixTimeMovement = 0.25f;
    private Vector2 falling;
    private LayerMask groundLayer;
    private BoxCollider2D collider;

    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {     
        moveDelta = Vector2.zero;
        falling = new Vector2(0, -1);

        transform.Translate(falling);


        float x = Input.GetAxisRaw("Horizontal");

        moveDelta = new Vector2(x, 0);

        transform.Translate(moveDelta * fixTimeMovement);


    }
}
