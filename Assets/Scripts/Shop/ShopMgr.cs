using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMgr : MonoBehaviour
{
    public GameObject shopPanel;      // 指向商店面板
    public Button closeButton;        // 關閉商店面板的按鈕

    void Start()
    {
        shopPanel.SetActive(false);   // 初始隱藏商店面板
        closeButton.onClick.AddListener(CloseShopPanel); // 設定關閉按鈕的監聽
    }

    // 當玩家點擊商店時顯示商店面板
    private void OnMouseDown()
    {
        OpenShopPanel();
    }

    // 開啟商店面板
    public void OpenShopPanel()
    {
        shopPanel.SetActive(true);
    }

    // 關閉商店面板
    public void CloseShopPanel()
    {
        shopPanel.SetActive(false);
    }
}