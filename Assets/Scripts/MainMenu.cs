using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // MainMenu
    public void BtnStart()
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

    public void BtnStartNewGame(int slot)
    {
        SaveLoadSystem.Instance.SetSlot(slot);
        SaveLoadSystem.Instance.NewGame();
        SaveLoadSystem.Instance.SaveGame();
        SceneManager.LoadScene("Lobby");
    }

    public void BtnContinue(int slot)
    {
        SaveLoadSystem.Instance.SetSlot(slot);
        SaveLoadSystem.Instance.LoadGame();
        SceneManager.LoadScene("Lobby");
    }
}
