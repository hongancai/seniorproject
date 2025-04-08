using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QionglinMgr : WindLionGodBaseMgr
{
    public GameObject qionglinprefabs;
    public AudioClip placingsfx;
    public GridHighlightManager gridManager;
    public InfoPanelHandler infoPanelHandler;
    public TowerPnlMgr towerPnlMgr;
   /*
    public enum QionglinState
    {
        Idle,
        Placing,
        Cancel,
        Drag,
        OpenPnl,
    }
    */

   // private QionglinState currentState;
    public Button btnQionglin;
    //public GameObject followImage;

    private GameObject cache砲塔;

    #region Life Cycle
    private void Start()
    {
        cache砲塔 = null;
        currentState = Status.Idle;
        btnQionglin.onClick.AddListener(OnBtnQiongClick);
        btnQionglin.interactable = GameDB.qionglinBtnInteractable;
        if (towerPnlMgr == null)
        {
            towerPnlMgr = FindObjectOfType<TowerPnlMgr>();
        }
    }

    private  void Update()
    {
        switch (currentState)
        {
            case Status.Idle:
                ProcessIdle();
                break;
            case Status.Placing:
                ProcessPlacingTower();
                if (Input.GetMouseButtonDown(1)) // 按下右鍵
                {
                    currentState = Status.Cancel; // 切換到取消狀態
                }
                break;
            case Status.Cancel:
                ProcessCancel();
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

    #endregion

    #region Event Function

    private void OnBtnQiongClick()
    {
        followImage.gameObject.SetActive(true);
        currentState = Status.Placing;
        btnQionglin.interactable = false;
        GameDB.qionglinBtnInteractable = false;
        if (gridManager != null)
        {
            gridManager.ShowAllValidAreas();
        }
        Debug.Log("開始丟瓊林風獅爺喔");
    }

    #endregion
    
    #region  Private Function 
    private void ProcessIdle()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<QionglinTag>() != null)
                {
                    // cache 
                    cache砲塔 = hit.transform.gameObject;
                    //該狀態
                    currentState = Status.OpenPnl;
                    FindObjectOfType<TowerPnlMgr>().OnNpcClick("qionglin");
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
        _avatar.SetActive(false);  //
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
                        _avatar = Instantiate(qionglinprefabs);
                        _avatar.transform.localScale = Vector3.one;
                        _avatar.transform.localEulerAngles = new Vector3(30, 0, 0);
                    }
                    Vector3 position = hit.point;
                    position.y = 0;
                    _avatar.transform.localPosition = position;
                    _avatar.SetActive(true);

                    GameDB.qionglinPos = _avatar.transform.localPosition;
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

    private void ProcessCancel()
    {
        // 隱藏跟隨圖片
        followImage.gameObject.SetActive(false);

        // 重新啟用按鈕
        btnQionglin.interactable = true;
        GameDB.qionglinBtnInteractable = true;

        // 重置狀態
        currentState = Status.Idle;
        
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

    private void RacastAll()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Debug.Log(hit.transform.gameObject.name);
            hit.transform.gameObject.GetComponent<QionglinTag>();
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
    
    #endregion


}