using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniteMatch : Ignition {

    public bool canBeIgnited = false;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "match_box")
        {
            canBeIgnited = true;
        }

        if (isBurning && canSpreadFromThisSource)
        {
            collider.SendMessageUpwards("FireExposure", SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        canBeIgnited = false;
    }
}
