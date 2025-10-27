using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField] private string interactWith;
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject dialogueBoxToShow;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && boxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (interactWith == "door")
            {
                SceneManager.LoadScene(sceneName);
            }
            else if (interactWith == "npc")
            {
                dialogueBoxToShow.SetActive(true);
            }           

        }
    }
}
