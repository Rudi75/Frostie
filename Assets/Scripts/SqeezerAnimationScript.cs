using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.Utils;

public class SqeezerAnimationScript : MonoBehaviour
{


    private Transform top;
    private Transform bottom;
    private Transform arm;
    private Transform squeezer;
    private Vector3 startPosition;
    private float deltaTime = 0;
    private float distance;

    public bool isSqeezing = true;

    public float direction = -1;
    public float distanceToMovePerFrame = 1;

    public float animationTimeDown = 1;
    public float animationTimeUp = 1;
    public float animationTimePause = 1;

    private Enums.Edges sqeezerTop;
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

        float rotation = transform.localEulerAngles.z;

        if (rotation >= 225 && rotation < 315)
        {
            sqeezerTop = Enums.Edges.RIGHT;
            distance = Math.Abs(squeezer.position.x - bottom.position.x);
            direction = -1;
        }
        else if (rotation >= 45 && rotation < 135)
        {
            sqeezerTop = Enums.Edges.LEFT;
            distance = Math.Abs(squeezer.position.x - bottom.position.x);
            direction = 1;
        }
        else if (rotation >= 135 && rotation < 225)
        {
            sqeezerTop = Enums.Edges.BOTTOM;
            distance = Math.Abs(squeezer.position.y - bottom.position.y);
            direction = 1;
        }
        else
        {
            sqeezerTop = Enums.Edges.TOP;
            direction = -1;
            distance = Math.Abs(squeezer.position.y - bottom.position.y);
        }

        startPosition = squeezer.position;
        
        
        distanceToMovePerFrame = distance / animationTimeDown;
        deltaTime = animationTimeDown;
	}

	
	// Update is called once per frame
	void FixedUpdate () 
    {
        deltaTime -= Time.deltaTime;
        switch (sqeezerTop)
        {
            case Enums.Edges.LEFT:
                {
                    animateLeftToRight();
                    break;
                }
            case Enums.Edges.RIGHT:
                {
                    animateRightToLeft();
                    break;
                }
            case Enums.Edges.TOP:
                {
                    animateTopDown();
                    break;
                }
            case Enums.Edges.BOTTOM:
                {
                    animateBottomUp();
                    break;
                }
        }
	}

    void animateTopDown()
    {
        if (deltaTime <= 0 && direction == 0)
        {
            if (squeezer.position.y <= bottom.position.y)
            {
                direction = 1;
                deltaTime = animationTimeUp;
                isSqeezing = false;
            }
            else
            {
                direction = -1;
                deltaTime = animationTimeDown;
                isSqeezing = true;
            }
        }

        if (squeezer.position.y >= startPosition.y && direction > 0)
        {
            direction = 0;
            deltaTime = animationTimePause;
            arm.localScale = new Vector3(1, 1, 1);
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


    void animateRightToLeft()
    {
        if (deltaTime <= 0 && direction == 0)
        {
            if (squeezer.position.x <= bottom.position.x)
            {
                direction = 1;
                deltaTime = animationTimeUp;
                isSqeezing = false;
            }
            else
            {
                direction = -1;
                deltaTime = animationTimeDown;
                isSqeezing = true;
            }
        }

        if (squeezer.position.x >= startPosition.x && direction > 0)
        {
            direction = 0;
            deltaTime = animationTimePause;
            arm.localScale = new Vector3(1, 1, 1);
        }

        if (squeezer.position.x <= bottom.position.x && direction < 0)
        {
            direction = 0;
            deltaTime = animationTimePause;
        }

        squeezer.Translate(new Vector3(direction * distanceToMovePerFrame * Time.deltaTime, 0, 0), Space.World);
        squeezer.position = new Vector3( Mathf.Clamp(squeezer.position.x, bottom.position.x, startPosition.x),
                                       squeezer.position.y,
                                        squeezer.position.z);
        Vector3 armScale = arm.localScale;
        armScale.y = armScale.y - (2 * direction * distanceToMovePerFrame * Time.deltaTime);
        arm.localScale = armScale;
    }

    void animateBottomUp()
    {
        if (deltaTime <= 0 && direction == 0)
        {
            if (squeezer.position.y >= bottom.position.y)
            {
                direction = -1;
                deltaTime = animationTimeUp;
                isSqeezing = false;
            }
            else
            {
                direction = 1;
                deltaTime = animationTimeDown;
                isSqeezing = true;
            }
        }

        if (squeezer.position.y <= startPosition.y && direction < 0)
        {
            direction = 0;
            deltaTime = animationTimePause;
            arm.localScale = new Vector3(1, 1, 1);
        }

        if (squeezer.position.y >= bottom.position.y && direction > 0)
        {
            direction = 0;
            deltaTime = animationTimePause;
        }

        squeezer.Translate(new Vector3(0, direction * distanceToMovePerFrame * Time.deltaTime, 0), Space.World);
        squeezer.position = new Vector3(squeezer.position.x,
                                        Mathf.Clamp(squeezer.position.y, startPosition.y, bottom.position.y),
                                        squeezer.position.z);
        Vector3 armScale = arm.localScale;
        armScale.y = armScale.y - (2 * -direction * distanceToMovePerFrame * Time.deltaTime);
        arm.localScale = armScale;
    }

    void animateLeftToRight()
    {
        if (deltaTime <= 0 && direction == 0)
        {
            if (squeezer.position.x >= bottom.position.x)
            {
                direction = -1;
                deltaTime = animationTimeUp;
                isSqeezing = false;
            }
            else
            {
                direction = 1;
                deltaTime = animationTimeDown;
                isSqeezing = true;
            }
        }

        if (squeezer.position.x <= startPosition.x && direction < 0)
        {
            direction = 0;
            deltaTime = animationTimePause;
            arm.localScale = new Vector3(1, 1, 1);
        }

        if (squeezer.position.x >= bottom.position.x && direction > 0)
        {
            direction = 0;
            deltaTime = animationTimePause;
        }

        squeezer.Translate(new Vector3(direction * distanceToMovePerFrame * Time.deltaTime, 0, 0), Space.World);
        squeezer.position = new Vector3(
            Mathf.Clamp(squeezer.position.x, startPosition.x, bottom.position.x)
                                        ,squeezer.position.y,
                                        squeezer.position.z);
        Vector3 armScale = arm.localScale;
        armScale.y = armScale.y - (2 * -direction * distanceToMovePerFrame * Time.deltaTime);
        arm.localScale = armScale;
    }
}
