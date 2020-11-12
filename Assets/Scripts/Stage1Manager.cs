using System.Collections;
using System.Collections.Generic;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;
using System.Threading;

public class Stage1Manager : MonoBehaviour
{

    [SerializeField]
    private Warning warningObject;
    
    [SerializeField]
    private Enemy1 enemy1;

    [SerializeField]
    private Boss boss1;

    void Start()
    {
        MainRoutineAsync();
    }

    async UniTaskVoid MainRoutineAsync()
    {
        await UniTask.Delay(1000);

        
        float[] x = { -1, 0, 1};
        for (int i = 0; i < x.Length; i++)
        {
            float y = 2;
            Enemy1 enemy = Instantiate(enemy1, new Vector3(x[i], y, -6), Quaternion.identity);
            enemy.moveAction = (cancellationToken) =>
            {
                Debug.Log("moveAction");

                enemy.moveTweener = enemy.transform.DOPath(new Vector3[] { new Vector3(0, -2f), new Vector3(0, 1f) }, 9, pathMode: PathMode.TopDown2D, pathType: PathType.CatmullRom)
                .SetLookAt(0.1f, enemy.transform.forward, enemy.transform.right)
                .SetRelative(true);
            };
        }

        await UniTask.Delay(1000);

        for(int i = 0; i < 8; i++)
        {
            Enemy1 enemy = Instantiate(enemy1, new Vector3(1.1f, 2f, -6), Quaternion.identity);
            enemy.moveAction = (CancellationToken) =>
            {
                enemy.moveTweener = enemy.transform.DOPath(new Vector3[] {
                                                                            new Vector3(-0.2f, 1.9f, -6),
                                                                            new Vector3(-0.1f, 0.6f, -6),
                                                                            new Vector3(2, -0.3f, -6)},
                                                                            5, pathMode: PathMode.TopDown2D, pathType: PathType.CatmullRom)
                .SetLookAt(0.1f, enemy.transform.forward, enemy.transform.right);
            };

            await UniTask.Delay(800);
        }

        await UniTask.Delay(5000);

        // Warning表示
        Warning warning = Instantiate(warningObject, new Vector3(0, 0, -7), Quaternion.identity);
        await warning.IsComplete;

        Debug.Log("warning complete");

        Destroy(warning);

        Boss boss = Instantiate(boss1, new Vector3(0, 1, -6), Quaternion.Euler(0, 0, 180));
        await boss.IsComplete;

        Debug.Log("boss complete");
    }
}
