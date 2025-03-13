using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositioner : MonoBehaviour
{
    public Vector3 defaultPosition = new Vector3(499.99f, 0.1113546f, 498.68f);
    private void Start()
    {
        // 獲取上一個場景的名稱
        string lastScene = PlayerPrefs.GetString("LastScene", "");
        
        // 獲取場景中的玩家對象
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            // 根據上一個場景設置玩家位置
            if (lastScene == "Market")
            {
                // 從Market回到S1的位置
                player.transform.position = new Vector3(500.1843f, 0.1113546f, 505.86f);
            }
            else if (lastScene == "Park")
            {
                // 從Park回到S1的位置
                player.transform.position = new Vector3(499.953f, 0.1113546f, 492.4973f);
            }
            else if (lastScene == "MainMenu")
            {
                player.transform.position = defaultPosition;
            }
        }
    }
}