using DG.Tweening;
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
        flashImage.gameObject.SetActive(false);
    }

    public void Flash(Color color, float duration)
    {
        flashImage.DOKill();

        float startAlpha = 0.8f;
        Color startColor = new Color(color.r, color.g, color.b, startAlpha);

        flashImage.color = startColor;
        flashImage.gameObject.SetActive(true);

        flashImage.DOFade(0f, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                flashImage.color = originalColor;
                flashImage.gameObject.SetActive(false);
            });
    }
}