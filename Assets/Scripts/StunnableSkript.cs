using UnityEngine;
using System.Collections;

public class StunnableSkript : MonoBehaviour 
{
    public float StunningResistance = 0;

    private float StunnTime = 0;
    private bool isStunned = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

	// Use this for initialization
    public void Stunn(float StunnForce) 
    {
        StunnTime += Mathf.Max(StunnForce - Random.Range(0, StunningResistance), 0);
        if (!isStunned)
        {
            isStunned = true;
            if (animator)
            {
                animator.SetBool("IsStunned", true);
            }
            else
            {
                transform.Rotate(new Vector3(0f, 0f, 1f), 180, Space.Self);
            }
            foreach (var item in GetComponentsInChildren<MonoBehaviour>())
            {
                if (item == this) continue;
                if (item == animator) continue;
                item.enabled = false;
            }
            foreach (var item in GetComponents<MonoBehaviour>())
            {
                if (item == this) continue;
                if (item == animator) continue;
                item.enabled = false;
            }
        }
	}

    void Update()
    {
        if (StunnTime > 0)
        {
            StunnTime -= Time.deltaTime;
        }
        if (StunnTime <= 0 && isStunned)
        {
            isStunned = false;
            if (animator)
            {
                animator.SetBool("IsStunned", false);
            }
            else
            {
                transform.Rotate(new Vector3(0f, 0f, 1f), 180, Space.Self);
            }
            foreach (var item in GetComponentsInChildren<MonoBehaviour>())
            {
                item.enabled = true;
            }
            foreach (var item in GetComponents<MonoBehaviour>())
            {
                item.enabled = true;
            } 
        }
    }
}
