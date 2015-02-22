using UnityEngine;
using System.Collections;

public class DeathAnimation : MonoBehaviour 
{
    public float AnimationLength = 0;
    public bool DisableAllOtherSkripts = true;
    private bool deathInProgress = false;

    private IEnumerator DieProcess()
    {
        var animator = GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Death");
            yield return new WaitForSeconds(AnimationLength);  
        }
        Destroy(transform.gameObject);
    }

    private void DisableSkripts()
    {
        foreach (var item in GetComponentsInChildren<MonoBehaviour>())
        {
            if (item == this) continue;
            item.enabled = false;
        }
        foreach (var item in GetComponents<MonoBehaviour>())
        {
            if (item == this) continue;
            item.enabled = false;
        }
    }

    public void Kill()
    {
        if (!deathInProgress)
        {
            deathInProgress = true;
            if (DisableAllOtherSkripts)
            {
                DisableSkripts();
            }
            StartCoroutine(DieProcess());
        }
    }
}
