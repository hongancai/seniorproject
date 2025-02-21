using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDB
{
    
    //Player
    public static float playerSpd = 0.0f;
    public static int money;
    //Shop
    
    public static List<bool> Bought = new List<bool>();
    private static bool isInitialized = false;
    static GameDB()
    {
        if (!isInitialized)
        {
            InitializeData();
            isInitialized = true;
        }
    }

    private static void InitializeData()
    {
        // 清空列表以防重複添加
        Bought.Clear();
        // 初始化購買狀態
        for (int i = 0; i < 5; i++)
        {
            Bought.Add(false);
        }
    }

    public static void BuyItem(int index)
    {
        Bought[index] = true;
        Save();
    }
    
    
    //S1 Wave
    public static bool wave1 = false;
    public static bool wave2= false;
    public static bool wave3= false;
    public static bool wave4= false;
    
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
        public float bgmAudioVolume = 0.6f;  // 預設音量為 1
        public float sfxAudioVolume = 0.8f;  // 預設音量為 1
    }
    
    //PlayerPrefs
    public static void Save()
    {
        PlayerPrefs.SetInt("Money", money);
        
        // 儲存購買狀態
        for (int i = 0; i < Bought.Count; i++)
        {
            PlayerPrefs.SetInt($"Bought_{i}", Bought[i] ? 1 : 0);
        }
        
        PlayerPrefs.Save();
    }
    
    public static void Load()
    {
        money = PlayerPrefs.GetInt("Money", 0);
        
        // 讀取購買狀態
        for (int i = 0; i < Bought.Count; i++)
        {
            Bought[i] = PlayerPrefs.GetInt($"Bought_{i}", 0) == 1;
        }
    }
   
    
}


