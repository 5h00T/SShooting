using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Player playerObj;
    private Player player;
    private int initialLeft = 3;
    private int initialBomb = 2;
    private int initialBombStack = 9;

    [SerializeField]
    private Stage1Manager stage1Manager;

    [SerializeField]
    private GameUIPresenter gameUIPresenter;

    void Start()
    {
        Application.targetFrameRate = 30;

        gameUIPresenter.Initialize();
        player = Instantiate(playerObj, new Vector3(0, -1, -6), Quaternion.identity);
        PlayerInitialize();
        PlayerAppearance();
        // PlayerLeftZeroMonitor();
    }

    void PlayerInitialize()
    {
        player.Initialize(initialLeft, initialBomb, initialBombStack);
    }

    /// <summary>
    /// プレイヤーを登場させる
    /// </summary>
    private void PlayerAppearance()
    {
        player.Appearance();
    }

    /// <summary>
    /// プレイヤーの残機を監視する
    /// </summary>
    /*
    async void PlayerLeftZeroMonitor()
    {
        await UniTask.WaitUntil(() => player.left.Value == 0);

        Debug.Log("GameOver");
    }
    */
}
