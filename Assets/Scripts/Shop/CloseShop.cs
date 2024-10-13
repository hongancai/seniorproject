using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseShop : MonoBehaviour
{
    public GameObject shopUI;  // 指定商店的UI介面
    public Button closeButton; // 關閉按鈕
   
    void Start()
    {
        closeButton.onClick.AddListener(OnBtnCloseShopClick);
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(OnBtnCloseShopClick);
            Debug.Log("Close button listener added successfully.");
        }
        else
        {
            Debug.LogError("Close button is not assigned!");
        }
    }

    void OnBtnCloseShopClick()
    {
        shopUI.SetActive(false);
        if (shopUI != null)
        {
            shopUI.SetActive(false);
            Debug.Log("Shop UI is now closed.");
        }
        else
        {
            Debug.LogError("Shop UI is not assigned!");
        }
    }


    void Update()
    {
        
    }
}

