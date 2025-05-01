using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // 金幣預製體
    
    // 用於測試的按鍵，預設為空格鍵
    public KeyCode spawnKey = KeyCode.Space;
    
    void Update()
    {
        // 按下指定按鍵時生成金幣
        if (Input.GetKeyDown(spawnKey))
        {
            SpawnCoin();
        }
    }
    
    // 在指定位置生成金幣
    public void SpawnCoin()
    {
        // 在相機前方5單位處生成金幣
        Vector3 spawnPosition = new Vector3(499.99f, 0f, 497.3f);
        
        // 實例化金幣
        GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        coin.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // 將縮放設為0.5
        
        // 確保它有Money組件
        if (coin.GetComponent<Money>() == null)
        {
            coin.AddComponent<Money>();
        }
        
        // 儲存金幣位置到GameDB
        GameDB.coinPos = spawnPosition;
        GameDB.Save();
        
        Debug.Log($"已生成金幣，位置: {spawnPosition}");
    }
    
    // 提供一個公開方法，可以從按鈕點擊事件或其他腳本調用
    public void SpawnCoinFromButton()
    {
        SpawnCoin();
    }
}