using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundIcosphere : MonoBehaviour
{
    void Start()
    {
        transform.DOLocalRotate(new Vector3(-360f, 0, 0), 30f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}
