using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoushuiMgr : MonoBehaviour
{
    public GameObject houshuiprefabs;
    public enum HoushuiState
    {
        Idle,
        丟砲塔,
        Cancel,
        拖砲塔,
    }
    private HoushuiState currentState;
    public Button btnHoushui;
    public GameObject followHoushuiImage;
   
    private GameObject cache砲塔;
    
    void Start()
    {
        cache砲塔 = null;
        currentState = HoushuiState.Idle;
        btnHoushui.onClick.AddListener(OnBtnHoushuiClick);
    }

    private void OnBtnHoushuiClick()
    {
        followHoushuiImage.gameObject.SetActive(true); 
        currentState = HoushuiState.丟砲塔;
        btnHoushui.interactable = false;
        Debug.Log("開始丟安崎風獅爺喔");
    }


    void Update()
    {
        switch (currentState)
        {
            case HoushuiState.Idle:
                ProcessIdle();
                break;
            case HoushuiState.丟砲塔:
                ProcessPlacingTower();
                if (Input.GetMouseButtonDown(1))  // 按下右鍵
                {
                    currentState = HoushuiState.Cancel;  // 切換到取消狀態
                }
                break;
            case HoushuiState.Cancel:
                ProcessCancel();
                break;
            case HoushuiState.拖砲塔:
                ProcessDargTower();
                break;
        }
        if (currentState == HoushuiState.丟砲塔)
        {
            followHoushuiImage.transform.position = Input.mousePosition;
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
                if (hit.transform.gameObject.GetComponent<HoushuiTag>()!= null)
                {
                    // cache 
                    cache砲塔 = hit.transform.gameObject;
                    //該狀態
                    currentState = HoushuiState.拖砲塔;
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
                    GameObject temp = Instantiate(houshuiprefabs);
                    temp.transform.localScale = Vector3.one;
                    temp.transform.localEulerAngles = new Vector3(30, 0, 0);
                    temp.transform.localPosition = hit.point;
                    followHoushuiImage.gameObject.SetActive(false);
                    currentState = HoushuiState.Idle; //改變狀態!!!
                }
            }
        }
    }

    private void ProcessCancel()
    {
        // 隱藏跟隨圖片
        followHoushuiImage.gameObject.SetActive(false);
    
        // 重新啟用按鈕
        btnHoushui.interactable = true;
    
        // 重置狀態
        currentState = HoushuiState.Idle;
    
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
        }

        if (Input.GetButtonUp("Fire1"))
        {
            cache砲塔 = null;
            currentState = HoushuiState.Idle;
        }
        
    }
}

