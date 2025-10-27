using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    [SerializeField] private float delay = 0.1f;
    private string fullText;
    private string currentText = "";

    void Start()
    {
        fullText = GetComponent<TextMesh>().text;
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<TextMesh>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
