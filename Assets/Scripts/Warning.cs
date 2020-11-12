using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using System.Threading;

public class Warning : MonoBehaviour
{
    VisualEffect visualEffect;

    public BoolReactiveProperty IsComplete { get; set; } = new BoolReactiveProperty(false);

    void Start()
    {
        MainRoutine(this.GetCancellationTokenOnDestroy());
    }

    async UniTaskVoid MainRoutine(CancellationToken token)
    {
        await UniTask.Delay(7000, cancellationToken: token);

        IsComplete.Value = true;
    }
}
