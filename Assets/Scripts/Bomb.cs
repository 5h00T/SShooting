using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UniRx;
using UniRx.Triggers;
using System.Threading;
using Cysharp.Threading.Tasks;

public class Bomb : MonoBehaviour
{
    private VisualEffect visualEffect;

    public BoolReactiveProperty isComplete { get; set; }

    void Awake()
    {
        visualEffect = GetComponent<VisualEffect>();

        isComplete = new BoolReactiveProperty(false);

        // Start内でStopが動かない
        // visualEffect.SendEvent("OnStop");
        // visualEffect.Stop();
        // visualEffect.pause = true;
        /*
        Observable.NextFrame()
            .Subscribe(_ => visualEffect.Stop());
        */

        MainRoutine();
    }

    public async UniTaskVoid MainRoutine()
    {
        await UniTask.Delay(3000);

        visualEffect.Stop();

        await UniTask.Delay(1000);

        isComplete.Value = true;
    }

    /// <summary>
    /// 弾を消す処理と一定フレーム毎にダメージを与える
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
            collision.GetComponent<EnemyBullet>().Dead();
        }

        if(collision.gameObject.tag == "Enemy")
        {
            // collision.GetComponent<Enemy>().AddDamage(50);
        }
    }
}
