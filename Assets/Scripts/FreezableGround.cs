using UnityEngine;
using System.Collections;

public class FreezableGround : MonoBehaviour 
{
    public Transform FrozenOverlayPrefab;
    public float TimeToDefrost = 12;

    private Animator FrozenOverlayAnimator;
    private Transform FrozenOverlay;
    private bool isFrozen = false;
    private float deltaTime = 0;

    public bool IsFrozen { get { return isFrozen; } }

	// Use this for initialization
	void Start () 
    {
	}

    public void Freeze()
    {
        if (!isFrozen)
        {
            isFrozen = true;
            deltaTime = TimeToDefrost;

            FrozenOverlay = Instantiate(FrozenOverlayPrefab) as Transform;
            FrozenOverlay.SetParent(transform, false);
            FrozenOverlayAnimator = FrozenOverlay.GetComponentInChildren<Animator>();
        }
    }


    private IEnumerator UnFreeze()
    {
        isFrozen = false;
        FrozenOverlayAnimator.SetTrigger("IsFrozen");
        var info = FrozenOverlayAnimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(info.length);
        Destroy(FrozenOverlay.gameObject);
        FrozenOverlayAnimator = null;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (deltaTime > 0)
        {
            deltaTime -= Time.deltaTime;
        }
        if (isFrozen && deltaTime < 0)
        {
            StartCoroutine(UnFreeze());
        }
	}
}
