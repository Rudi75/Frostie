using UnityEngine;
using System.Collections;
using System;

public class SqeezerAnimationScript : MonoBehaviour
{


    private Transform top;
    private Transform bottom;
    private Transform arm;
    private Transform squeezer;
    private Vector3 startPosition;
    private float deltaTime = 0;
    private float distance;

    public float direction = -1;
    public float distanceToMovePerFrame = 1;

    public float animationTimeDown = 1;
    public float animationTimeUp = 1;
    public float animationTimePause = 1;

	// Use this for initialization
	void Awake () 
    {
        foreach (Transform child in transform)
        {
            if(child.name.Contains("Top"))
                top = child;
            else if(child.name.Contains("Base"))
                bottom = child;
            else if(child.name.Contains("Arm"))
                arm = child;
            else
                squeezer = child; 
        }
        startPosition = squeezer.position;
        distance = Math.Abs(squeezer.position.y - bottom.position.y);
        direction = -1;
        distanceToMovePerFrame = distance / animationTimeDown;
        deltaTime = animationTimeDown;
	}

	
	// Update is called once per frame
	void FixedUpdate () 
    {
        deltaTime -= Time.deltaTime;

        if (deltaTime <= 0 && direction == 0)
        {
            if (squeezer.position.y <= bottom.position.y)
            {
                direction = 1;
                distanceToMovePerFrame = distance / animationTimeUp;
                deltaTime = animationTimeUp;
            } 
            else
            {
                direction = -1;
                distanceToMovePerFrame = distance / animationTimeDown;
                deltaTime = animationTimeDown;
            }
        }
        
        if (squeezer.position.y >= startPosition.y && direction > 0)
        {
            direction = 0;
            deltaTime = animationTimePause;
            arm.localScale = new Vector3(1,1,1);
        }

        if (squeezer.position.y <= bottom.position.y && direction < 0)
        {
            direction = 0;
            deltaTime = animationTimePause;
        }
 
        squeezer.Translate(new Vector3(0, direction * distanceToMovePerFrame * Time.deltaTime, 0), Space.World);
        squeezer.position = new Vector3(squeezer.position.x, 
                                        Mathf.Clamp(squeezer.position.y, bottom.position.y, startPosition.y),
                                        squeezer.position.z);
        Vector3 armScale = arm.localScale;
        armScale.y = armScale.y - (2 * direction * distanceToMovePerFrame * Time.deltaTime);
        arm.localScale = armScale;
	}
}
