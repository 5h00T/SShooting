using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;
using System.Threading;

public class Enemy1 : NormalEnemy
{
    [SerializeField]
    Gradient particleColor;

    [SerializeField]
    Enemy1ShotPosition enemy1ShotPosition;

    protected VisualEffect vfx;

    private PolygonCollider2D polygonCollider2D;

    float maxHP = 15;
    float explosionLifeTime = 4; // 体力が0になって爆発してからオブジェクトが削除されるまでの時間

    public Tweener moveTweener;

    public Action<CancellationToken> moveAction;
    public Action<CancellationToken> attackAction;
    

    new void Start()
    {
        base.Start();

        polygonCollider2D = GetComponent<PolygonCollider2D>();
        vfx = GetComponent<VisualEffect>();

        vfx.SetGradient("ParticleColor", particleColor);
        vfx.SetFloat("SpawnLifeTime", spawnTime);

        Observable.Timer(TimeSpan.FromSeconds(spawnTime))
            .Subscribe( _ => status.Value = EnemyStatus.Alive);

        HP.Value = maxHP;
        score = 100;

        HP.Where(value => value <= 0)
            .Subscribe(_ => HitPointZero());
    }

    void HitPointZero()
    {
        status.Value = EnemyStatus.Dead;

        vfx.SetBool("Alive", false);

        // スコアを加算
        ScoreManager.GetInstance().Score.Value += score;

        Destroy(this.gameObject, explosionLifeTime);
    }

    async UniTask MainRoutine(CancellationToken token)
    {
        MoveRoutine(token);
        AttackRoutine(token);

    }
    

    protected override void OnStatusChanged(EnemyStatus enemyStatus)
    {
        switch (enemyStatus)
        {
            case EnemyStatus.Alive:
                polygonCollider2D.enabled = true;
                MainRoutine(tokenSource.Token).Forget();
                break;
            case EnemyStatus.Dead:
                polygonCollider2D.enabled = false;
                moveTweener.Kill();
                tokenSource.Cancel();
                break;
        }
    }

    protected override void OnMainRoutineFinish()
    {
        throw new NotImplementedException();
    }

    public override async UniTaskVoid MoveRoutine(CancellationToken token)
    {
        moveAction.Invoke(token);
    }

    public override async UniTaskVoid AttackRoutine(CancellationToken token)
    {
        
        await UniTask.Delay(3000, cancellationToken: token);

        if (token.IsCancellationRequested)
        {
            Debug.Log("cancel");
            return;
        }

        Debug.Log("shot");
        enemy1ShotPosition.Shot();
    }
}
