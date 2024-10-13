using System;
using UnityEngine;

public class ShopOpener : MonoBehaviour
{
    public GameObject shopUI;  // 指定商店的UI介面
    public LayerMask shopLayerMask;  // 用於Raycast檢測的Layer

    void Start()
    {
        shopUI.SetActive(false);
    }

    void Update()
    {
        // 當玩家按下滑鼠右鍵時進行檢測
        if (Input.GetMouseButtonDown(1))  
        {
            PerformRaycast();
        }
    }

    // 執行Raycast檢測
    void PerformRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // 從攝影機到滑鼠指針發射射線
        RaycastHit hit;

        // 使用Raycast檢測射線是否打到了指定的Layer物件
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, shopLayerMask))
        {
            // 檢查擊中的物體是否有"Shop"標籤
            if (hit.collider != null && hit.collider.CompareTag("Shop"))
            {
                OpenShopUI();
            }
        }
    }

    // 打開商店UI
    void OpenShopUI()
    {
        if (shopUI != null)
        {
            shopUI.SetActive(true);
        }
    }
}