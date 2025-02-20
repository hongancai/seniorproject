using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S2Mgr : MonoBehaviour
{
    public AudioClip s2bgm;
    public List<Button> goodsButtons;
    public List<GameObject> goodsPnl;
    public List<Button> closegoodsPnlBtn;
    public List<GameObject> buyedItem;
    public GameObject warningPnl;
    public Button closeWarnBtn;
    public Button buy01;
    public Button buy02;
    public Button buy03;
    public Button buy04;
    public Button buy05;

    public GameObject activePanel = null;

    void Start()
    {
        warningPnl.gameObject.SetActive(false);
        GameDB.Audio.PlayBgm(s2bgm);
        // 初始化所有已購買物品的顯示狀態
        for (int i = 0; i < 5; i++)
        {
            buyedItem[i].SetActive(GameDB.Bought[i]);
        }

        // 初始化時關閉所有面板
        foreach (var panel in goodsPnl)
        {
            panel.SetActive(false);
        }

        // 設置開啟面板的按鈕監聽器
        for (int i = 0; i < goodsButtons.Count; i++)
        {
            int index = i; // 重要：要用區域變數來保存索引
            goodsButtons[i].onClick.AddListener(() => OpenPanel(index));
        }

        // 設置關閉面板的按鈕監聽器
        for (int i = 0; i < closegoodsPnlBtn.Count; i++)
        {
            int index = i;
            closegoodsPnlBtn[i].onClick.AddListener(() => ClosePanel(index));
        }
        
        closeWarnBtn.onClick.AddListener(OncCloseWarning);
        buy01.onClick.AddListener(OnBtnBuyItem1);
        buy02.onClick.AddListener(OnBtnBuyItem2);
        buy03.onClick.AddListener(OnBtnBuyItem3);
        buy04.onClick.AddListener(OnBtnBuyItem4);
        buy05.onClick.AddListener(OnBtnBuyItem5);
    }

    private void OncCloseWarning()
    {
        warningPnl.gameObject.SetActive(false);
    }
    
    private void OpenPanel(int index)
    {
        // 如果有活動面板，先關閉它
        if (activePanel != null)
        {
            activePanel.SetActive(false);
        }

        // 開啟新的面板
        goodsPnl[index].SetActive(true);
        activePanel = goodsPnl[index];
    }

    private void ClosePanel(int index)
    {
        goodsPnl[index].SetActive(false);
        if (activePanel == goodsPnl[index])
        {
            activePanel = null;
        }
    }

    public void OnBtnBuyItem1()
    {
        if (GameDB.money > 50 && !GameDB.Bought[0])
        {
            GameDB.money -= 50;
            GameDB.Bought[0] = true;
            buyedItem[0].SetActive(true);
            GameDB.Save();
            Debug.Log("你買了燒餅");
        }
        else
        {
            warningPnl.gameObject.SetActive(true);
            Debug.Log("你不夠50塊");
        }
    }
    public void OnBtnBuyItem2()
    {
        if (GameDB.money > 100 && !GameDB.Bought[1])
        {
            GameDB.money -= 100;
            GameDB.Bought[1] = true;
            buyedItem[1].SetActive(true);
            GameDB.Save();
            Debug.Log("你買了貢糖");
        }
        else
        {
            warningPnl.gameObject.SetActive(true);
            Debug.Log("你不夠100塊");
        }
    }
    public void OnBtnBuyItem3()
    {
        if (GameDB.money > 200 && !GameDB.Bought[2])
        {
            GameDB.money -= 200;
            GameDB.Bought[2] = true;
            buyedItem[2].SetActive(true);
            GameDB.Save();
            Debug.Log("你買了麵線");
        }
        else
        {
            warningPnl.gameObject.SetActive(true);
            Debug.Log("你不夠200塊");
        }
    }
    public void OnBtnBuyItem4()
    {
        if (GameDB.money > 500 && !GameDB.Bought[3])
        {
            GameDB.money -= 500;
            GameDB.Bought[3] = true;
            buyedItem[3].SetActive(true);
            GameDB.Save();
            Debug.Log("你買了燒餅");
        }
        else
        {
            warningPnl.gameObject.SetActive(true);
            Debug.Log("你不夠250塊");
        }
    }
    public void OnBtnBuyItem5()
    {
        if (GameDB.money > 600 && !GameDB.Bought[4])
        {
            GameDB.money -= 600;
            GameDB.Bought[4] = true;
            buyedItem[4].SetActive(true);
            GameDB.Save();
            Debug.Log("你買了高粱");
        }
        else
        {
            warningPnl.gameObject.SetActive(true);
            Debug.Log("你不夠500塊");
        }
    }
}