using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOpener : MonoBehaviour
{
    public GameObject shopUI;  // 指定商店的UI介面
    public LayerMask shopLayerMask;  // 用於區分哪些物件是可以點擊的，例如你的 Quad

    void Update()
    {
        // 檢測玩家點擊
        if (Input.GetMouseButtonDown(0))  // 檢查滑鼠左鍵點擊
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 如果點擊到物件
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, shopLayerMask))
            {
                // 檢查點擊的是否是 Quad（或是你想要的物件）
                if (hit.collider != null && hit.collider.CompareTag("ShopQuad"))
                {
                    ToggleShopUI();
                }
            }
        }
    }

    // 顯示或隱藏商店
    void ToggleShopUI()
    {
        bool isShopActive = shopUI.activeSelf;
        shopUI.SetActive(!isShopActive);  // 切換商店UI的顯示狀態
        
    }
}