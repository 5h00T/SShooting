using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public abstract class Bullet : MonoBehaviour
{
    public float Speed { get; set; }

    protected float Attack { get; set; } // 攻撃力

    protected float Age { get; set; } // 生存時間

    protected float LifeTime { get; set; } // 寿命

    // public BoolReactiveProperty IsAlive { get; set; } = new BoolReactiveProperty(true); // 生存

    System.IDisposable disposable;

    protected Rigidbody2D rigidbody2D;

    protected abstract void OnTriggerEnter2D(Collider2D collision);

    protected void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        disposable = this.UpdateAsObservable()
            .Subscribe(_ => CheckLifeTime());
    }

    protected void CheckLifeTime()
    {
        if (LifeTime - Age <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    protected void Initialize()
    {

    }

    private void OnDestroy()
    {
        // disposable.Dispose();
    }
}
