using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;
using UnityEngine;
using System;

public class PlayerBullet : Bullet
{
    [SerializeField]
    GameObject hitEffect;

    [SerializeField]
    GameObject trajectEffect; // 軌跡

    private float maxAttack = 6;
    private float minAttack = 3;

    void Start()
    {
        base.Start();
        
        Attack = maxAttack;
        LifeTime = 1;
        Speed = 4;

        this.UpdateAsObservable()
            .Subscribe(_ => Move())
            .AddTo(this);

        this.UpdateAsObservable()
            .Subscribe(_ => AttackChange());

        Observable.IntervalFrame(2)
            .Subscribe(_ => SpawnTrajectory())
            .AddTo(this);

        Destroy(gameObject, 1);
    }

    private void AttackChange()
    {
        Attack = Mathf.Lerp(maxAttack, minAttack, LifeTime);
    }

    void Move()
    {
        // transform.Translate(0, speed, 0);
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = Speed * transform.up;
    }

    void SpawnTrajectory()
    {
        GameObject gameObject = Instantiate(trajectEffect, transform.position, Quaternion.identity);
        Destroy(gameObject, 1);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ddd");

        if (collision.gameObject.tag == "Enemy")
        {
            // collision.GetComponent<Enemy>()._hp.Value -= Attack;
            collision.GetComponent<Enemy>().AddDamage(Attack);

            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
