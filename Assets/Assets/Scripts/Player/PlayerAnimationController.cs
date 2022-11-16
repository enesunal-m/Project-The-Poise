using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] float animScaler;
    [SerializeField] float duration;
    void Start()
    {
        ScaleAnimation();
    }

    public void ScaleAnimation()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 scaleTo = new Vector3(transform.localScale.x, transform.localScale.y + animScaler, transform.localScale.z);
        transform.DOLocalMoveY(transform.position.y + animScaler, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
        transform.DOScale(scaleTo, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
