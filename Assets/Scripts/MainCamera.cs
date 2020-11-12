using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private float GAME_ASPECT = 9.0f / 16.0f;

	[SerializeField, Tooltip("uGUIのCanvas")]
	private UnityEngine.UI.CanvasScaler canvasScaler = null;

	private Camera camera = null;

	void Start()
    {
		camera = GetComponent<Camera>();

		Debug.Log("aaa");
		float screenAspect = (float)Screen.width / (float)Screen.height;
		float diff = GAME_ASPECT - screenAspect;
		if (diff > Vector3.kEpsilon)
		{
			// ゲーム画面より縦が長い
			float height = (float)Screen.width / GAME_ASPECT;
			float y = (Screen.height - height) * 0.5f;
			Rect pixelRect = new Rect(0f, y, Screen.width, height);
			camera.pixelRect = pixelRect;
			this.canvasScaler.matchWidthOrHeight = 0f;
		}
		else if (diff < -Vector3.kEpsilon)
		{
			// ゲーム画面より横が長い
			float width = (float)Screen.height * GAME_ASPECT;
			float x = (Screen.width - width) * 0.5f;
			Rect pixelRect = new Rect(x, 0f, width, Screen.height);
			camera.pixelRect = pixelRect;
			this.canvasScaler.matchWidthOrHeight = 1f;
		}
	}
}
