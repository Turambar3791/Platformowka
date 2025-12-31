using System.Collections;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    [SerializeField] private float delay = 0.1f;
    [SerializeField] private TextAsset fileWithFullText;
    private string fullText;
    private string[] fullTextTab;
    private string currentText = "";
    [SerializeField] private GameObject dialogueBox;
    public bool isTextShowingFinished = false;

    void OnEnable()
    {
        fullText = fileWithFullText.ToString();
        fullTextTab = fullText.Split("]");
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullTextTab.Length; i++)
        {
            for (int j = 0; j <= fullTextTab[i].Length; j++)
            {
                currentText = fullTextTab[i].Substring(0, j);
                this.GetComponent<TextMesh>().text = currentText;
                yield return new WaitForSeconds(delay);
            }
            if (!((i + 1) == fullTextTab.Length))
            {
                yield return new WaitForSeconds(delay + 0.9f);
            }
            this.GetComponent<TextMesh>().text = "";
        }

        isTextShowingFinished = true;
        this.GetComponent<TextMesh>().text = fullText;
        dialogueBox.SetActive(false);    
    }
}
