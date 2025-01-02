using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDB
{
    public static string clickItemName;
    
    //Player
    public static int playerHp = 0;
    public static int playerAtk = 0;
    public static int playerDef = 0;
    public static float playerSpd = 0.0f;
    
    //Shop
    public static Res res;
    public static int money;
    
    //Enemy
    public static int enemyHp = 0;
    public static int enemyAtk = 0;
    public static int enemyDef =0;
    
    //Tower
    public static int towerHp;
    public static int towerAtk;
    public static int towerDef;
    public static int towerAtkSpd;
    
    //PauseMenu
    public static bool pausemenu = false;
    // Audio
    public static AudioMgr Audio;
    public static ConfigData Config = new ConfigData();
    public class ConfigData
    {
        public float bgmAudioVolume = 1.0f;  // 預設音量為 1
        public float sfxAudioVolume = 1.0f;  // 預設音量為 1
    }
    //PlayerPrefs
    public static void Save()
    {
       
    }
    
    public static void Load()
    {
       
    }
    public static Vector3 playerPosition = Vector3.zero; // 玩家位置

    public static void UpdatePlayerPosition(string fromScene)
    {
        // 根據來源場景設置玩家位置
        if (fromScene == "S2")
        {
            playerPosition = new Vector3(500.0653f, 0.1113f, 505.7999f);
        }
        else if (fromScene == "S3")
        {
            playerPosition = new Vector3(500.09f, 0.1113f, 492.08f);
        }
    }
    
}


