using UnityEngine;
using System.Collections;
using System;

public class DrawBridgePlanksScript : MonoBehaviour {

    public enum Directions
    {
        left = 1,
        right = -1
    };

    public Directions openingDirection;
    public float currentAngle = 90;
    public int planksCount;

	// Use this for initialization
    void Start()
    {
        Transform child = transform.GetChild(0);
        planksCount -= 1;

        for (int i = 0; i < planksCount; i++)
        {
            var newItem = Instantiate(child) as Transform;

            newItem.position = new Vector3(child.position.x, child.position.y + 2.25f, child.position.z);
            newItem.rotation = child.rotation;
            newItem.parent = transform;
            child = newItem;
        }

        transform.rotation = Quaternion.AngleAxis(currentAngle, new Vector3(0.0f, 0.0f, 1f));
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        transform.rotation = Quaternion.AngleAxis(currentAngle, new Vector3(0.0f, 0.0f, 1f));
	}

    public void rotateOnce(Directions direction)
    {
        float tempAngle = currentAngle + (Convert.ToInt32(direction) * 10f);

        if (openingDirection == Directions.left)
        {
            if (tempAngle <= 0)
                tempAngle = 0;

            if (tempAngle >= 90)
                tempAngle = 90;
        }
        else
        {
            if (tempAngle >= 0)
                tempAngle = 0;

            if (tempAngle <= -90)
                tempAngle = -90;
        }

        currentAngle = tempAngle;
    }
}
