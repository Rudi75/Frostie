using UnityEngine;
using System.Collections;

public class KeyInterpreter : MonoBehaviour {

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode jumpKey2 = KeyCode.UpArrow;
    public KeyCode meltKey = KeyCode.LeftAlt;
    public KeyCode pullKey = KeyCode.LeftControl;
    public KeyCode throwHeadKey = KeyCode.LeftShift;
    public KeyCode escapeKey = KeyCode.Escape;
    public KeyCode recallPartsKey = KeyCode.Y;
    public KeyCode ShootButtonKey = KeyCode.F;
    public KeyCode FreezeGroundKey = KeyCode.G;
    public KeyCode TakeWaterKey = KeyCode.V;
    public KeyCode SpawnIceCubeKey = KeyCode.C;
    public KeyCode decoupleMiddleKey = KeyCode.X;

    public GameObject Player;
	
	// Update is called once per frame
	void Update () {

        FrostieStatus frostieStatus = Player.GetComponentInChildren<FrostieStatus>();
        FrostieMoveScript frostieMoveScript = Player.GetComponentInChildren<FrostieMoveScript>();
        FrostieAnimationManager frostieAnimationManager = Player.GetComponentInChildren<FrostieAnimationManager>();
        ThrowHeadScript throwHeadScript = Player.GetComponentInChildren<ThrowHeadScript>();
        FrostiePartManager frostiePartManager = Player.GetComponentInChildren<FrostiePartManager>();
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

        if(Input.GetKeyDown(decoupleMiddleKey))
        {
            decoupleScript.decouple();
        }
	}
}
