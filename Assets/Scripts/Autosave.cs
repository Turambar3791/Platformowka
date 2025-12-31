using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Autosave : MonoBehaviour
{
    [SerializeField] private GameObject autosaveIcon;
    [SerializeField] private string sceneToLoad = "Lobby";
    private float rotationSpeed = 120f;

    void Start()
    {
        StartCoroutine(Save());
    }

    void Update()
    {
        autosaveIcon.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
    }

    IEnumerator Save()
    {
        yield return new WaitForSeconds(3);
        if (SaveLoadSystem.Instance.data.is3rdLevelCompleted)
        {
            SceneManager.LoadScene("TheEnd");
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
