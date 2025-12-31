using TMPro;
using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    [SerializeField] private TextAsset creditsTxt;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float padding = 200f;
    private RectTransform rectTransform;
    private TextMeshProUGUI textMeshProUGUI;
    private float endY;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = creditsTxt.text;
        endY = textMeshProUGUI.preferredHeight + padding;
    }

    void Update()
    {
        rectTransform.anchoredPosition += Vector2.up * speed * Time.deltaTime;

        if ((rectTransform.anchoredPosition.y > endY) || Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("QUIT");
            Application.Quit();
        }
    }
}
