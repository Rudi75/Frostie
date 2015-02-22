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
    private GameObject headAndMiddleClone;

    private GameObject activePart;

    public GameObject HeadClonePrefab;
    public GameObject MiddleClonePrefab;
    public GameObject HeadAndMiddleClonePrefab;


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
        frotieParent = head.parent.parent;
        activePart = frotieParent.gameObject;
    }

    public GameObject decoupleHead()
    {
        if (headClone == null)
        {

            if (headAndMiddleClone == null)
            {
                headClone = Instantiate(HeadClonePrefab, head.position, Quaternion.identity) as GameObject;

                head.gameObject.SetActive(false);

                headClone.transform.parent = head.parent.parent.parent;
            }
            else
            {
                foreach (Transform childTransform in headAndMiddleClone.transform)
                {
                    if(childTransform.name.Contains("Head"))
                    {
                        headClone = Instantiate(HeadClonePrefab, childTransform.position, Quaternion.identity) as GameObject;

                        Vector3 scale = headClone.transform.localScale;
                        scale.x *= headAndMiddleClone.GetComponent<FrostieMoveScript>().viewDirection;
                        headClone.transform.localScale = scale;
                        
                        headClone.transform.parent = head.parent.parent.parent;
                    }
                    else
                    {
                        middlePartClone = Instantiate(MiddleClonePrefab, childTransform.position, Quaternion.identity) as GameObject;

                        Vector3 scale = middlePartClone.transform.localScale;
                        scale.x *= headAndMiddleClone.GetComponent<FrostieMoveScript>().viewDirection;
                        middlePartClone.transform.localScale = scale;

                        middlePartClone.transform.parent = middlePart.parent.parent.parent;
                    }
                }
                if(activePart == headAndMiddleClone)
                {
                    activePart = middlePartClone;
                    activePart.GetComponent<FrostieMoveScript>().enabled = true;
                }
                Destroy(headAndMiddleClone);
            }

            return headClone;
        }
        return null;
    }

    public GameObject decoupleMiddlePart()
    {
        if (middlePartClone == null && headAndMiddleClone == null)
        {
            if (headClone != null)
            {
                middlePartClone = Instantiate(MiddleClonePrefab, middlePart.position, Quaternion.identity) as GameObject;
                middlePart.gameObject.SetActive(false);

                middlePartClone.transform.parent = middlePart.parent.parent.parent;
                return middlePartClone;
            }
            else
            {
                headAndMiddleClone = Instantiate(HeadAndMiddleClonePrefab, middlePart.position, Quaternion.identity) as GameObject;
                middlePart.gameObject.SetActive(false);
                head.gameObject.SetActive(false);

                headAndMiddleClone.transform.parent = middlePart.parent.parent.parent;
                return headAndMiddleClone;

            }
        }
        return null;
    }

    public void recallParts()
    {

        if(headClone != null)
        {
            Destroy(headClone);
        }

        if (middlePartClone != null)
        {
            Destroy(middlePartClone);
        }

        if (headAndMiddleClone != null)
        {
            Destroy(headAndMiddleClone);
        }

        head.gameObject.SetActive(true);
        middlePart.gameObject.SetActive(true);
        activePart = frotieParent.gameObject;
        activePart.GetComponent<FrostieMoveScript>().enabled = true;
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
        return headAndMiddleClone;
    }

    public GameObject getActivePart()
    {
        return activePart;
    }

    public void setActivePart(int part)
    {
        activePart.GetComponent<FrostieMoveScript>().enabled = false;
        if(part == 1)
        {
            activePart = frotieParent.gameObject;
            
        }else if(part == 2)
        {
            if(middlePartClone != null)
            {
                activePart = middlePartClone;
            }

            if(headAndMiddleClone != null)
            {
                activePart = headAndMiddleClone;
            }
        }
        activePart.GetComponent<FrostieMoveScript>().enabled = true;
    }
}
