using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositioner : MonoBehaviour
{
    public Vector3 defaultPosition = new Vector3(499.99f, 0.1113546f, 497.3f);
    
    private void Start()
    {
        // 獲取場景中的玩家對象
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            // 檢查是否是新遊戲開始 (從Video1過來)
            bool isNewGame = PlayerPrefs.GetInt("IsNewGame", 0) == 1;
            
            // 獲取上一個場景的名稱
            string lastScene = PlayerPrefs.GetString("LastScene", "");
            
            if (isNewGame || lastScene == "Video1" || lastScene == "MainMenu" || string.IsNullOrEmpty(lastScene))
            {
                // 新遊戲或從主選單/視頻開始，使用預設位置
                player.transform.position = defaultPosition;
                
                // 重置新遊戲標記
                PlayerPrefs.SetInt("IsNewGame", 0);
            }
            else if (lastScene == "Market")
            {
                // 從Market回到S1的位置
                player.transform.position = new Vector3(500.1843f, 0.1113546f, 505.86f);
            }
            else if (lastScene == "Park")
            {
                // 從Park回到S1的位置
                player.transform.position = new Vector3(499.953f, 0.1113546f, 492.4973f);
            }
        }
    }
}