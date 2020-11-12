using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UniRx;
using UniRx.Triggers;

public class PlayerShotTrajectory : MonoBehaviour
{
    VisualEffect visualEffect;

    void Start()
    {
        visualEffect = GetComponent<VisualEffect>();

        /*
        this.UpdateAsObservable()
            .Subscribe(_ => AliveCheck());

        
        Observable.TimerFrame(30)
            .Subscribe(_ => visualEffect.SetBool("Alive", false));
        */
    }

    void AliveCheck()
    {
        // Debug.Log(visualEffect.GetBool("Alive"));
        Debug.Log(visualEffect.GetFloat("Age"));
        Debug.Log(visualEffect.HasFloat("Age"));
        
        if (visualEffect.GetFloat("Age") > 1)
        {
            Destroy(this.gameObject);
        }
    }
}
