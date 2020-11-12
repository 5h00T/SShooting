using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UniRx;

public class EnemyBullet : Bullet
{
    // public float Speed { get; set; }

    private VisualEffect visualEffect;

    private float disappearanceTime = 1;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {

    }

    void Start()
    {
        base.Start();

        visualEffect = GetComponent<VisualEffect>();
        visualEffect.SetFloat("DisappearanceTime", disappearanceTime);

        Attack = 1;
        LifeTime = 10;

        /*
        this.IsAlive
            .Where(alive => alive == false)
            .First()
            .Subscribe(_ => Dead());
        */
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = Speed * transform.up;
    }

    public void Dead()
    {
        // Destroy(gameObject);
        
        visualEffect.SetBool("Alive", false);
        GetComponent<CircleCollider2D>().enabled = false;
        Speed *= 0.1f;
        Destroy(this.gameObject, disappearanceTime);
    }
}