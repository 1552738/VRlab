using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMedicine : MonoBehaviour {

    public GameObject medicinePrefab;
    public float scoopDelay;

    private float scoopTime;

    // Use this for initialization
    void Start()
    {
        scoopTime = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "medicine")
        {
            scoopTime = Time.time;
            Debug.Log("medicine");
        }

    }

    void OnTriggerExit(Collider collider)
    {
        if ((scoopTime != 0) && (Time.time > (scoopTime + scoopDelay)))
        {
            GameObject spoonful = Instantiate(medicinePrefab);
            spoonful.transform.parent = transform;
            spoonful.transform.position = transform.position;
        }

        scoopTime = 0;
    }
}
