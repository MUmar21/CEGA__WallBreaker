using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;

public class DoTweenPractice : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float duration;
    [SerializeField] private Ease easeType = Ease.OutBounce;

    private void Start()
    {
        SequenceAnimations();

        //MoveAnim();
        //ScaleAnim();
        //RotateAnim();
    }

    private void Update()
    {
        //MoveAnim();
    }

    private void MoveAnim()
    {
        transform.DOMove(targetTransform.position, duration).SetEase(easeType)
            .OnStart(() =>
            {
                Debug.Log("Move Animation Started!!!");
                ScaleAnim();
            })
            .OnUpdate(() => 
                Debug.Log("Move Animation Is Running!!!!"
            ))
            .OnComplete(() =>
            {
                transform.localScale = Vector3.one;
                RotateAnim();
            });

        //transform.DOMoveX(transform.localPosition.x * 2f, 2f);
    }

    private void ScaleAnim()
    {
        transform.DOScaleX(3f, 1f).SetEase(Ease.OutQuad);
        //transform.DOPunchScale(transform.localScale*3f, duration);
    }

    private void RotateAnim()
    {
        transform.DORotate(new Vector3(0, 0, 360f),5f, RotateMode.FastBeyond360);
    }

    private void SequenceAnimations()
    {
        Sequence mySequence = DOTween.Sequence();

        Vector3 originalPos = transform.position;

        mySequence.AppendInterval(2f);

        mySequence.Append(transform.DOMove(targetTransform.position, duration).SetEase(easeType));
        mySequence.Append(transform.DOScale(transform.localScale * 2f, duration).SetEase(Ease.OutQuad));
        mySequence.Join(transform.DORotate(new Vector3(0f, 0f, 360f), 5f, RotateMode.FastBeyond360));
        mySequence.Insert(5f, transform.DOMove(originalPos, duration).SetEase(easeType));

        mySequence.Play();
    }
}
