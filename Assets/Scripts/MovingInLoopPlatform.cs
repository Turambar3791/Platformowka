using UnityEngine;

public class MovingInLoopPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Transform destination;
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;
    [SerializeField] private float speed;
    private GameObject player;
    private Rigidbody2D rbPlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        destination = target2;
        player = GameObject.FindGameObjectWithTag("Player");
        rbPlayer = player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(Vector2.MoveTowards(transform.position, destination.position, speed * Time.deltaTime));

        if (Vector2.Distance(transform.position, destination.position) < 0.05f)
        {
            destination = (destination == target2) ? target1 : target2;
        }

        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            rbPlayer.MovePosition(Vector2.MoveTowards(player.transform.position, destination.position, speed * Time.deltaTime));
        }
    }
}
