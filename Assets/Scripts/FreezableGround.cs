using UnityEngine;
using System.Collections;

public class FreezableGround : MonoBehaviour 
{
    public Transform FrozenOverlayPrefab;
    public float TimeToDefrost = 4;

    private Animator FrozenOverlayAnimator;
    private bool isFrozen = false;
    private float deltaTime = 0;

    public bool IsFrozen { get { return isFrozen; } }

	// Use this for initialization
	void Start () 
    {
        FrozenOverlayPrefab = Instantiate(FrozenOverlayPrefab, transform.position, transform.rotation) as Transform;
        FrozenOverlayPrefab.parent = transform;
        FrozenOverlayAnimator = FrozenOverlayPrefab.GetComponentInChildren<Animator>();
        FrozenOverlayAnimator.SetBool("IsFrozen", false);
	}

    public void Freeze()
    {
        if (!isFrozen)
        {
            isFrozen = true;
            deltaTime = TimeToDefrost;
            FrozenOverlayAnimator.SetBool("IsFrozen", true);
        }
    }

    private void UnFreeze()
    {
        isFrozen = false;
        FrozenOverlayAnimator.SetBool("IsFrozen", false);
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
            UnFreeze();
        }
	}
}
