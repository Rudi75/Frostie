using UnityEngine;
using System.Collections;

public class KeyInterpreter : MonoBehaviour {

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode jumpKey2 = KeyCode.UpArrow;
    public KeyCode meltKey = KeyCode.LeftAlt;
    public KeyCode pullKey = KeyCode.LeftControl;
    public KeyCode throwHeadKey = KeyCode.A;
    public KeyCode escapeKey = KeyCode.Tab;
    public KeyCode recallHeadKey = KeyCode.S;
    public KeyCode recallMiddleKey = KeyCode.X;
    public KeyCode ShootButtonKey = KeyCode.F;
    public KeyCode FreezeGroundKey = KeyCode.G;
    public KeyCode TakeWaterKey = KeyCode.V;
    public KeyCode SpawnIceCubeKey = KeyCode.C;
    public KeyCode decoupleMiddleKey = KeyCode.Y;
    public KeyCode basePartKey = KeyCode.Alpha1;
    public KeyCode middlePartKey = KeyCode.Alpha2;
    public KeyCode HeadKey = KeyCode.Alpha3;
    public KeyCode turnDrawBridgeWheelKey = KeyCode.E;
    public KeyCode drawBridgeRotateLeftKey = KeyCode.LeftArrow;
    public KeyCode drawBridgeRotateRightKey = KeyCode.RightArrow;

    public static bool meltingEnabedSave = false;
    public static bool throwingHeadEnabledSave = false;
    public static bool shootButtonEnabledSave = false;
    public static bool freezeGroundEnabledSave = false;
    public static bool takeWaterEnabledSave = false;
    public static bool decoupleMiddleEnabledSave = false;

    public bool meltingEnabed = false;
    public bool throwingHeadEnabled = false;
    public bool shootButtonEnabled = false;
    public bool freezeGroundEnabled = false;
    public bool takeWaterEnabled = false;
    public bool decoupleMiddleEnabled = false;

    public GameObject Player;

    private bool turnDrawBridgeWheelKeyPressed;
    private bool drawBridgeRotateLeftKeyPressed;
    private bool drawBridgeRotateRightKeyPressed;
	
    public void Start()
    {
        meltingEnabed |= KeyInterpreter.meltingEnabedSave;
        throwingHeadEnabled |= KeyInterpreter.throwingHeadEnabledSave;
        shootButtonEnabled |= KeyInterpreter.shootButtonEnabledSave;
        freezeGroundEnabled |= KeyInterpreter.freezeGroundEnabledSave;
        takeWaterEnabled |= KeyInterpreter.takeWaterEnabledSave;
        decoupleMiddleEnabled |= KeyInterpreter.decoupleMiddleEnabledSave;
    }

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

        if (Input.GetKeyDown(meltKey) && frostieStatus.canMelt() && meltingEnabed)
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
                //frostiePartManager.getActivePart().GetComponentInChildren<Animator>().SetBool("IsWalking", false);
                frostiePartManager.getActivePart().GetComponentInChildren<Animator>().SetTrigger("Jump");
                //Debug.Log("sent jump trigger");
            }
        }

        if (Input.GetKeyDown(throwHeadKey) && !frostieStatus.IsMelted && throwHeadScript != null && throwingHeadEnabled)
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

        if (Input.GetKeyDown(ShootButtonKey) && shootButtonEnabled)
        {
            buttonShotSkript.Shoot();
        }

        if (Input.GetKeyDown(FreezeGroundKey) && freezeGroundEnabled)
        {
            freezeGroundSkript.FreezeGround();
        }

        if (Input.GetKeyDown(TakeWaterKey) && takeWaterEnabled)
        {
            waterReserveSkript.TakeWater();
        }
        if (Input.GetKeyDown(SpawnIceCubeKey))
        {
            waterReserveSkript.SpawnIceCube();
        }

        if(Input.GetKeyDown(decoupleMiddleKey) && !frostieStatus.IsMelted && decoupleMiddleEnabled)
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

        if (Input.GetKeyDown(HeadKey))
        {
           // frostiePartManager.setActivePart(3);
        }

        if (frostieMoveScript != null)
        {
            frostieMoveScript.inputXAxis = Input.GetAxis("Horizontal");
        }
            

        if(Input.GetKeyDown(turnDrawBridgeWheelKey))
        {
            turnDrawBridgeWheelKeyPressed = true;
        }

        if (Input.GetKeyDown(drawBridgeRotateRightKey))
        {
            drawBridgeRotateRightKeyPressed = true;
        }

        if (Input.GetKeyDown(drawBridgeRotateLeftKey))
        {
            drawBridgeRotateLeftKeyPressed = true;
        }
	}

    public bool isTurnWheelKeyPressed()
    {
        if(turnDrawBridgeWheelKeyPressed)
        {
            turnDrawBridgeWheelKeyPressed = false;
            drawBridgeRotateLeftKeyPressed = false;
            drawBridgeRotateRightKeyPressed = false;
            return true;
        }
        return false;
    }

    public bool isDrawBridgeRotateLeftKeyPressed()
    {
        if (drawBridgeRotateLeftKeyPressed)
        {
            drawBridgeRotateLeftKeyPressed = false;
            return true;
        }
        return false;
    }

    public bool isDrawBridgeRotateRightKeyPressed()
    {
        if (drawBridgeRotateRightKeyPressed)
        {
            drawBridgeRotateRightKeyPressed = false;
            return true;
        }
        return false;
    }
}
