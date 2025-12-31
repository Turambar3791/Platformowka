using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    [SerializeField] private string interactWith;

    [Header("Door")]
    [SerializeField] private string sceneName;
    [SerializeField] private int whichLevelCompleted;

    [Header("Npc")]
    [SerializeField] private GameObject dialogueBoxToShow;
    [SerializeField] private GameObject dialogueAboutDoubleJump;

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
                if (whichLevelCompleted != 0)
                {
                    switch (whichLevelCompleted)
                    {
                        case 1:
                            SaveLoadSystem.Instance.data.is1stLevelCompeleted = true;
                            break;
                        case 2:
                            SaveLoadSystem.Instance.data.is2ndLevelCompleted = true;
                            break;
                        case 3:
                            SaveLoadSystem.Instance.data.is3rdLevelCompleted = true;
                            break;
                    }
                    SaveLoadSystem.Instance.SaveGame();
                }
                SceneManager.LoadScene(sceneName);
            }
            else if (interactWith == "npc")
            {
                if (!SaveLoadSystem.Instance.data.is2ndLevelCompleted)
                {
                    dialogueBoxToShow.SetActive(true);
                }
                else
                {
                    dialogueAboutDoubleJump.SetActive(true);
                }
            }           

        }
    }
}
