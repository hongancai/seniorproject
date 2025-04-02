using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TahouMgr : MonoBehaviour
{
   public GameObject tahouprefabs;
   public AudioClip placingsfx;
   public GridHighlightManager gridManager;
   public InfoPanelHandler infoPanelHandler;
   public TowerPnlMgr towerPnlMgr;
    public enum TahouState
    {
        Idle,
        Placing,
        Cancel,
        Drag,
        OpenPnl,
    }
    private TahouState currentState;
    public Button btnTahou;
    public GameObject followTahouImage;
    
    private GameObject cache砲塔;
    
    void Start()
    {
        cache砲塔 = null;
        currentState = TahouState.Idle;
        btnTahou.onClick.AddListener(OnBtnTahouClick);
        btnTahou.interactable = GameDB.tahouBtnInteractable;
        if (towerPnlMgr == null)
        {
            towerPnlMgr = FindObjectOfType<TowerPnlMgr>();
        }
    }

    private void OnBtnTahouClick()
    {
        followTahouImage.gameObject.SetActive(true); 
        currentState = TahouState.Placing;
        btnTahou.interactable = false;
        GameDB.tahouBtnInteractable = false;
        if (gridManager != null)
        {
            gridManager.ShowAllValidAreas();
        }
        Debug.Log("開始丟塔后風獅爺喔");
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
            case TahouState.OpenPnl:
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
                    currentState = TahouState.OpenPnl;
                    FindObjectOfType<TowerPnlMgr>().OnNpcClick("tahou");
                }
            }
        }
    }
    private void ProcessOpenPanel()
    {
        if (!infoPanelHandler.isActiveAndEnabled)
        {
            currentState = TahouState.Idle;
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
                    GameObject temp = Instantiate(tahouprefabs);
                    temp.transform.localScale = Vector3.one;
                    temp.transform.localEulerAngles = new Vector3(30, 0, 0);
                    Vector3 position = hit.point;
                    position.y = 0;
                    temp.transform.localPosition = position;
                    GameDB.tahouPos = temp.transform.localPosition;
                    followTahouImage.gameObject.SetActive(false);
                    if (gridManager != null)
                    {
                        gridManager.HideAllHighlights();
                    }
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
        GameDB.tahouBtnInteractable = true;
        // 重置狀態
        currentState = TahouState.Idle;
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
            currentState = TahouState.Idle;
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
                currentState = TahouState.Idle;
            }
        }
    }
}
