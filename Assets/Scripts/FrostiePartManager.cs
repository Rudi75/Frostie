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
    private GameObject headClone;
    private GameObject middlePartClone;
    private GameObject HeadAndMiddleClone;

    public GameObject HeadClonePrefab;


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
    }

    public GameObject decoupleHead()
    {
        if (headClone == null)
        {
            headClone = Instantiate(HeadClonePrefab, head.position, Quaternion.identity) as GameObject;

            head.gameObject.SetActive(false);

            headClone.transform.parent = head.parent.parent.parent;

            return headClone;
        }
        return null;
    }

    public void recallParts()
    {

        if(headClone != null)
        {
            Destroy(headClone);
            head.gameObject.SetActive(true);
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

    public GameObject getHeadClone()
    {
        return headClone;
    }

    public GameObject getMiddlePartClone()
    {
        return middlePartClone;
    }

    public GameObject getHeadAndMiddleClone()
    {
        return HeadAndMiddleClone;
    }
}
