using UnityEngine;
using System.Collections;

public class CameraControler : MonoBehaviour 
{
	public Transform Player;
	public Vector2 Margin;
	public Vector2 Smoothing;

	public BoxCollider2D Bounds;

	private Vector3 minPosition, maxPosition;

	public bool IsFollowing = true;

	public void Start()
	{
		minPosition = Bounds.bounds.min;
		maxPosition = Bounds.bounds.max;
	}

	public void Update()
	{
		var x = camera.transform.position.x;
		var y = camera.transform.position.y;

    if (IsFollowing && Player != null)
		{
			if (Mathf.Abs (x - Player.position.x) > Margin.x)
				x = Mathf.Lerp (x, Player.position.x, Smoothing.x * Time.deltaTime);

			if (Mathf.Abs (y - Player.position.y) > Margin.y)
				y = Mathf.Lerp (y, Player.position.y, Smoothing.y * Time.deltaTime);
		}

		var cameraHalfWidth = camera.orthographicSize * ((float)Screen.width / Screen.height);

		x = Mathf.Clamp(x,minPosition.x +cameraHalfWidth, maxPosition.x - cameraHalfWidth);
		y = Mathf.Clamp(y,minPosition.y +camera.orthographicSize, maxPosition.y - camera.orthographicSize);

		camera.transform.position = new Vector3 (x, y, transform.position.z);
	}



}
