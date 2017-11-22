using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchObject : MonoBehaviour
{

    SteamVR_TrackedController SteamVR_TrackedController;
    Transform pointTransform;
    GameObject currentCatch;


    // Use this for initialization
    void Start()
    {
        //获取SteamVR_TrackedController并监听属性
        SteamVR_TrackedController = GetComponent<SteamVR_TrackedController>();
        SteamVR_TrackedController.TriggerClicked += TriggerClicked;
        SteamVR_TrackedController.TriggerUnclicked += TriggerUnclicked;
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.transform.gameObject.tag == "Catch")
        {
            pointTransform = collider.transform;
            Debug.Log("标记啦2");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        pointTransform = null;
    }

    void TriggerClicked(object sender, ClickedEventArgs e)
    {
        if (pointTransform == null)
        {
            return;
        }
        //修改指向物体位置，并将其绑在手柄上
        pointTransform.position = this.transform.position;
        pointTransform.gameObject.AddComponent<FixedJoint>().connectedBody = this.GetComponent<Rigidbody>();
        currentCatch = pointTransform.gameObject;
        //Debug.Log(currentCatch.name);
    }

    void TriggerUnclicked(object sender, ClickedEventArgs e)
    {
        if (currentCatch == null)
        {
            return;
        }
        var device = SteamVR_Controller.Input((int)this.GetComponent<SteamVR_TrackedObject>().index);
        //使用device.TriggerHapticPulse(2500)触发手柄震动，参数为震动强度
        device.TriggerHapticPulse(2500);
        //松开时将速度传递给物体，实现投掷效果
        currentCatch.GetComponent<Rigidbody>().velocity = device.velocity * 5;
        currentCatch.GetComponent<Rigidbody>().angularVelocity = device.angularVelocity;
        Destroy(currentCatch.GetComponent<FixedJoint>());
        currentCatch = null;
    }
}
