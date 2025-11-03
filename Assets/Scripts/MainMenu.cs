using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // MainMenu
    public void BtnNewGame()
    {
        SceneManager.LoadScene("Lobby");
        SaveLoadSystem.Instance.NewGame();
    }

    public void BtnLoadGame()
    {
        SceneManager.LoadScene("MenuLoadGame");
    }

    public void BtnKeybinds()
    {
        SceneManager.LoadScene("MenuKeybinds");
    }


    public void BtnQuit()
    {
        Application.Quit();
    }

    // SubScenes

    public void BtnBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
