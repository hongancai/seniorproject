using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiuMgr : MonoBehaviour
{
   public GameObject liuprefabs;
   public AudioClip placingsfx;
   public GridHighlightManager gridManager;
   public InfoPanelHandler infoPanelHandler;
   public TowerPnlMgr towerPnlMgr;
    public enum LiuState
    {
        Idle,
        Placing,
        Cancel,
        Drag,
        OpenPnl,
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
        
        if (towerPnlMgr == null)
        {
            towerPnlMgr = FindObjectOfType<TowerPnlMgr>();
        }
    }

    private void OnBtnLiuClick()
    {
        followLiuImage.gameObject.SetActive(true); 
        currentState = LiuState.Placing;
        btnLiu.interactable = false;
        if (gridManager != null)
        {
            gridManager.ShowAllValidAreas();
        }
        Debug.Log("開始丟劉澳風獅爺喔");
    }


    void Update()
    {
        switch (currentState)
        {
            case LiuState.Idle:
                ProcessIdle();
                break;
            case LiuState.Placing:
                ProcessPlacingTower();
                if (Input.GetMouseButtonDown(1))  // 按下右鍵
                {
                    currentState = LiuState.Cancel;  // 切換到取消狀態
                }
                break;
            case LiuState.Cancel:
                ProcessCancel();
                break;
            case LiuState.Drag:
            case LiuState.OpenPnl:
                ProcessOpenPanel();
                break;
                return;
                ProcessDargTower();
                if (cache砲塔 != null)
                {
                    RacastAll();
                }
                break;
                break;
        }
        if (currentState == LiuState.Placing)
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
                    currentState = LiuState.OpenPnl;
                    FindObjectOfType<TowerPnlMgr>().OnNpcClick("liu");
                }
            }
        }
    }
    private void ProcessOpenPanel()
    {
        if (!infoPanelHandler.isActiveAndEnabled)
        {
            currentState =  LiuState.Idle;
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
                    //Vector3 placePosition = hit.point;
                    GameObject temp = Instantiate(liuprefabs);
                    temp.transform.localScale = Vector3.one;
                    temp.transform.localEulerAngles = new Vector3(30, 0, 0);
                    Vector3 position = hit.point;
                    position.y = 0;
                    temp.transform.localPosition = position;
                    GameDB.liuPos = temp.transform.localPosition;
                    followLiuImage.gameObject.SetActive(false);
                    if (gridManager != null)
                    {
                        gridManager.HideAllHighlights();
                    }
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
            currentState = LiuState.Idle;
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
                currentState = LiuState.Idle;
            }
        }
    }
}
