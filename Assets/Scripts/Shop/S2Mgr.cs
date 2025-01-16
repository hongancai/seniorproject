using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S2Mgr : MonoBehaviour
{
    public AudioClip s2bgm;
    public List<Button> goodsButtons; // 商品按鈕列表
    public List<GameObject> goodsPanels; // 商品面板列表
    public List<Button> boughtItem;
    public Button closeButton; // 關閉按鈕

    private GameObject activePanel = null; // 當前顯示的面板

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            //boughtItem[i].onClick.AddListener(() => OpenPanel(index));
        }

        // 初始化，確保所有面板關閉
        foreach (var panel in goodsPanels)
        {
            panel.SetActive(false);
        }

        // 綁定按鈕點擊事件
        for (int i = 0; i < goodsButtons.Count; i++)
        {
            int index = i; // 避免閉包問題
            goodsButtons[i].onClick.AddListener(() => OpenPanel(index));
        }

        // 關閉按鈕綁定
        closeButton.onClick.AddListener(CloseActivePanel);


        GameDB.Audio.PlayBgm(s2bgm);
    }

    // 打開指定面板
    void OpenPanel(int index)
    {
        // 關閉當前面板（如果有）
        if (activePanel != null)
        {
            activePanel.SetActive(false);
        }

        // 打開新的面板
        activePanel = goodsPanels[index];
        activePanel.SetActive(true);
    }

    // 關閉當前面板
    void CloseActivePanel()
    {
        if (activePanel != null)
        {
            activePanel.SetActive(false);
            activePanel = null; // 重置
        }
    }

    public void OnBtnBuyIem1()
    {
        if (GameDB.money > 80 && !GameDB.Bought[0])
        {
            GameDB.money -= 80;
            GameDB.Bought[0] = true;
        }
        else
        {
            //warning
        }
    }

    public void OnBtnBuyIem2()
    {
        if (GameDB.money > 100 && !GameDB.Bought[1])
        {
            GameDB.money -= 100;
            GameDB.Bought[1] = true;
        }
        else
        {
            //warning
        }
    }

    public void OnBtnBuyIem3()
    {
        if (GameDB.money > 200 && !GameDB.Bought[2])
        {
            GameDB.money -= 200;
            GameDB.Bought[2] = true;
        }
        else
        {
            //warning
        }
    }

    public void OnBtnBuyIem4()
    {
        if (GameDB.money > 500 && !GameDB.Bought[3])
        {
            GameDB.money -= 500;
            GameDB.Bought[3] = true;
        }
        else
        {
            //warning
        }
    }

    public void OnBtnBuyIem5()
    {
        if (GameDB.money > 600 && !GameDB.Bought[4])
        {
            GameDB.money -= 600;
            GameDB.Bought[4] = true;
        }
        else
        {
            //warning
        }
    }
}