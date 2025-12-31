using System.Collections.Generic;
using UnityEngine;

public class FollowingPlayer : MonoBehaviour
{
    [Header("For following")]
    [SerializeField] private float speed = 8;
    [SerializeField] private int delay = 10;

    private Queue<Vector2> queue = new Queue<Vector2>();
    private int lastCount = 0;

    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    [Header("Starting position")]
    [SerializeField] private float x;
    [SerializeField] private float y;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();  
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Player")) || GameObject.FindWithTag("Danger").GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            transform.SetPositionAndRotation(new Vector3(x, y, 0), transform.rotation);
            queue.Clear();
            lastCount = TrackingPlayer.positions.Count;
            delay = 10;
            spriteRenderer.flipX = false;
        }

        if (TrackingPlayer.positions.Count > lastCount)
        {
            Vector2[] array = TrackingPlayer.positions.ToArray();

            for (int i = lastCount; i < array.Length; i++)
            {
                queue.Enqueue(array[i]);
            }

            lastCount = array.Length;
        }

        while (delay > 0 && queue.Count > 0)
        {
            queue.Dequeue();
            delay--;
        }

        if (queue.Count > 0)
        {
            Vector2 target = queue.Peek();

            float directionX = target.x - transform.position.x;

            if (directionX > 0.01f)
            {
                spriteRenderer.flipX = false;
            }
            else if (directionX < -0.01f)
            {
                spriteRenderer.flipX = true;
            }

            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, target) < 0.05f)
            {
                queue.Dequeue();
            }
        }
    }
}
