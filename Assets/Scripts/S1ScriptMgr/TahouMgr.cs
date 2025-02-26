using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TahouMgr : MonoBehaviour
{
   public GameObject tahouprefabs;
    public enum TahouState
    {
        Idle,
        丟砲塔,
        Cancel,
        拖砲塔,
    }
    private TahouState currentState;
    public Button btnTahou;
    public GameObject followTahouImage;
   
    private GameObject cache砲塔;
    
    void Start()
    {
        currentState = TahouState.Idle;
        btnTahou.onClick.AddListener(OnBtnTahouClick);
    }

    private void OnBtnTahouClick()
    {
        followTahouImage.gameObject.SetActive(true); 
        currentState = TahouState.丟砲塔;
        btnTahou.interactable = false;
        Debug.Log("開始丟塔后風獅爺喔");
    }


    void Update()
    {
        switch (currentState)
        {
            case TahouState.Idle:
                ProcessIdle();
                break;
            case TahouState.丟砲塔:
                ProcessPlacingTower();
                if (Input.GetMouseButtonDown(1))  // 按下右鍵
                {
                    currentState = TahouState.Cancel;  // 切換到取消狀態
                }
                break;
            case TahouState.Cancel:
                ProcessCancel();
                break;
            case TahouState.拖砲塔:
                ProcessDargTower();
                break;
        }
        if (currentState == TahouState.丟砲塔)
        {
            followTahouImage.transform.position = Input.mousePosition;
        }
    }

    private void ProcessIdle()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name.ToLower().Contains("tahou"))
                {
                    // cache 
                    cache砲塔 = hit.transform.gameObject;
                    //該狀態
                    currentState = TahouState.拖砲塔;
                }
            }
        }
    }

    private void ProcessPlacingTower()
    {
        if (Input.GetButtonDown("Fire1"))
        {   
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<RoadTag>() != null)
                {
                    Vector3 placePosition = hit.point;
                    GameObject temp = Instantiate(tahouprefabs);
                    temp.transform.localScale = Vector3.one;
                    temp.transform.localEulerAngles = new Vector3(30, 0, 0);
                    temp.transform.localPosition = hit.point;
                    followTahouImage.gameObject.SetActive(false);
                    currentState = TahouState.Idle; //改變狀態!!!
                }
            }
        }
    }

    private void ProcessCancel()
    {
        // 隱藏跟隨圖片
        followTahouImage.gameObject.SetActive(false);
    
        // 重新啟用按鈕
        btnTahou.interactable = true;
    
        // 重置狀態
        currentState = TahouState.Idle;
    
        Debug.Log("取消放置");
    }

    private void ProcessDargTower()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.GetComponent<RoadTag>() != null)
            {
                cache砲塔.transform.localPosition = hit.point;
            }
            return;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            cache砲塔 = null;
            currentState = TahouState.Idle;
        }
    }
}
