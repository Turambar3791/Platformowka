using UnityEngine;
using UnityEngine.SceneManagement;

public class Danger : MonoBehaviour
{
    [SerializeField] private int sceneToLoadAnew;
    private BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();    
    }

    void FixedUpdate()
    {
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            SceneManager.LoadScene(0);
        }
    }
}
