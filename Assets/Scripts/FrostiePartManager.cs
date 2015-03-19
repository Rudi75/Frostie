using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;
using KindsOfDeath = Assets.Scripts.Utils.Enums.KindsOfDeath;
using AssemblyCSharp;

public class FrostiePartManager : MonoBehaviour {

    public static Vector3 spawnPosition = new Vector3(0,0,0);

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
    public GameObject camera;


    public void Awake()
    {
        transform.position = spawnPosition;
    }

    void Start()
    {
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
        camera.GetComponent<CameraControler>().Player = activePart.transform;
    }

    public GameObject decoupleHead()
    {
        if (headClone == null)
        {

            if (headAndMiddleClone == null)
            {
                headClone = Instantiate(HeadClonePrefab, head.position, Quaternion.identity) as GameObject;

                head.gameObject.SetActive(false);

                Vector3 scale = headClone.transform.localScale;
                scale.x *= frotieParent.GetComponent<FrostieMoveScript>().viewDirection;
                headClone.transform.localScale = scale;

                headClone.transform.parent = head.parent.parent.parent;

                headClone.transform.parent = head.parent.parent.parent;
            }
            else
            {
                foreach (Transform child in headAndMiddleClone.transform)
                {
                    foreach (Transform childTransform in child)
                    {


                        if (childTransform.name.Contains("Head"))
                        {
                            headClone = Instantiate(HeadClonePrefab, childTransform.position, Quaternion.identity) as GameObject;

                            Vector3 scale = headClone.transform.localScale;
                            scale.x *= headAndMiddleClone.GetComponent<FrostieMoveScript>().viewDirection;
                            headClone.transform.localScale = scale;

                            headClone.transform.parent = head.parent.parent.parent;
                        }
                        else if (childTransform.name.Contains("Middle"))
                        {
                            middlePartClone = Instantiate(MiddleClonePrefab, childTransform.position, Quaternion.identity) as GameObject;

                            Vector3 scale = middlePartClone.transform.localScale;
                            scale.x *= headAndMiddleClone.GetComponent<FrostieMoveScript>().viewDirection;
                            middlePartClone.transform.localScale = scale;

                            middlePartClone.transform.parent = middlePart.parent.parent.parent;
                        }
                    }
                }
                if(activePart == headAndMiddleClone)
                {
                    activePart = middlePartClone;
                    activePart.GetComponent<FrostieMoveScript>().enabled = true;
                }
                Destroy(headAndMiddleClone);
            }
            activePart = headClone;
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

    public void recallMiddlePart()
    {
        if (middlePartClone != null)
        {
            Destroy(middlePartClone);
        }
        if (headAndMiddleClone != null)
        {
            Destroy(headAndMiddleClone);
            head.gameObject.SetActive(true);
        }

        middlePart.gameObject.SetActive(true);

        activePart = frotieParent.gameObject;
        activePart.GetComponent<FrostieMoveScript>().enabled = true;
    }



    public void recallHead()
    {
        if(middlePartClone != null)
        {
            headAndMiddleClone = Instantiate(HeadAndMiddleClonePrefab, middlePartClone.transform.position, Quaternion.identity) as GameObject;
            middlePart.gameObject.SetActive(false);
            head.gameObject.SetActive(false);

            Vector3 scale = headAndMiddleClone.transform.localScale;
            scale.x *= middlePartClone.GetComponent<FrostieMoveScript>().viewDirection;
            headAndMiddleClone.transform.localScale = scale;

            headAndMiddleClone.transform.parent = middlePart.parent.parent.parent;
            if (activePart.Equals(middlePartClone))
            {
                activePart = headAndMiddleClone;
                activePart.GetComponent<FrostieMoveScript>().enabled = true;
            }

            Destroy(middlePartClone);
            Destroy(headClone);
            setActivePart(2);
        }else if(headAndMiddleClone == null)
        {
            if (headClone != null)
            {
                Destroy(headClone);
            }
            head.gameObject.SetActive(true);
            setActivePart(1);
        }
    }



    public void pleaseJustKillYourself()
    {
        /*
        Destroy(head.gameObject);
        Destroy(middlePart.gameObject);
        Destroy(basePart.gameObject);
        Destroy(transform.parent.gameObject);*/
        Application.LoadLevel(Application.loadedLevel);
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
        FrostieMoveScript moveScript = activePart.GetComponent<FrostieMoveScript>();
        if(moveScript != null)
            moveScript.enabled = false;

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
        }else if(part == 3)
        {
            if(headClone != null)
            {
                activePart = headClone;
            }
        }
        moveScript = activePart.GetComponent<FrostieMoveScript>();
        if (moveScript != null)
            moveScript.enabled = true;
    }
    public void FixedUpdate()
    {
        if (camera != null)
            camera.GetComponent<CameraControler>().Player = activePart.transform;
    }
}
