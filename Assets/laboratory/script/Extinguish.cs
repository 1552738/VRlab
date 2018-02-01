using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguish : MonoBehaviour
{

    public float extinguishDelay = 0;

    private float extinguishTime;

    // Use this for initialization
    void Start()
    {
        extinguishTime = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "FireSource")
        {
            extinguishTime = Time.time;
            Debug.Log("FireSource Enter");
        }
    }

    void OnTriggerStay(Collider collider)
    {
        Debug.Log(collider.gameObject.name);

        if (collider.gameObject.name != "FireSource")
            return;

        if ((extinguishTime != 0) && (Time.time > (extinguishTime + extinguishDelay)))
        {
            Transform tf = collider.gameObject.transform;
            for (int i = tf.childCount - 1; i >= 0; i--)
            {
                Destroy(tf.GetChild(i).gameObject);
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        extinguishTime = 0;
    }
}
