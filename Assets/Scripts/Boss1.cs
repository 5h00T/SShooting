using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;

public class Boss1 : Boss
{
    protected override async UniTaskVoid MainRoutine(CancellationToken token)
    {
        await UniTask.Delay(3000, cancellationToken: token);

        while (true)
        {
            moveSequence = DOTween.Sequence();
            moveSequence.Append(transform.DOShakePosition(4, new Vector3(0.2f, 0.2f, 0), 0, 40, false, false)
                .SetRelative(true)
                .SetEase(Ease.InOutSine));
            moveSequence.Join(transform.DOShakePosition(4, new Vector3(0.1f, 0.1f, 0), 1, 40, false, false)
                .SetRelative(true)
                .SetEase(Ease.InExpo));

            moveSequence.Play();

            await UniTask.Delay(5000);


            // 左右に振って弾を撒く
            Sequence rotateSequence = DOTween.Sequence();
            rotateSequence.Append(transform.DORotate(new Vector3(0, 0, 100), 1));
            rotateSequence.Append(transform.DORotate(new Vector3(0, 0, 260), 1));
            rotateSequence.Append(transform.DORotate(new Vector3(0, 0, 100), 1));
            rotateSequence.Append(transform.DORotate(new Vector3(0, 0, 180), 1));
            rotateSequence.Play();

            for (int i = 0; i < 20; i++)
            {
                foreach (var sp in boss1KnifeShotPosition)
                {
                    sp.Shot1();
                }

                await UniTask.Delay(200);
            }


            await UniTask.Delay(2000);

            // 全方位10way弾
            while (true)
            {
                float angle = UnityEngine.Random.Range(0, 360);
                int way = 10;
                float deltaAngle = 360 / 10f;
                for (int i = 0; i < way; i++)
                {
                    boss1CenterShotPosition.Shot2(angle);
                    angle += deltaAngle;
                }

                await UniTask.Delay(300);
            }
        }
    }

    protected override void OnStatusChanged(EnemyStatus enemyStatus)
    {
        switch (enemyStatus)
        {
            case EnemyStatus.Alive:
                polygonCollider2D.enabled = true;
                MainRoutine(this.GetCancellationTokenOnDestroy()).Forget();
                break;
            case EnemyStatus.Dead:
                polygonCollider2D.enabled = false;
                moveTweener.Kill();
                moveSequence.Kill();
                break;
        }
    }

    new void Start()
    {
        base.Start();

        HP.Value = maxHP;
        score = 100;
        spawnTime = 2;

        vfx.SetGradient("ParticleColor", particleColor);
        vfx.SetFloat("SpawnLifeTime", spawnTime);

        Observable.Timer(TimeSpan.FromSeconds(spawnTime))
            .Subscribe(_ => status.Value = EnemyStatus.Alive);
    }
}
