using UnityEngine;
using System.Collections;


public class AnimationCallbackHelper : MonoBehaviour 
{
    public void Kill()
    {
        Debug.Log("Kill");
        HealthScript livingBeeing = GetComponentInChildren<HealthScript>();
        if (livingBeeing != null)
        {
            livingBeeing.Die();
        }
    }

    public void Jump()
    {
        FrostieScript frostie = GetComponentInChildren<FrostieScript>();
        if (frostie != null)
        {
            rigidbody2D.AddForce(new Vector2(0, frostie.jumpHight), ForceMode2D.Impulse);
        }
    }
}
