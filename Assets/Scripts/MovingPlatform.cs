using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    [SerializeField] private Transform platformToMove;
    private Rigidbody2D rbPlatformToMove;
    [SerializeField] private Transform startingPosition;
    [SerializeField] private Transform destination;
    [SerializeField] private float speed = 8;
    [SerializeField] private GameObject button;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rbPlatformToMove = platformToMove.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) 
        {
            rbPlatformToMove.MovePosition(Vector2.MoveTowards(platformToMove.position, destination.position, speed * Time.deltaTime));
            button.SetActive(false);
        }
        else
        {
            rbPlatformToMove.MovePosition(Vector2.MoveTowards(platformToMove.position, startingPosition.position, speed * Time.deltaTime));
            button.SetActive(true);
        }
    }
}
