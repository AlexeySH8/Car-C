using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTypewriter : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float delay = 0.03f;
    private string fullText;

    private void OnEnable()
    {
        fullText = textMeshPro.text;
        textMeshPro.text = "";
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            AudioManager.Instance.PlayTextTypewriterSFX();
            textMeshPro.text = fullText.Substring(0, i + 1);
            yield return new WaitForSeconds(delay);
        }
    }
}
