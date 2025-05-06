using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiuMgr : WindLionGodBaseMgr
{
   public GameObject liuprefabs;
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
    //private LiuState currentState;
    public Button btnLiu;
    //public GameObject followLiuImage;
   
    private GameObject cache砲塔;
    
   #region Life Cycle
    private void Start()
    {
        cache砲塔 = null;
        currentState = Status.Idle;
        btnLiu.onClick.AddListener(OnBtnLiuClick);
        btnLiu.interactable = GameDB.liuBtnInteractable;
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

    private void OnBtnLiuClick()
    {
        followImage.gameObject.SetActive(true);
        currentState = Status.Placing;
        btnLiu.interactable = false;
        GameDB.liuBtnInteractable = false;
        if (gridManager != null)
        {
            gridManager.ShowAllValidAreas();
        }
        Debug.Log("開始丟劉澳風獅爺喔");
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
                if (hit.transform.gameObject.GetComponent<LiuTag>() != null)
                {
                    // cache 
                    cache砲塔 = hit.transform.gameObject;
                    //該狀態
                    currentState = Status.OpenPnl;
                    FindObjectOfType<TowerPnlMgr>().OnNpcClick("liu");
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
                        _avatar = Instantiate(liuprefabs);
                        _avatar.transform.localScale =  new Vector3(2.3f,2.3f,1);
                        _avatar.transform.localEulerAngles = new Vector3(30, 0, 0);
                    }
                    Vector3 position = hit.point;
                    position.y = 0.6f;
                    _avatar.transform.localPosition = position;
                    _avatar.SetActive(true);
                    GameDB.liuPos = _avatar.transform.localPosition;
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
            hit.transform.gameObject.GetComponent<LiuTag>();
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