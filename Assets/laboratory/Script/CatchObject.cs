using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchObject : MonoBehaviour
{

    private SteamVR_TrackedController SteamVR_TrackedController;
    private Transform pointTransform;
    private GameObject currentCatch;

    public GameObject matchPrefab;

    // Use this for initialization
    void Start()
    {
        //获取SteamVR_TrackedController并监听属性
        SteamVR_TrackedController = GetComponent<SteamVR_TrackedController>();
        SteamVR_TrackedController.TriggerClicked += TriggerClicked;
        SteamVR_TrackedController.TriggerUnclicked += TriggerUnclicked;
        SteamVR_TrackedController.Gripped += Gripped;
    }

    void OnTriggerStay(Collider collider)
    {
        if (!collider.CompareTag("Catch"))
            return;

        pointTransform = collider.transform;
    }

    void OnTriggerExit(Collider collider)
    {
        pointTransform = null;
    }

    void TriggerClicked(object sender, ClickedEventArgs e)
    {
        if (pointTransform == null)
            return;

        if (pointTransform.name == "match")
        {
            Debug.Log("111");
            if (pointTransform.CompareTag("Catched"))
            {
                GameObject oneMatch = Instantiate(matchPrefab);
                oneMatch.transform.position = this.transform.position;
                oneMatch.tag = "Catched";
                oneMatch.name = "oneMatch";
                oneMatch.AddComponent<FixedJoint>().connectedBody = this.GetComponent<Rigidbody>();
                currentCatch = oneMatch;
                return;
            }
        }

        //修改指向物体位置、Tag，并将其绑在手柄上
        pointTransform.position = this.transform.position;
        pointTransform.gameObject.tag = "Catched";
        pointTransform.gameObject.AddComponent<FixedJoint>().connectedBody = this.GetComponent<Rigidbody>();
        currentCatch = pointTransform.gameObject;
    }

    void TriggerUnclicked(object sender, ClickedEventArgs e)
    {
        if (currentCatch == null)
            return;
        
        var device = SteamVR_Controller.Input((int)this.GetComponent<SteamVR_TrackedObject>().index);
        //使用device.TriggerHapticPulse(2500)触发手柄震动，参数为震动强度
        device.TriggerHapticPulse(2500);
        //松开时将速度传递给物体，实现投掷效果
        currentCatch.tag = "Catch";
        currentCatch.GetComponent<Rigidbody>().velocity = device.velocity * 5;
        currentCatch.GetComponent<Rigidbody>().angularVelocity = device.angularVelocity;
        Destroy(currentCatch.GetComponent<FixedJoint>());
        currentCatch = null;
    }

    void Gripped(object sender, ClickedEventArgs e)
    {
        if (currentCatch.name != "oneMatch")
            return;

        IgniteMatch igniteMatch = currentCatch.GetComponent<IgniteMatch>();
        if (igniteMatch.canBeIgnited)
        {
            currentCatch.SendMessage("FireExposure");
        }
    }
}
