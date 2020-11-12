using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.VFX;

public abstract class Boss : Enemy
{
    [SerializeField]
    protected Gradient particleColor;

    [SerializeField]
    protected List<Boss1ShotPosition> boss1KnifeShotPosition; // 刀身部分

    [SerializeField]
    protected Boss1ShotPosition boss1CenterShotPosition;

    protected PolygonCollider2D polygonCollider2D;

    protected VisualEffect vfx;

    protected float maxHP = 50;
    protected float explosionLifeTime = 4; // 体力が0になって爆発してからオブジェクトが削除されるまでの時間

    protected Tweener moveTweener;
    protected Sequence moveSequence;

    public BoolReactiveProperty IsComplete { get; set; } = new BoolReactiveProperty(false);

    protected void Start()
    {
        base.Start();

        polygonCollider2D = GetComponent<PolygonCollider2D>();
        vfx = GetComponent<VisualEffect>();

        // コメントアウトして不死身にする
        /*
        HP.Where(value => value <= 0)
            .Subscribe(_ => HitPointZero());

        */
    }

    protected void HitPointZero()
    {
        status.Value = EnemyStatus.Dead;

        vfx.SetBool("Alive", false);

        // スコアを加算
        ScoreManager.GetInstance().Score.Value += score;

        Destroy(this.gameObject, explosionLifeTime);
    }

    protected abstract UniTaskVoid MainRoutine(CancellationToken token);

    protected override void OnMainRoutineFinish()
    {
        throw new System.NotImplementedException();
    }

    // protected abstract void OnStatusChanged(EnemyStatus enemyStatus);
}
