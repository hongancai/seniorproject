using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class S1Mgr : MonoBehaviour
{
    public AudioClip s1bgm;
    public Image blackScreen;
    public List<GameObject> buyedItems;
    public List<GameObject> buyTowers;
    public GameObject qionglinprefabs;
    public GameObject houshuiprefabs;
    public GameObject liuprefabs;
    public GameObject anprefabs;
    public GameObject tahouprefabs;

    public Button qionglinButton;
    public Button houshuiButton;
    public Button liuButton;
    public Button anButton;
    public Button tahouButton;
    
    //
    public InfoPanelHandler _infoPanelHandler;

    private WindLionGodBaseMgr _cacheWindLionGod; //暫存，
    private QionglinMgr _qionglinMgr;
    private HoushuiMgr _houshuiMgr;
    private LiuMgr _liuMgr;
    private AnMgr _anMgr;
    private TahouMgr _tahouMgr;
    
    private TowerPnlMgr _towerPnlMgr;
    

    #region Life Cycle

    private void Awake()
    {
        GameEngine.Instance.Initialization();

        ScanObject();

        if (_infoPanelHandler != null)
        {
            _infoPanelHandler.OnPanelClosingEvent.AddListener(CallBackOnPanelClosingEvent);
        }
        if (_towerPnlMgr != null)
        {
            _towerPnlMgr.OnNpcBtnCloseEvent.AddListener(CallBackOnNpcBtnCloseEvent);
        }


        if (GameDB.qionglinPos != Vector3.zero)
        {
            GameObject temp = Instantiate(qionglinprefabs);
            temp.transform.localScale = Vector3.one;
            temp.transform.localEulerAngles = new Vector3(30, 0, 0);
            temp.transform.localPosition = GameDB.qionglinPos;
            _qionglinMgr.SetAvatar(temp);
        }

        if (GameDB.houshuiPos != Vector3.zero)
        {
            GameObject temp = Instantiate(houshuiprefabs);
            temp.transform.localScale = Vector3.one;
            temp.transform.localEulerAngles = new Vector3(30, 0, 0);
            temp.transform.localPosition = GameDB.houshuiPos;
            _houshuiMgr.SetAvatar(temp);
        }

        if (GameDB.liuPos != Vector3.zero)
        {
            GameObject temp = Instantiate(liuprefabs);
            temp.transform.localScale = Vector3.one;
            temp.transform.localEulerAngles = new Vector3(30, 0, 0);
            temp.transform.localPosition = GameDB.liuPos;
            _liuMgr.SetAvatar(temp);
        }

        if (GameDB.anPos != Vector3.zero)
        {
            GameObject temp = Instantiate(anprefabs);
            temp.transform.localScale = Vector3.one;
            temp.transform.localEulerAngles = new Vector3(30, 0, 0);
            temp.transform.localPosition = GameDB.anPos;
            _anMgr.SetAvatar(temp);
        }

        if (GameDB.tahouPos != Vector3.zero)
        {
            GameObject temp = Instantiate(tahouprefabs);
            temp.transform.localScale = Vector3.one;
            temp.transform.localEulerAngles = new Vector3(30, 0, 0);
            temp.transform.localPosition = GameDB.tahouPos;
            _tahouMgr.SetAvatar(temp);
        }
    }
    private void CallBackOnNpcBtnCloseEvent(NpcType type)
    {
        WindLionGodBaseMgr targetMgr = null;
    
        switch (type)
        {
            case NpcType.QiongLin:
                targetMgr = _qionglinMgr;
                break;
            case NpcType.HouShui:
                targetMgr = _houshuiMgr;
                break;
            case NpcType.Liu:
                targetMgr = _liuMgr;
                break;
            case NpcType.An:
                targetMgr = _anMgr;
                break;
            case NpcType.TaHou:
                targetMgr = _tahouMgr;
                break;
        }
    
        if (targetMgr != null)
        {
            GameObject avatar = targetMgr.GetAvatar();
            if (avatar != null)
            {
                avatar.SetActive(true);
            }
            targetMgr.ChangeState(WindLionGodBaseMgr.Status.Idle);
        }
    }
    
    private void Start()
    {
        if (qionglinButton != null)
        {
            qionglinButton.interactable = GameDB.qionglinBtnInteractable;
        }

        if (houshuiButton != null)
        {
            houshuiButton.interactable = GameDB.houshuiBtnInteractable;
        }

        if (liuButton != null)
        {
            liuButton.interactable = GameDB.liuBtnInteractable;
        }

        if (anButton != null)
        {
            anButton.interactable = GameDB.anBtnInteractable;
        }

        if (tahouButton != null)
        {
            tahouButton.interactable = GameDB.tahouBtnInteractable;
        }

        GameDB.Audio.PlayBgm(s1bgm);
        blackScreen.gameObject.SetActive(true);
        blackScreen.color = new Color(0, 0, 0, 1);

        // 開始淡出黑幕
        blackScreen.DOFade(0f, 2f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => { blackScreen.gameObject.SetActive(false); });
        GameDB.Load();
        UpdateDisplay();
    }

    #endregion
    
    #region Event Function

    #endregion
   
    #region  Private Function 
    private void UpdateDisplay()
    {
        GameDB.Load();

        // 根據 GameDB.Bought 的狀態更新物品顯示
        for (int i = 0; i < GameDB.Bought.Count; i++)
        {
            // 假設你有一個存放已購買物品的GameObject列表
            if (buyedItems != null && i < buyedItems.Count)
            {
                buyedItems[i].SetActive(GameDB.Bought[i]);
            }
        }

        for (int i = 0; i < GameDB.BoughtTower.Count; i++)
        {
            // 假設你有一個存放已購買物品的GameObject列表
            if (buyTowers != null && i < buyTowers.Count)
            {
                buyTowers[i].SetActive(GameDB.BoughtTower[i]);
            }
        }
    }

    private void ScanObject()
    {
        _qionglinMgr = this.gameObject.GetComponent<QionglinMgr>();
        _houshuiMgr  = this.gameObject.GetComponent<HoushuiMgr>();
        _liuMgr = this.gameObject.GetComponent<LiuMgr>();
        _anMgr = this.gameObject.GetComponent<AnMgr>();
        _tahouMgr = this.gameObject.GetComponent<TahouMgr>();
       // _infoPanelHandler = this.gameObject.GetComponent<InfoPanelHandler>();
        _towerPnlMgr  = this.gameObject.GetComponent<TowerPnlMgr>();
        _towerPnlMgr.OnNpcClickEvent.AddListener(CallBackOnNpcClick);
    }
    
    #endregion

    #region Call Back Event

    private void CallBackOnNpcClick(NpcType type)
    {
        switch (type)
        {
            case  NpcType.QiongLin:
                
                _cacheWindLionGod = (WindLionGodBaseMgr)_qionglinMgr; //封箱處理
                _qionglinMgr.ChangeState(WindLionGodBaseMgr.Status.OpenPnl);  //切換狀態
                _infoPanelHandler.Setup(GameDB.qionglin, "qionglin"); //開啟面板
                break;
            
            case  NpcType.HouShui:
                
                _cacheWindLionGod = (WindLionGodBaseMgr)_houshuiMgr; //封箱處理
                _houshuiMgr.ChangeState(WindLionGodBaseMgr.Status.OpenPnl);  //切換狀態
                _infoPanelHandler.Setup(GameDB.houshui, "houshui"); //開啟面板
                break;
            
            case  NpcType.Liu:
                
                _cacheWindLionGod = (WindLionGodBaseMgr)_liuMgr; //封箱處理
                _liuMgr.ChangeState(WindLionGodBaseMgr.Status.OpenPnl);  //切換狀態
                _infoPanelHandler.Setup(GameDB.liu, "liu"); //開啟面板
                break;
            
            case  NpcType.An:
                
                _cacheWindLionGod = (WindLionGodBaseMgr)_anMgr; //封箱處理
                _anMgr.ChangeState(WindLionGodBaseMgr.Status.OpenPnl);  //切換狀態
                _infoPanelHandler.Setup(GameDB.an, "an"); //開啟面板
                break;
            
            case  NpcType.TaHou:
                
                _cacheWindLionGod = (WindLionGodBaseMgr)_tahouMgr; //封箱處理
                _tahouMgr.ChangeState(WindLionGodBaseMgr.Status.OpenPnl);  //切換狀態
                _infoPanelHandler.Setup(GameDB.tahou, "tahou"); //開啟面板
                break;
        }
    }

    private void CallBackOnPanelClosingEvent()
    {
        /*
        if (_cacheWindLionGod.GetType() == typeof(QionglinMgr)) // 解箱
        {
        }
        */
        _cacheWindLionGod.ChangeState(WindLionGodBaseMgr.Status.Placing);
    }


    #endregion

}