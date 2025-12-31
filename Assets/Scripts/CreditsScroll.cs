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

    [SerializeField] private GameObject textAboutSkip;
    private bool isEscPressedOnce = false;

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

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isEscPressedOnce)
            {
                Application.Quit();
            }
            else
            {
                isEscPressedOnce = true;
                textAboutSkip.SetActive(true);
            }
        }

        if (rectTransform.anchoredPosition.y > endY)
        {
            Application.Quit();
        }
    }
}
