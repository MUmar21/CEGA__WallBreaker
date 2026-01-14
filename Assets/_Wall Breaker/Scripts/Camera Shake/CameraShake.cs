using System.Collections;
using DG.Tweening;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;
    Sequence sequence;

    private void Awake()
    {
        Instance = this;
    }

    public void Shake(float duration, float magnitude)
    {
        originalPosition = transform.position;

        sequence.Kill(true);

        sequence = DOTween.Sequence();

        sequence.Append(transform.DOShakePosition(duration, magnitude).
            OnComplete(() =>
            {
                transform.position = originalPosition;
            }));

        sequence.Play();

        //if (shakeCoroutine != null)
        //{
        //    StopCoroutine(shakeCoroutine);
        //}
        //shakeCoroutine = StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float timeElapsed = 0f;
        transform.localPosition = originalPosition;

        while (timeElapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPosition + new Vector3(x, y, 0);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    private void OnDisable()
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            transform.localPosition = originalPosition;
        }
    }
}
