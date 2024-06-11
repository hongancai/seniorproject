using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TargetHandler : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        ProcessRayCast();
    }

    // 處理光線投射
    void ProcessRayCast()
    {
        // 判斷是否有按鈕按下
        if (Input.GetMouseButtonDown(0))
        {
            // 取得座標
            Vector3 mousePosition = Input.mousePosition;

            Debug.Log("Mouse Click On : " + mousePosition); // for debug

            // 以攝影機為參考計算光追
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            // 光追 (Mathf.Infinity = 無限遠)
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                // 獲取物件
                GameObject go = hit.collider.gameObject;

                Debug.Log(go.name); // Debug

                // 可以在這裡處理對象點擊邏輯
                // 比如調用一個點擊處理方法
                OnObjectClicked(go);
            }
        }
    }

    // 處理對象點擊
    void OnObjectClicked(GameObject clickedObject)
    {
        Debug.Log("Clicked on object: " + clickedObject.name);
        // 在這裡添加點擊對象後的處理邏輯
    }
}