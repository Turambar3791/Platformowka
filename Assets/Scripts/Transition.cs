using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField] private int sceneNumber;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && boxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        { 
            SceneManager.LoadScene(sceneNumber);
        }
    }
}
