﻿using UnityEngine;
using System.Collections;

public class KeyInterpreter : MonoBehaviour {

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode jumpKey2 = KeyCode.UpArrow;
    public KeyCode meltKey = KeyCode.LeftAlt;
    public KeyCode pullKey = KeyCode.LeftControl;
    public KeyCode throwHeadKey = KeyCode.LeftShift;
    public KeyCode escapeKey = KeyCode.Escape;
    public KeyCode recallPartsKey = KeyCode.Y;

    public GameObject Player;

    private FrostieStatus frostieStatus;
    private FrostieMoveScript frostieMoveScript;
    private FrostieAnimationManager frostieAnimationManager;
    private ThrowHeadScript throwHeadScript;
    private FrostiePartManager frostiePartManager;

	// Use this for initialization
	void Start () {
        frostieStatus = Player.GetComponentInChildren<FrostieStatus>();
        frostieMoveScript = Player.GetComponentInChildren<FrostieMoveScript>();
        frostieAnimationManager = Player.GetComponentInChildren<FrostieAnimationManager>();
        throwHeadScript = Player.GetComponentInChildren<ThrowHeadScript>();
        frostiePartManager = Player.GetComponentInChildren<FrostiePartManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(meltKey) && !frostieStatus.isPartMising)
        {
            frostieStatus.IsMelted = !frostieStatus.IsMelted;
        }

        if((Input.GetKeyDown(jumpKey) || Input.GetKeyDown(jumpKey2)) && frostieStatus.canJump())
        {
            frostieStatus.isFixated = true;
            frostieAnimationManager.animateJump();
        }

        if(Input.GetKeyDown(throwHeadKey))
        {
            throwHeadScript.setForward();
        }

        if(Input.GetKeyDown(escapeKey))
        {
            throwHeadScript.setBackward();
        }

        if(Input.GetKey(pullKey))
        {
            frostieStatus.isPulling = true;
        }
        else
        {
            frostieStatus.isPulling = false;
        }

        if(Input.GetKeyDown(recallPartsKey))
        {
            frostiePartManager.recallParts();
        }
	
	}
}