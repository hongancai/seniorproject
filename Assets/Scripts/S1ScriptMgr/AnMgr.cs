using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnMgr : MonoBehaviour
{
    public GameObject anprefabs;
    public AudioClip placingsfx;
    public GridHighlightManager gridManager;
    public InfoPanelHandler infoPanelHandler;
    public TowerPnlMgr towerPnlMgr;
    public enum AnState
    {
        Idle,
        Placing,
        Cancel,
        Drag,
        OpenPnl,
    }
    private AnState currentState;
    public Button btnAn;
    public GameObject followAnImage;
   
    private GameObject cache砲塔;
    
    void Start()
    {
        cache砲塔 = null;
        currentState = AnState.Idle;
        btnAn.onClick.AddListener(OnBtnAnClick);
        btnAn.interactable = GameDB.anBtnInteractable;
        if (towerPnlMgr == null)
        {
            towerPnlMgr = FindObjectOfType<TowerPnlMgr>();
        }
    }

    private void OnBtnAnClick()
    {
        followAnImage.gameObject.SetActive(true); 
        currentState = AnState. Placing;
        btnAn.interactable = false;
        GameDB.anBtnInteractable = false;
        if (gridManager != null)
        {
            gridManager.ShowAllValidAreas();
        }
        Debug.Log("開始丟安崎風獅爺喔");
    }


    void Update()
    {
        switch (currentState)
        {
            case AnState.Idle:
                ProcessIdle();
                break;
            case AnState. Placing:
                ProcessPlacingTower();
                if (Input.GetMouseButtonDown(1))  // 按下右鍵
                {
                    currentState = AnState.Cancel;  // 切換到取消狀態
                }
                break;
            case AnState.Cancel:
                ProcessCancel();
                break;
            case AnState.Drag:
            case AnState.OpenPnl:
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
        if (currentState == AnState. Placing)
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
                if (hit.transform.gameObject.GetComponent<AnTag>()!= null)
                {
                    // cache 
                    cache砲塔 = hit.transform.gameObject;
                    //該狀態
                    currentState = AnState.OpenPnl;
                    FindObjectOfType<TowerPnlMgr>().OnNpcClick("an");
                }
            }
        }
    }
    private void ProcessOpenPanel()
    {
        if (!infoPanelHandler.isActiveAndEnabled)
        {
            currentState = AnState.Idle;
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
                    GameObject temp = Instantiate(anprefabs);
                    temp.transform.localScale = Vector3.one;
                    temp.transform.localEulerAngles = new Vector3(30, 0, 0);
                    Vector3 position = hit.point;
                    position.y = 0;
                    temp.transform.localPosition = position;
                    GameDB.anPos = temp.transform.localPosition;
                    followAnImage.gameObject.SetActive(false);
                    if (gridManager != null)
                    {
                        gridManager.HideAllHighlights();
                    }
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
        GameDB.anBtnInteractable = true;
        // 重置狀態
        currentState = AnState.Idle;
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
            currentState = AnState.Idle;
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
                currentState = AnState.Idle;
            }
        }
    }
}
