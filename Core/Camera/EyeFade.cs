using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EyeFade : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;

    private void Start()
    {
        fadeImage.color = Color.black;
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = 1f - (t / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, a);
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);
    }
}
