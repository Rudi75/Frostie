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
            frostie.Jump();
        }
    }
}
