using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;
using System.Collections.Generic;

public class EnableFeatureScript : MonoBehaviour {

    public KeyInterpreter interpreter;
    public List<Enums.Features> features;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("Player"))
        {
            if(features != null)
            {
                foreach (Enums.Features feature in features)
                {
                    switch (feature)
                    {
                        case Enums.Features.MELTING:
                           interpreter.meltingEnabed = true; break;
                        case Enums.Features.THROW_HEAD:
                            interpreter.throwingHeadEnabled = true; break;
                        case Enums.Features.SHOOT_BUTTON:
                            interpreter.shootButtonEnabled = true; break;
                        case Enums.Features.FREEZE_GROUND:
                            interpreter.freezeGroundEnabled = true; break;
                        case Enums.Features.TAKE_WATER:
                            interpreter.takeWaterEnabled = true; break;
                        case Enums.Features.DECOUPLE_MID:
                            interpreter.decoupleMiddleEnabled = true; break;
                        default:
                            break;
                    }
                    
                }
            }
        }
    }

}
