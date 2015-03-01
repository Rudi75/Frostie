using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class FollowPath : TargetActionScript
{
	public enum FollowType
	{
		MoveTowards,
		Lerp
	}
	
	public FollowType Type = FollowType.MoveTowards;
	public PathDefinition Path;
	public float Speed = 1;
	public float MaxDistanceToGoal = .1f;
	
	private IEnumerator<Transform> currentPoint;
    public bool started = false;
	
	public void Start()
	{
        base.Start();
		if (Path == null)
		{
			Debug.LogError("Path cannot be null", gameObject);
			return;
		}
		
		currentPoint = Path.GetPathEnumerator();
		currentPoint.MoveNext();
		
		if (currentPoint.Current == null)
			return;
		
		transform.position = currentPoint.Current.position;
	}
	
	public void Update()
	{
        if (started)
        {
            if (currentPoint == null || currentPoint.Current == null)
                return;

            if (Type == FollowType.MoveTowards)
                transform.position = Vector3.MoveTowards(transform.position, currentPoint.Current.position, Time.deltaTime * Speed);
            else if (Type == FollowType.Lerp)
                transform.position = Vector3.Lerp(transform.position, currentPoint.Current.position, Time.deltaTime * Speed);

            var distanceSquared = (transform.position - currentPoint.Current.position).magnitude;
            if (distanceSquared < MaxDistanceToGoal)
                currentPoint.MoveNext();
        }
	}

    protected override void performAction()
    {
        started = true;
    }
}﻿