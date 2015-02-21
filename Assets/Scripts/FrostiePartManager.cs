using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;
using KindsOfDeath = Assets.Scripts.Utils.Enums.KindsOfDeath;
using AssemblyCSharp;

public class FrostiePartManager : MonoBehaviour {
    private Transform head;
    private Transform middlePart;
    private Transform basePart;
    private Transform frotieParent;
    

    private Vector3 headLocalPosition;
    private Vector3 middleLocalPosition;
    private Vector3 baseLocalPosition;
    private Vector3 distToBase;

    private FrostieStatus frostieStatus;

    void Start()
    {
        frostieStatus = GetComponent<FrostieStatus>();
        foreach (Transform childTransform in transform)
        {
            foreach (Transform child in childTransform)
            {
                if (child.name.Contains("Head"))
                    head = child;
                else if (child.name.Contains("Middle"))
                    middlePart = child;
                else if (child.name.Contains("Base"))
                    basePart = child;
            }

        }
        middleLocalPosition = middlePart.localPosition;
        baseLocalPosition = basePart.localPosition;
        headLocalPosition = head.localPosition;
        frotieParent = head.parent;
        distToBase = basePart.position - transform.position;
        frostieStatus.isPartMising = false;
    }

    public void recallParts()
    {


        getRidOfDummies(head);
        head.localScale = new Vector3(1, 1, 1);

        getRidOfDummies(basePart);
        basePart.localScale = new Vector3(1, 1, 1);

        getRidOfDummies(middlePart);
        middlePart.localScale = new Vector3(1, 1, 1);


        ThrowHeadScript headAim = head.GetComponentInChildren<ThrowHeadScript>();
        if (headAim != null)
        {
            headAim.reset();
        }

        transform.position = basePart.position - distToBase;

        head.localPosition = headLocalPosition;
        middlePart.localPosition = middleLocalPosition;
        basePart.localPosition = baseLocalPosition;
        frostieStatus.isPartMising = false;
    }

    private void getRidOfDummies(Transform part)
    {
        if (part.parent == null)
            return;

        getRidOfDummies(part.parent);


        if (part.parent.name.Contains("Dummy"))
        {
            Transform dummy = part.parent;
            part.parent = frotieParent;
            Destroy(dummy.gameObject);
        }
    }



    public void pleaseJustKillYourself()
    {
        Destroy(head.gameObject);
        Destroy(middlePart.gameObject);
        Destroy(basePart.gameObject);
        Destroy(transform.parent.gameObject);
    }

    public Transform getBasePart()
    {
        return basePart;
    }

    public Transform getHead()
    {
        return head;
    }

    public Transform getMiddlePart()
    {
        return middlePart;
    }

    
}
