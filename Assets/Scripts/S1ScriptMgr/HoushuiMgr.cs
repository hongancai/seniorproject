using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoushuiMgr : MonoBehaviour
{
    public GameObject houshuiprefabs;
    public AudioClip placingsfx;
    public GridHighlightManager gridManager;
    public InfoPanelHandler infoPanelHandler;
    public TowerPnlMgr towerPnlMgr;
    public enum HoushuiState
    {
        Idle,
        Placing,
        Cancel,
        Drag,
        OpenPnl,
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
        btnHoushui.interactable = GameDB.houshuiBtnInteractable;
        if (towerPnlMgr == null)
        {
            towerPnlMgr = FindObjectOfType<TowerPnlMgr>();
        }
    }

    private void OnBtnHoushuiClick()
    {
        followHoushuiImage.gameObject.SetActive(true); 
        currentState = HoushuiState.Placing;
        btnHoushui.interactable = false;
        GameDB.houshuiBtnInteractable = false;
        if (gridManager != null)
        {
            gridManager.ShowAllValidAreas();
        }
        Debug.Log("開始丟后水頭風獅爺喔");
    }


    void Update()
    {
        switch (currentState)
        {
            case HoushuiState.Idle:
                ProcessIdle();
                break;
            case HoushuiState.Placing:
                ProcessPlacingTower();
                if (Input.GetMouseButtonDown(1)) // 按下右鍵
                {
                    currentState = HoushuiState.Cancel; // 切換到取消狀態
                }

                break;
            case HoushuiState.Cancel:
                ProcessCancel();
                break;
            case HoushuiState.Drag:
            case HoushuiState.OpenPnl:
                ProcessOpenPanel();
                break;
                return;
                ProcessDargTower();
                if (cache砲塔 != null)
                {
                    RacastAll();
                }
                break;
        }
        if (currentState == HoushuiState.Placing)
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
                    currentState = HoushuiState.OpenPnl;
                    FindObjectOfType<TowerPnlMgr>().OnNpcClick("houshui");
                }
            }
        }
    }
    private void ProcessOpenPanel()
    {
        if (!infoPanelHandler.isActiveAndEnabled)
        {
            currentState =  HoushuiState.Idle;
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
                    //Vector3 placePosition = hit.point;
                    GameDB.Audio.PlaySfx(placingsfx);
                    GameObject temp = Instantiate(houshuiprefabs);
                    temp.transform.localScale = Vector3.one;
                    temp.transform.localEulerAngles = new Vector3(30, 0, 0);
                    Vector3 position = hit.point;
                    position.y = 0;
                    temp.transform.localPosition = position;
                    GameDB.houshuiPos = temp.transform.localPosition;
                    followHoushuiImage.gameObject.SetActive(false);
                    if (gridManager != null)
                    {
                        gridManager.HideAllHighlights();
                    }
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
        GameDB.houshuiBtnInteractable = true;
        // 重置狀態
        currentState = HoushuiState.Idle;
        if (gridManager != null)
        {
            gridManager.HideAllHighlights();
        }
    
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
    void RacastAll()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Debug.Log(hit.transform.gameObject.name);
            hit.transform.gameObject.GetComponent<HoushuiTag>();
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<RoadTag>() != null)
                {
                    Debug.Log(hit.transform.gameObject.name);
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
}

