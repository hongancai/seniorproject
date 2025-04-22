using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoushuiMgr : WindLionGodBaseMgr
{
    public GameObject houshuiprefabs;
    public AudioClip placingsfx;
    public GridHighlightManager gridManager;
    public InfoPanelHandler infoPanelHandler;

    public TowerPnlMgr towerPnlMgr;

/*
public enum HoushuiState
{
    Idle,
    Placing,
    Drag,
    OpenPnl,
}
 */
//private HoushuiState currentState;
    public Button btnHoushui;
    //public GameObject followHoushuiImage;

    private GameObject cache砲塔;

    void Start()
    {
        cache砲塔 = null;
        currentState = Status.Idle;
        btnHoushui.onClick.AddListener(OnBtnHoushuiClick);
        btnHoushui.interactable = GameDB.houshuiBtnInteractable;
        if (towerPnlMgr == null)
        {
            towerPnlMgr = FindObjectOfType<TowerPnlMgr>();
        }
    }

    private void OnBtnHoushuiClick()
    {
        followImage.gameObject.SetActive(true);
        currentState = Status.Placing;
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
            case Status.Idle:
                ProcessIdle();
                break;
            case Status.Placing:
                ProcessPlacingTower();
                break;
            case Status.Drag:
            case Status.OpenPnl:
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

        if (currentState == Status.Placing)
        {
            followImage.transform.position = Input.mousePosition;
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
                if (hit.transform.gameObject.GetComponent<HoushuiTag>() != null)
                {
                    // cache
                    cache砲塔 = hit.transform.gameObject;
                    //該狀態
                    currentState = Status.OpenPnl;
                    FindObjectOfType<TowerPnlMgr>().OnNpcClick("houshui");
                }
            }
        }
    }

    private void ProcessOpenPanel()
    {
        if (!infoPanelHandler.isActiveAndEnabled)
        {
            currentState = Status.Idle;
        }
        
        _avatar.SetActive(false);
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
                    
                    //實例化只產生一次
                    if (_avatar == null)
                    {
                        _avatar = Instantiate(houshuiprefabs);
                        _avatar.transform.localScale = Vector3.one;
                        _avatar.transform.localEulerAngles = new Vector3(30, 0, 0);
                    }
                    Vector3 position = hit.point;
                    position.y = 0;
                    _avatar.transform.localPosition = position;
                    _avatar.SetActive(true);
                    GameDB.houshuiPos = _avatar.transform.localPosition;
                    followImage.gameObject.SetActive(false);
                    if (gridManager != null)
                    {
                        gridManager.HideAllHighlights();
                    }
                    currentState = Status.Idle; //改變狀態!!!
                }
            }
        }
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
            currentState = Status.Idle;
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
                currentState = Status.Idle;
            }
        }
    }
}