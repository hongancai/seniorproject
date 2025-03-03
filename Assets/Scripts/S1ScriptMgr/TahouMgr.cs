using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TahouMgr : MonoBehaviour
{
   public GameObject tahouprefabs;
   public AudioClip placingsfx;
    public enum TahouState
    {
        Idle,
        Placing,
        Cancel,
        Drag,
    }
    private TahouState currentState;
    public Button btnTahou;
    public GameObject followTahouImage;
    
    private GameObject cache砲塔;
    
    void Start()
    {
        cache砲塔 = null;
        currentState = TahouState.Idle;
        btnTahou.onClick.AddListener(OnBtnLiuClick);
    }

    private void OnBtnLiuClick()
    {
        followTahouImage.gameObject.SetActive(true); 
        currentState = TahouState.Placing;
        btnTahou.interactable = false;
        Debug.Log("開始丟劉澳風獅爺喔");
    }


    void Update()
    {
        switch (currentState)
        {
            case TahouState.Idle:
                ProcessIdle();
                break;
            case TahouState.Placing:
                ProcessPlacingTower();
                if (Input.GetMouseButtonDown(1))  // 按下右鍵
                {
                    currentState = TahouState.Cancel;  // 切換到取消狀態
                }
                break;
            case TahouState.Cancel:
                ProcessCancel();
                break;
            case TahouState.Drag:
                ProcessDargTower();
                break;
        }
        if (currentState == TahouState.Placing)
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
                if (hit.transform.gameObject.GetComponent<TahouTag>()!= null)
                {
                    // cache 
                    cache砲塔 = hit.transform.gameObject;
                    //該狀態
                    currentState = TahouState.Drag;
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
                    GameDB.Audio.PlaySfx(placingsfx);
                    Vector3 placePosition = hit.point;
                    GameObject temp = Instantiate(tahouprefabs);
                    temp.transform.localScale = Vector3.one;
                    temp.transform.localEulerAngles = new Vector3(30, 0, 0);
                    Vector3 position = hit.point;
                    position.y = 0;
                    temp.transform.localPosition = position;
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
        }

        if (Input.GetButtonUp("Fire1"))
        {
            cache砲塔 = null;
            currentState = TahouState.Idle;
        }
    }
}
