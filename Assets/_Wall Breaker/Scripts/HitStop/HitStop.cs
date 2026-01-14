using System.Collections;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    public static HitStop Instance;
    private Coroutine unfreezeCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    public void Freeze(float duration)
    {
        if (unfreezeCoroutine != null)
        {
            StopCoroutine(unfreezeCoroutine);
            Time.timeScale = 1f;
        }

        Time.timeScale = 0f;
        unfreezeCoroutine = StartCoroutine(UnFreeze(duration));
    }

    private IEnumerator UnFreeze(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }

    private void OnDisable()
    {
        if (unfreezeCoroutine != null)
        {
            StopCoroutine(unfreezeCoroutine);
            Time.timeScale = 1f;
        }
    }
}
