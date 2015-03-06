using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using FollowType = Assets.Scripts.Utils.Enums.FollowType;

public class CameraActivateFollowPathScript : TargetActionScript 
{
    CameraFollowPathScript controller;

    public FollowType Type = FollowType.MoveTowards;
    public PathDefinition Path;
    public float Speed = 1;
    public float Presicion = 0.5f;

    public bool consumed = false;

    public void Start()
    {
        base.Start();
        var camera = FindObjectOfType<Camera>();
        controller = camera.gameObject.GetComponent<CameraFollowPathScript>();
        Debug.Log(controller);
	}

    protected override void performAction()
    {
        if (!consumed)
        {
            consumed = true;
            controller.setPathAndStart(Type, Speed, Presicion, Path); 
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("Player") && !consumed)
        {
            consumed = true;
            controller.setPathAndStart(Type, Speed, Presicion, Path);
        }
    }
}
