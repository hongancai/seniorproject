using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnMgr : MonoBehaviour
{
    public GameObject anprefabs;
    public enum AnState
    {
        Idle,
        丟砲塔,
        Cancel,
        拖砲塔,
    }
    private AnState currentState;
    public Button btnAn;
    public GameObject followAnImage;
   
    private GameObject cache砲塔;
    
    void Start()
    {
        currentState = AnState.Idle;
        btnAn.onClick.AddListener(OnBtnAnClick);
    }

    private void OnBtnAnClick()
    {
        followAnImage.gameObject.SetActive(true); 
        currentState = AnState.丟砲塔;
        btnAn.interactable = false;
        Debug.Log("開始丟安崎風獅爺喔");
    }


    void Update()
    {
        switch (currentState)
        {
            case AnState.Idle:
                ProcessIdle();
                break;
            case AnState.丟砲塔:
                ProcessPlacingTower();
                if (Input.GetMouseButtonDown(1))  // 按下右鍵
                {
                    currentState = AnState.Cancel;  // 切換到取消狀態
                }
                break;
            case AnState.Cancel:
                ProcessCancel();
                break;
            case AnState.拖砲塔:
                ProcessDargTower();
                break;
        }
        if (currentState == AnState.丟砲塔)
        {
            followAnImage.transform.position = Input.mousePosition;
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
                if (hit.transform.gameObject.name.ToLower().Contains("an"))
                {
                    // cache 
                    cache砲塔 = hit.transform.gameObject;
                    //該狀態
                    currentState = AnState.拖砲塔;
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
                    GameObject temp = Instantiate(anprefabs);
                    temp.transform.localScale = Vector3.one;
                    temp.transform.localEulerAngles = new Vector3(30, 0, 0);
                    temp.transform.localPosition = hit.point;
                    followAnImage.gameObject.SetActive(false);
                    currentState = AnState.Idle; //改變狀態!!!
                }
            }
        }
    }

    private void ProcessCancel()
    {
        // 隱藏跟隨圖片
        followAnImage.gameObject.SetActive(false);
    
        // 重新啟用按鈕
        btnAn.interactable = true;
    
        // 重置狀態
        currentState = AnState.Idle;
    
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
            currentState = AnState.Idle;
        }
    }
}
