using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class SavePointScript : SaveableScript 
{
    private bool activated = false;
    private Transform fire;

    public void Awake()
    {
        base.Awake();
        foreach (Transform childTransform in transform)
        {
            if(childTransform.name.Contains("Fire"))
            {
                fire = childTransform;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("Player") && !activated)
        {
            activated = true;
            fire.gameObject.SetActive(true);

            var savedData = FindObjectOfType<SavedDataManager>();
           // if (savedData != null) 
            savedData.SaveCurrentState();

            FrostiePartManager.spawnPosition = transform.position;

            KeyInterpreter interpreter = other.GetComponentInParent<KeyInterpreter>();

            KeyInterpreter.decoupleMiddleEnabledSave = interpreter.decoupleMiddleEnabled;
            KeyInterpreter.meltingEnabedSave = interpreter.meltingEnabed;
            KeyInterpreter.throwingHeadEnabledSave = interpreter.throwingHeadEnabled;
            KeyInterpreter.shootButtonEnabledSave = interpreter.shootButtonEnabled;
            KeyInterpreter.freezeGroundEnabledSave = interpreter.freezeGroundEnabled;
            KeyInterpreter.takeWaterEnabledSave = interpreter.takeWaterEnabled;

        }
    }

    public override void saveData(SavedDataContainer dataContainer)
    {
        dataContainer.AddData("active", activated);
    }

    public override void loadData(SavedDataContainer dataContainer)
    {
        activated = (bool)dataContainer.retrieveData("active");
        fire.gameObject.SetActive(activated);
    }
}
