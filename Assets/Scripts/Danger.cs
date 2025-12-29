using UnityEngine;
using UnityEngine.SceneManagement;

public class Danger : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float x;
    [SerializeField] private float y;
    private BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();    
    }

    void FixedUpdate()
    {
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            GameObject.FindWithTag("Player").transform.SetPositionAndRotation(new Vector3(x, y, -2), transform.rotation);
        }
    }
}
