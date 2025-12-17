using UnityEngine;
using UnityEngine.UI;

public class CheckingSaveSlots : MonoBehaviour
{
    [SerializeField] private Button[] buttons;

    void Start()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            int slot = i + 1;
            SaveLoadSystem.Instance.SetSlot(slot);
            SaveLoadSystem.Instance.LoadGame();
            buttons[i].interactable = (SaveLoadSystem.Instance.data.is1stLevelCompeleted);
        }
    }
}
