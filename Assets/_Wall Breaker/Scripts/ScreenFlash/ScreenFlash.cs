using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public static ScreenFlash Instance;

    [SerializeField] private Image flashImage;
    private Color originalColor;

    private void Awake()
    {
        Instance = this;
        originalColor = flashImage.color;
    }

    public void Flash(Color color, float duration)
    {
        StartCoroutine(FlashCoroutine(color, duration));
    }

    private IEnumerator FlashCoroutine(Color color, float duration)
    {
        float elapsed = 0;
        flashImage.color = color;
        flashImage.gameObject.SetActive(true);

        while (elapsed < duration)
        {
            float alpha = 0.8f - (elapsed / duration);

            flashImage.color = new Color(color.r, color.g, color.b, alpha);

            elapsed += Time.deltaTime;
            yield return null;
        }

        flashImage.color = originalColor;
        flashImage.gameObject.SetActive(false);
    }
}
