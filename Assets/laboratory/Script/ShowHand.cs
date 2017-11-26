using UnityEngine;
using System.Collections;

public class ShowHand : MonoBehaviour
{
    //public bool isShowGun = true;      //设置枪初始显示状态
    public GameObject Hand;         //公开一个枪的模块用于拖拽
    public GameObject mod;          //公开一个手柄模型
    SteamVR_TrackedController steamVR;  //定义一个 SteamVR_TrackedController 对象
    //void showGun()  //显示枪的BOOL函数
    //{
    //    Gun.SetActive(true);//枪模型是真
    //    mod.SetActive(false);//手柄模型是假
    //}
    //void shouMod()//显示手柄的bool函数
    //{
    //    Gun.SetActive(false);//枪模型是假的
    //    mod.SetActive(true);//手柄模型是真的
    //}

    void Start()
    {
        Hand.SetActive(true);
        mod.SetActive(false);
        //Gun.SetActive(false);//设置枪的隐藏状态
        steamVR = GetComponent<SteamVR_TrackedController>();//给对象一个组件
        //steamVR.MenuButtonClicked += OnMenuButtonClicked;//OnMenuButtonClicked将这个函数附加给控制组件
        //这个键代表圆盘上面的键
        //if (isShowGun)//如果要显示枪
        //{
        //    showGun();//调用函数
        //}
        //else//如果不显示枪
        //{
        //    shouMod();//调用函数
        //}
    }

    void Update()
    {

    }

    //void OnMenuButtonClicked(object sender, ClickedEventArgs e)//定义一个函数附加给控制组件
    //{
    //    if (isShowGun == false)//当状态为假的时候
    //    {
    //        shouMod();//调用函数
    //        isShowGun = true;//将状态定义成真
    //    }
    //    else if (isShowGun == true)//当状态为真时
    //    {
    //        showGun();//调用函数
    //        isShowGun = false;//将状态定义为假
    //    }
    //}
}
