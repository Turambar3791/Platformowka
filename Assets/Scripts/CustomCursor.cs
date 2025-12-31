using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private Canvas cursorCanvas;
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Cursor.visible = false;
        DontDestroyOnLoad(cursorCanvas);
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        rectTransform.position = mousePos;
    }
}
