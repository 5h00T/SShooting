using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.VFX;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;

public enum EnemyStatus
{
    Spawn, // 敵の発生中
    Alive,
    Dead
}

public abstract class Enemy : GameAircraft
{
    [SerializeField]
    Camera gameCamera;

    protected bool isAlive = true;
    protected int score;
    protected float spawnTime = 2; // パーティクルが集まるのにかかる時間

    protected ReactiveProperty<EnemyStatus> status;

    protected CancellationTokenSource tokenSource;

    protected void Start()
    {
        gameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        tokenSource = new CancellationTokenSource();
        status = new ReactiveProperty<EnemyStatus>(EnemyStatus.Spawn);
        HP = new FloatReactiveProperty(0);

        status.Subscribe(value => OnStatusChanged(value));
    }

    public void AddDamage(float damage)
    {
        // カメラ外ではダメージ無効
        // 画面左下のワールド座標をビューポートから取得
        Vector3 min = gameCamera.ViewportToWorldPoint(new Vector3(0.05f, 0.05f, 4)); // z=オブジェクトの座標-カメラ座標
        // 画面右上のワールド座標をビューポートから取得
        Vector3 max = gameCamera.ViewportToWorldPoint(new Vector3(0.95f, 0.95f, 4));

        if(min.x < transform.position.x && transform.position.x < max.x && min.y < transform.position.y && transform.position.y < max.y)
        {
            HP.Value -= damage;
        }
    }

    private void OnDestroy()
    {
        tokenSource.Cancel();
    }

    protected abstract void OnMainRoutineFinish();

    protected abstract void OnStatusChanged(EnemyStatus enemyStatus);
}
