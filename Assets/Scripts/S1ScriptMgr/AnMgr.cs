using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnMgr : WindLionGodBaseMgr
{
    public GameObject anprefabs;
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
    //private AnState currentState;
    public Button btnAn;
    //public GameObject followAnImage;
   
    private GameObject cache砲塔;
    
    #region Life Cycle
     private void Start()
    {
        cache砲塔 = null;
        currentState = Status.Idle;
        btnAn.onClick.AddListener(OnBtnAnClick);
        btnAn.interactable = GameDB.anBtnInteractable;
        if (towerPnlMgr == null)
        {
            towerPnlMgr = FindObjectOfType<TowerPnlMgr>();
        }
        
    }

    private  void Update()
    {
        Debug.Log ("============>"+currentState);
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

    private void OnBtnAnClick()
    {
        followImage.gameObject.SetActive(true);
        currentState = Status.Placing;
        btnAn.interactable = false;
        GameDB.anBtnInteractable = false;
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
                if (hit.transform.gameObject.GetComponent<AnTag>() != null)
                {
                    // cache 
                    cache砲塔 = hit.transform.gameObject;
                    //該狀態
                    currentState = Status.OpenPnl;
                    FindObjectOfType<TowerPnlMgr>().OnNpcClick("an");
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
                        _avatar = Instantiate(anprefabs);
                        _avatar.transform.localScale = Vector3.one;
                        _avatar.transform.localEulerAngles = new Vector3(30, 0, 0);
                    }
                    Vector3 position = hit.point;
                    position.y = 0;
                    _avatar.transform.localPosition = position;
                    _avatar.SetActive(true);
                    GameDB.anPos = _avatar.transform.localPosition;
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
            hit.transform.gameObject.GetComponent<AnTag>();
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