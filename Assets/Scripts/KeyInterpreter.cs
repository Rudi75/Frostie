using UnityEngine;
using System.Collections;

public class KeyInterpreter : MonoBehaviour {

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode jumpKey2 = KeyCode.UpArrow;
    public KeyCode meltKey = KeyCode.LeftAlt;
    public KeyCode pullKey = KeyCode.LeftControl;
    public KeyCode throwHeadKey = KeyCode.A;
    public KeyCode escapeKey = KeyCode.Escape;
    public KeyCode recallHeadKey = KeyCode.S;
    public KeyCode recallMiddleKey = KeyCode.X;
    public KeyCode ShootButtonKey = KeyCode.F;
    public KeyCode FreezeGroundKey = KeyCode.G;
    public KeyCode TakeWaterKey = KeyCode.V;
    public KeyCode SpawnIceCubeKey = KeyCode.C;
    public KeyCode decoupleMiddleKey = KeyCode.Y;
    public KeyCode basePartKey = KeyCode.Alpha1;
    public KeyCode middlePartKey = KeyCode.Alpha2;

    public GameObject Player;
	
	// Update is called once per frame
	void Update () {

        FrostieStatus frostieStatus = Player.GetComponentInChildren<FrostieStatus>();
        
        FrostiePartManager frostiePartManager = Player.GetComponentInChildren<FrostiePartManager>();
        ThrowHeadScript throwHeadScript = frostiePartManager.getActivePart().GetComponentInChildren<ThrowHeadScript>();
        FrostieMoveScript frostieMoveScript = frostiePartManager.getActivePart().GetComponent<FrostieMoveScript>();
        ButtonShotSkript buttonShotSkript = Player.GetComponentInChildren<ButtonShotSkript>();
        WaterReserveSkript waterReserveSkript = Player.GetComponentInChildren<WaterReserveSkript>();
        FreezeGroundSkript freezeGroundSkript = Player.GetComponentInChildren<FreezeGroundSkript>();
        DecoupleMiddleScript decoupleScript = Player.GetComponentInChildren<DecoupleMiddleScript>();

        if (Input.GetKeyDown(meltKey) && frostieStatus.canMelt())
        {
            frostieStatus.IsMelted = !frostieStatus.IsMelted;
        }

        if((Input.GetKeyDown(jumpKey) || Input.GetKeyDown(jumpKey2)) && frostieStatus.canJump())
        {
            frostieStatus.isFixated = true;
            FrostieAnimationManager frostieAnimationManager = frostiePartManager.getActivePart().GetComponent<FrostieAnimationManager>();
            if (frostieAnimationManager != null)
            {
                frostieAnimationManager.animateJump();
            }else
            {
                frostiePartManager.getActivePart().GetComponent<FrostieMoveScript>().Jump();
            }
        }

        if (Input.GetKeyDown(throwHeadKey) && !frostieStatus.IsMelted && throwHeadScript != null)
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

        if(Input.GetKeyDown(recallHeadKey))
        {
            frostiePartManager.recallHead();
        }

        if (Input.GetKeyDown(recallMiddleKey))
        {
            frostiePartManager.recallMiddlePart();
        }

        if (Input.GetKeyDown(ShootButtonKey))
        {
            buttonShotSkript.Shoot();
        }

        if (Input.GetKeyDown(FreezeGroundKey))
        {
            freezeGroundSkript.FreezeGround();
        }

        if (Input.GetKeyDown(TakeWaterKey))
        {
            waterReserveSkript.TakeWater();
        }
        if (Input.GetKeyDown(SpawnIceCubeKey))
        {
            waterReserveSkript.SpawnIceCube();
        }

        if(Input.GetKeyDown(decoupleMiddleKey) && !frostieStatus.IsMelted)
        {
            decoupleScript.decouple();
        }

        if(Input.GetKeyDown(basePartKey))
        {
            frostiePartManager.setActivePart(1);
        }

        if (Input.GetKeyDown(middlePartKey))
        {
            frostiePartManager.setActivePart(2);
        }

        frostieMoveScript.inputXAxis = Input.GetAxis("Horizontal");
	}
}
