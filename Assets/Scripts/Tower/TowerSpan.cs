using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpan : MonoBehaviour
{
    public GameObject backpackPanel;          // 書包介面
    public Button lionStatueButton;           // 風獅爺塔按鈕
    public GameObject lionStatuePrefab;       // 風獅爺塔的預製物件
    private bool isPlacingLionStatue = false; // 是否進入選擇模式

    void Start()
    {
        // 為按鈕添加點擊事件
        lionStatueButton.onClick.AddListener(OnLionStatueButtonClicked);
    }

    // 當按下風獅爺塔按鈕
    private void OnLionStatueButtonClicked()
    {
        // 進入選擇模式並隱藏書包介面
        isPlacingLionStatue = true;
        backpackPanel.SetActive(false);
    }

    void Update()
    {
        if (isPlacingLionStatue && Input.GetMouseButtonDown(0))
        {
            // 檢測滑鼠點擊的物件
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // 檢查點擊的物件是否為 Plane
                if (hit.collider.CompareTag("PlaceablePlane"))
                {
                    // 在選中的 Plane 上生成風獅爺塔
                    Instantiate(lionStatuePrefab, hit.point, Quaternion.identity);
                    
                    // 結束選擇模式
                    isPlacingLionStatue = false;
                }
            }
        }
    }
}