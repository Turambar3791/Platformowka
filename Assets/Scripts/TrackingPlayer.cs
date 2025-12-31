using System.Collections.Generic;
using UnityEngine;

public class TrackingPlayer : MonoBehaviour
{
    public static Queue<Vector2> positions = new Queue<Vector2>();
    [SerializeField] private float recordDistance = 0.2f;
    private Vector2 lastPos;

    void Start()
    {
        positions.Clear();
        lastPos = transform.position;
        positions.Enqueue(lastPos);
    }

    void FixedUpdate()
    {
        Vector2 current = transform.position;
        if (Vector2.Distance(current, lastPos) > recordDistance)
        {
            positions.Enqueue(current);
            lastPos = current;
        }
    }
}
