using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WindLionGodBaseMgr : MonoBehaviour
{

    public GameObject followImage;


    protected GameObject _avatar;

    public enum Status
    {
        Idle,
        Placing,
        Cancel,
        Drag,
        OpenPnl,
    }

    protected Status currentState;

    public void ChangeState(Status status)
    {
        switch (currentState)
        {
            case Status.Idle:
                //根據目前的狀態變換
                if (status == Status.OpenPnl)
                {
                   // _avatar.SetActive(false);  //預先關閉物件
                }
                break;
            
            case Status.OpenPnl:
                if (status == Status.Placing)  // 
                {
                    followImage.gameObject.SetActive(true); //重新開啟
                }
                break;
            
        }

        currentState = status; //更新目前狀態 (正式切換)
    }
}
