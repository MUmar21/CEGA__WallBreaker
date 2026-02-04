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
            //Time.timeScale = 1f;
            //StopCoroutine(unfreezeCoroutine);
            return;
        }

        Time.timeScale = 0.5f;
        unfreezeCoroutine = StartCoroutine(UnFreeze(duration));
    }

    private IEnumerator UnFreeze(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        unfreezeCoroutine = null;
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
