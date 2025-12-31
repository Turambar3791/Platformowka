using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool is1stLevelCompeleted = false;
    public bool is2ndLevelCompleted = false;
    public bool is3rdLevelCompleted = false;
}


public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem Instance;
    public GameData data = new GameData();

    private int currentSlot = 1;
    private string savePath => Application.persistentDataPath + $"/savegame_slot{currentSlot}.json";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            data = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            SaveGame();
        }
    }

    public void NewGame()
    {
        data = new GameData();
        SaveGame();
    }

    public void SetSlot(int slot)
    {
        currentSlot = slot;
        LoadGame();
    }
}
