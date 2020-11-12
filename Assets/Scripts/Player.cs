using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.VFX;
using System;
using Cysharp.Threading.Tasks;

public enum Status
{
	Appearance, // 登場
	Alive, // 生存
	Hit, // 被弾
	Event // その他
}

public class Player : GameAircraft
{
    [SerializeField]
    private Camera gameCamera;

	[SerializeField]
	private GameObject bullet;

	[SerializeField]
	private Bomb bomb;

	[SerializeField]
	private float speed = 1;

	public Status status;

	Vector3 mousePos;     // 最初にタッチ(左クリック)した地点の情報を入れる

	VisualEffect visualEffect;

	public IntReactiveProperty Score { get; set; } = new IntReactiveProperty(0);

	public IntReactiveProperty left; // 残機
	private int bombNum; // ボム数
	private int bombStackSize; // ボム枠数
	private bool duringBombEffect; // ボムの効果中か

	private bool bombTouch = false;

	private CircleCollider2D circleCollider2D;

	public void Initialize(int left, int bombNum, int bombStackSize)
    {
		visualEffect = GetComponent<VisualEffect>();
		circleCollider2D = GetComponent<CircleCollider2D>();

		this.left = new IntReactiveProperty(left);
		this.bombNum = bombNum;
		this.bombStackSize = bombStackSize;

		gameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		
		this.UpdateAsObservable()
			   .Where(_ => status == Status.Alive)
			   .Subscribe(_ => Move());

		this.UpdateAsObservable()
			.Where(_ => status == Status.Alive)
			.Where(_ => Input.GetMouseButton(0))
			.SampleFrame(4)
			.Subscribe(_ => Shot());

		this.UpdateAsObservable()
			.Where(_ => status == Status.Alive)
			.Subscribe(_ => BombCheck());

		this.OnTriggerEnter2DAsObservable()
			.Subscribe(gameObject => OnTriggerEnter(gameObject));
	}

	public async UniTask Appearance()
    {
		status = Status.Appearance;
		visualEffect.SendEvent("Appearance");
		visualEffect.SetBool("Alive", true);

		await UniTask.Delay(2000);

		status = Status.Alive;
	}

	private void BombCheck()
    {
		// Debug.Log(Input.touches[0].position);
		// Debug.Log(Input.mousePosition);

		if ((Input.touchCount == 2 || Input.GetKeyDown(KeyCode.X)) && !duringBombEffect )
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			mousePos = Input.touches[0].position;
#endif
			BombFire();
        }
    }

	private async UniTaskVoid BombFire()
    {
		duringBombEffect = true;
		Bomb b = Instantiate(bomb, transform.position, Quaternion.identity);
		// await UniTask.DelayFrame(1);
		await b.isComplete;

		Destroy(b.gameObject);

		duringBombEffect = false;
    }

    private void OnTriggerEnter(Collider2D collision)
    {
		Debug.Log("nnn");

		if (collision.gameObject.tag == "EnemyBullet")
		{
			Destroy(collision.gameObject);

			if (!duringBombEffect && status == Status.Alive)
			{
				Hit();
			}
		}
	}

    /// <summary>
    /// 被弾
    /// </summary>
    async UniTask Hit()
    {
		status = Status.Hit;
		left.Value--;
		circleCollider2D.enabled = false;
		visualEffect.SetBool("Alive", false);

		await UniTask.Delay(2000);

		await Appearance();
		await UniTask.Delay(1000); // 無敵時間

		circleCollider2D.enabled = true;
	}

    void Move()
    {
		Vector3 newPos = transform.position;

		/*
		 * スマホで2本指タッチした場合、Input.mousePositionは中点を返すので使えない
		 */
		// マウス左クリック(画面タッチ)が行われたら
		if (Input.GetMouseButtonDown(0))
		{
			// タッチした位置を代入
#if UNITY_ANDROID && !UNITY_EDITOR
			mousePos = Input.touches[0].position;
#else
			mousePos = Input.mousePosition;
#endif
		}
		if (Input.GetMouseButton(0))
		{

			Vector3 mouseDiff;
#if UNITY_ANDROID && !UNITY_EDITOR
			mouseDiff =  new Vector3(Input.touches[0].position.x, Input.touches[0].position.y, 0) - mousePos;
			mousePos = Input.touches[0].position;
#else
			mouseDiff = Input.mousePosition - mousePos;
			mousePos = Input.mousePosition;
#endif

			newPos = transform.position + new Vector3(mouseDiff.x, mouseDiff.y, 0) * speed;
		}

		newPos = Clamp(newPos);

		// 制限をかけた値をプレイヤーの位置とする
		transform.position = newPos;
	}

	private Vector3 Clamp(Vector3 pos)
    {
		// 画面左下のワールド座標をビューポートから取得
		Vector3 min = gameCamera.ViewportToWorldPoint(new Vector3(0.05f, 0.05f, 4)); // z=オブジェクトの座標-カメラ座標
		// 画面右上のワールド座標をビューポートから取得
		Vector3 max = gameCamera.ViewportToWorldPoint(new Vector3(0.95f, 0.95f, 4));

		// プレイヤーの位置をカメラ内に収める
		pos.x = Mathf.Clamp(pos.x, min.x, max.x);
		pos.y = Mathf.Clamp(pos.y, min.y, max.y);

		return pos;
	}

	private void Shot()
    {
		Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
