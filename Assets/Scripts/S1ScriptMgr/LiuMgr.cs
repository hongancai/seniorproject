using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiuMgr : MonoBehaviour
{
   public GameObject liuprefabs;
    public enum LiuState
    {
        Idle,
        丟砲塔,
        Cancel,
        拖砲塔,
    }
    private LiuState currentState;
    public Button btnLiu;
    public GameObject followLiuImage;
   
    private GameObject cache砲塔;
    
    void Start()
    {
        cache砲塔 = null;
        currentState = LiuState.Idle;
        btnLiu.onClick.AddListener(OnBtnLiuClick);
    }

    private void OnBtnLiuClick()
    {
        followLiuImage.gameObject.SetActive(true); 
        currentState = LiuState.丟砲塔;
        btnLiu.interactable = false;
        Debug.Log("開始丟劉澳風獅爺喔");
    }


    void Update()
    {
        switch (currentState)
        {
            case LiuState.Idle:
                ProcessIdle();
                break;
            case LiuState.丟砲塔:
                ProcessPlacingTower();
                if (Input.GetMouseButtonDown(1))  // 按下右鍵
                {
                    currentState = LiuState.Cancel;  // 切換到取消狀態
                }
                break;
            case LiuState.Cancel:
                ProcessCancel();
                break;
            case LiuState.拖砲塔:
                ProcessDargTower();
                break;
        }
        if (currentState == LiuState.丟砲塔)
        {
            followLiuImage.transform.position = Input.mousePosition;
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
                if (hit.transform.gameObject.GetComponent<LiuTag>()!= null)
                {
                    // cache 
                    cache砲塔 = hit.transform.gameObject;
                    //該狀態
                    currentState = LiuState.拖砲塔;
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
                    GameObject temp = Instantiate(liuprefabs);
                    temp.transform.localScale = Vector3.one;
                    temp.transform.localEulerAngles = new Vector3(30, 0, 0);
                    temp.transform.localPosition = hit.point;
                    followLiuImage.gameObject.SetActive(false);
                    currentState = LiuState.Idle; //改變狀態!!!
                }
            }
        }
    }

    private void ProcessCancel()
    {
        // 隱藏跟隨圖片
        followLiuImage.gameObject.SetActive(false);
    
        // 重新啟用按鈕
        btnLiu.interactable = true;
    
        // 重置狀態
        currentState = LiuState.Idle;
    
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
            currentState = LiuState.Idle;
        }
    }
}
