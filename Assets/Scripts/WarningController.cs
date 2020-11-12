using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningController : MonoBehaviour
{
    [SerializeField]
    GameObject quad;

    Renderer renderer;

    void Start()
    {
        
        renderer = quad.GetComponent<Renderer>();

        // renderer.material.SetFloat("Vector1_B7CC7423", 0.5f);

        /*
        transform.DOScale(new Vector3(1, 1, 1), 1).SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart)
            .OnComplete(() => transform.DOScale(new Vector3(1, 0, 1), 0));
        */

        DOVirtual.Float(0, 2, 1, value =>
        {
            renderer.material.SetFloat("Vector1_B7CC7423", value);
        }).SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
        //    .OnStepComplete(() => transform.DOScale(new Vector3(1, 0, 1), 0));
    }
}
