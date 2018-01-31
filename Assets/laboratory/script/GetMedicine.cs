using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMedicine : MonoBehaviour {

    private GameObject currentObject;

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("111");
        if (collider.name != "spoon")
            return;

        
    }
}
