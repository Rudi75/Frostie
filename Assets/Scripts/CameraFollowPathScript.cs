using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using FollowType = Assets.Scripts.Utils.Enums.FollowType;

public class CameraFollowPathScript : MonoBehaviour 
{
    CameraControler controller;
    private Vector3 minPosition, maxPosition;

    private FollowType Type;
    private PathDefinition Path;
    private float Speed;
    private float MaxDistanceToGoal;

    private IEnumerator<Transform> currentPoint;
    private bool onWayBack = false;
    private bool started = false;

    public void Start()
    {
        controller = GetComponent<CameraControler>();
        minPosition = controller.Bounds.bounds.min;
        maxPosition = controller.Bounds.bounds.max;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (started)
        {
            if (currentPoint == null || currentPoint.Current == null)
                return;

            var x = camera.transform.position.x;
            var y = camera.transform.position.y;

            if (Type == FollowType.MoveTowards)
            {
                var pos = Vector2.MoveTowards(camera.transform.position, currentPoint.Current.position, Time.deltaTime * Speed);
                x = pos.x;
                y = pos.y;
            }
            else if (Type == FollowType.Lerp)
            {
                var pos = Vector2.Lerp(camera.transform.position, currentPoint.Current.position, Time.deltaTime * Speed);
                 x = pos.x;
                 y = pos.y;
            }

            Debug.Log("New");
            Debug.Log(x);
            Debug.Log(y);

		    var cameraHalfWidth = camera.orthographicSize * ((float)Screen.width / Screen.height);

		    var xt = Mathf.Clamp(x,minPosition.x + cameraHalfWidth, maxPosition.x - cameraHalfWidth);
		    var yt = Mathf.Clamp(y,minPosition.y + camera.orthographicSize, maxPosition.y - camera.orthographicSize);
		    camera.transform.position = new Vector3 (xt, yt, transform.position.z);

            Debug.Log(xt);
            Debug.Log(yt);

            var distanceSquared = Vector2.Distance(camera.transform.position, currentPoint.Current.position);
            if ((distanceSquared < MaxDistanceToGoal) || ((xt != x) && (yt != y)) || 
                ((xt != x) && ((y - currentPoint.Current.position.y) < MaxDistanceToGoal)) || 
                (((x - currentPoint.Current.position.x) < MaxDistanceToGoal)) && (yt != y))
            {
                if (onWayBack)
                {
                    if (currentPoint.Current == Path.GetFirstOrDefault())
                    {
                        removePathAndEnd();
                        return;
                    }
                }
                else
                {
                    if (currentPoint.Current == Path.GetLastOrDefault())
                        onWayBack = true;
                }
                currentPoint.MoveNext();
            }
        }
	}

    public void setPathAndStart(FollowType type, float speed, float maxDisToGoal, PathDefinition path)
    {
        if (!started)
        {
            Path = path;
            var camTrans = new GameObject();
            camTrans.transform.position = camera.transform.position;
            Path.Points.Insert(0, camTrans.transform);
            currentPoint = Path.GetPathEnumerator();
            currentPoint.MoveNext();

            if (currentPoint.Current == null)
                return;

            Type = type;
            MaxDistanceToGoal = maxDisToGoal;
            Speed = speed;
            onWayBack = false;
            started = true;

            controller.enabled = false;
            var status = controller.Player.GetComponent<FrostieStatus>();
            if (status != null)
            {
                status.isFixated = true;
            }
            
        }
    }

    protected void removePathAndEnd()
    {
        started = false;
        Path.Points.RemoveAt(0);
        Path = null;

        controller.enabled = true;
        var status = controller.Player.GetComponent<FrostieStatus>();
        if (status != null)
        {
            status.isFixated = false;
        }
    }
}
