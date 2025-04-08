using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDB
{
    
    //Player
    public static float playerSpd = 0.0f;
    public static int playerAtk = 0;
    public static int playerHp = 0;
    public static int playerDef = 0;
    public static int money = 100000;
    
    //Shop
    public static List<bool> Bought = new List<bool>();
    public static List<bool> BoughtTower = new List<bool>();
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
        BoughtTower.Clear();
        for (int i = 0; i < 4; i++)
        {
            BoughtTower.Add(false);
        }
        InitializeTowers();
        InitializeEnemys();
    }

    public static void BuyItem(int index)
    {
        Bought[index] = true;
        Save();
    }
    public static void BuyTower(int index)
    {
        BoughtTower[index] = true;
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
    
    public static EnemyNpc storm;
    public static EnemyNpc bird;
    public static EnemyNpc fishman;
    public static EnemyNpc soilder;
    public static EnemyNpc ghost;

    private static void InitializeEnemys()
    {
        storm = new EnemyNpc();
        storm.enemybased = new EnemyBuff() { HP = 50, Atk = 10, Def = 2, Spd = 1.5f};
        
        bird = new EnemyNpc();
        bird.enemybased = new EnemyBuff() { HP = 80, Atk = 15, Def = 5, Spd = 2.0f};
        
        fishman = new EnemyNpc();
        fishman.enemybased = new EnemyBuff() { HP = 120, Atk = 25, Def = 8, Spd = 1.2f};
        
        soilder = new EnemyNpc();
        soilder.enemybased = new EnemyBuff() { HP = 180, Atk = 30, Def = 10, Spd = 1.8f};
        
        ghost = new EnemyNpc();
        ghost.enemybased = new EnemyBuff() { HP = 150, Atk = 20, Def = 5, Spd = 2.5f};
    }
    //Tower
     private static void InitializeTowers()
    {
        // 初始化瓊林風獅爺 
        qionglin = new Npc();
        qionglin.Lv = 1;
        qionglin.based = new Buff() { HP = 100, Atk = 20, Def = 5, SpeedRate = 1.0f };
        
        // 初始化后水頭風獅爺
        houshui = new Npc();
        houshui.Lv = 1;
        houshui.based = new Buff() { HP = 150, Atk = 35, Def = 10, SpeedRate = 1.0f };
        
        // 初始化劉澳風獅爺
        liu = new Npc();
        liu.Lv = 1;
        liu.based = new Buff() { HP = 120, Atk = 25, Def = 8, SpeedRate = 1.0f };
        
        // 初始化安歧風獅爺
        an = new Npc();
        an.Lv = 1;
        an.based = new Buff() { HP = 130, Atk = 30, Def = 9, SpeedRate = 1.0f };
        
        // 初始化塔后風獅爺
        tahou = new Npc();
        tahou.Lv = 1;
        tahou.based = new Buff() { HP = 200, Atk = 15, Def = 12, SpeedRate = 1.0f };
    }
    
    // 升級風獅爺
    public static void UpgradeTower(Npc tower)
    {
        if (tower.Lv >= 5) return; // 最高等級為5
        
        tower.Lv++;
        
        // 根據不同角色和等級設定屬性
        if (tower == qionglin)
        {
            switch (tower.Lv)
            {
                case 2:
                    tower.based = new Buff() { HP = 120, Atk = 25, Def = 7, SpeedRate = 1.0f };
                    break;
                case 3:
                    tower.based = new Buff() { HP = 140, Atk = 30, Def = 9, SpeedRate = 1.0f };
                    break;
                case 4:
                    tower.based = new Buff() { HP = 160, Atk = 35, Def = 11, SpeedRate = 1.0f };
                    break;
                case 5:
                    tower.based = new Buff() { HP = 180, Atk = 40, Def = 13, SpeedRate = 1.0f };
                    break;
            }
        }
        else if (tower == houshui)
        {
            switch (tower.Lv)
            {
                case 2:
                    tower.based = new Buff() { HP = 180, Atk = 40, Def = 13, SpeedRate = 1.0f };
                    break;
                case 3:
                    tower.based = new Buff() { HP = 210, Atk = 45, Def = 16, SpeedRate = 1.0f };
                    break;
                case 4:
                    tower.based = new Buff() { HP = 240, Atk = 50, Def = 19, SpeedRate = 1.0f };
                    break;
                case 5:
                    tower.based = new Buff() { HP = 270, Atk = 55, Def = 22, SpeedRate = 1.0f };
                    break;
            }
        }
        else if (tower == liu)
        {
            switch (tower.Lv)
            {
                case 2:
                    tower.based = new Buff() { HP = 140, Atk = 30, Def = 10, SpeedRate = 1.0f };
                    break;
                case 3:
                    tower.based = new Buff() { HP = 160, Atk = 35, Def = 12, SpeedRate = 1.0f };
                    break;
                case 4:
                    tower.based = new Buff() { HP = 180, Atk = 40, Def = 14, SpeedRate = 1.0f };
                    break;
                case 5:
                    tower.based = new Buff() { HP = 200, Atk = 45, Def = 16, SpeedRate = 1.0f };
                    break;
            }
        }
        else if (tower == an)
        {
            switch (tower.Lv)
            {
                case 2:
                    tower.based = new Buff() { HP = 160, Atk = 35, Def = 11, SpeedRate = 1.0f };
                    break;
                case 3:
                    tower.based = new Buff() { HP = 190, Atk = 40, Def = 13, SpeedRate = 1.0f };
                    break;
                case 4:
                    tower.based = new Buff() { HP = 220, Atk = 45, Def = 15, SpeedRate = 1.0f };
                    break;
                case 5:
                    tower.based = new Buff() { HP = 250, Atk = 50, Def = 17, SpeedRate = 1.0f };
                    break;
            }
        }
        else if (tower == tahou)
        {
            switch (tower.Lv)
            {
                case 2:
                    tower.based = new Buff() { HP = 230, Atk = 18, Def = 14, SpeedRate = 1.0f };
                    break;
                case 3:
                    tower.based = new Buff() { HP = 260, Atk = 21, Def = 16, SpeedRate = 1.0f };
                    break;
                case 4:
                    tower.based = new Buff() { HP = 290, Atk = 24, Def = 18, SpeedRate = 1.0f };
                    break;
                case 5:
                    tower.based = new Buff() { HP = 320, Atk = 27, Def = 20, SpeedRate = 1.0f };
                    break;
            }
        }
    }
    
    // 根據等級獲取風獅爺的數值
    public static Buff GetTowerStatsByLevel(string towerType, int level)
    {
        Buff stats = new Buff();
        
        switch (towerType)
        {
            case "qionglin": // 瓊林
                switch (level)
                {
                    case 1: stats = new Buff() { HP = 100, Atk = 20, Def = 5, SpeedRate = 1.0f }; break;
                    case 2: stats = new Buff() { HP = 120, Atk = 25, Def = 7, SpeedRate = 1.0f }; break;
                    case 3: stats = new Buff() { HP = 140, Atk = 30, Def = 9, SpeedRate = 1.0f }; break;
                    case 4: stats = new Buff() { HP = 160, Atk = 35, Def = 11, SpeedRate = 1.0f }; break;
                    case 5: stats = new Buff() { HP = 180, Atk = 40, Def = 13, SpeedRate = 1.0f }; break;
                }
                break;
            
            case "houshui": // 后水頭
                switch (level)
                {
                    case 1: stats = new Buff() { HP = 150, Atk = 35, Def = 10, SpeedRate = 1.0f }; break;
                    case 2: stats = new Buff() { HP = 180, Atk = 40, Def = 13, SpeedRate = 1.0f }; break;
                    case 3: stats = new Buff() { HP = 210, Atk = 45, Def = 16, SpeedRate = 1.0f }; break;
                    case 4: stats = new Buff() { HP = 240, Atk = 50, Def = 19, SpeedRate = 1.0f }; break;
                    case 5: stats = new Buff() { HP = 270, Atk = 55, Def = 22, SpeedRate = 1.0f }; break;
                }
                break;
                
            case "liu": // 劉澳
                switch (level)
                {
                    case 1: stats = new Buff() { HP = 120, Atk = 25, Def = 8, SpeedRate = 1.0f }; break;
                    case 2: stats = new Buff() { HP = 140, Atk = 30, Def = 10, SpeedRate = 1.0f }; break;
                    case 3: stats = new Buff() { HP = 160, Atk = 35, Def = 12, SpeedRate = 1.0f }; break;
                    case 4: stats = new Buff() { HP = 180, Atk = 40, Def = 14, SpeedRate = 1.0f }; break;
                    case 5: stats = new Buff() { HP = 200, Atk = 45, Def = 16, SpeedRate = 1.0f }; break;
                }
                break;
                
            case "an": // 安歧
                switch (level)
                {
                    case 1: stats = new Buff() { HP = 130, Atk = 30, Def = 9, SpeedRate = 1.0f }; break;
                    case 2: stats = new Buff() { HP = 160, Atk = 35, Def = 11, SpeedRate = 1.0f }; break;
                    case 3: stats = new Buff() { HP = 190, Atk = 40, Def = 13, SpeedRate = 1.0f }; break;
                    case 4: stats = new Buff() { HP = 220, Atk = 45, Def = 15, SpeedRate = 1.0f }; break;
                    case 5: stats = new Buff() { HP = 250, Atk = 50, Def = 17, SpeedRate = 1.0f }; break;
                }
                break;
                
            case "tahou": // 塔后
                switch (level)
                {
                    case 1: stats = new Buff() { HP = 200, Atk = 15, Def = 12, SpeedRate = 1.0f }; break;
                    case 2: stats = new Buff() { HP = 230, Atk = 18, Def = 14, SpeedRate = 1.0f }; break;
                    case 3: stats = new Buff() { HP = 260, Atk = 21, Def = 16, SpeedRate = 1.0f }; break;
                    case 4: stats = new Buff() { HP = 290, Atk = 24, Def = 18, SpeedRate = 1.0f }; break;
                    case 5: stats = new Buff() { HP = 320, Atk = 27, Def = 20, SpeedRate = 1.0f }; break;
                }
                break;
        }
        
        return stats;
    }
    
    public static Npc qionglin;
    public static Npc houshui;
    public static Npc liu;
    public static Npc an;
    public static Npc tahou;
    
    public static Vector3 qionglinPos;
    public static Vector3 houshuiPos;
    public static Vector3 liuPos;
    public static Vector3 anPos;
    public static Vector3 tahouPos;
    
    public static bool qionglinBtnInteractable = true;
    public static bool houshuiBtnInteractable = true;
    public static bool liuBtnInteractable = true;
    public static bool anBtnInteractable = true;
    public static bool tahouBtnInteractable = true;
    
    public static int towerHp;
    public static int liuEffect = 30;
    public static int anEffect = 30;
    
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
        
        for (int i = 0; i < Bought.Count; i++)
        {
            PlayerPrefs.SetInt($"Shop_Bought_{i}", Bought[i] ? 1 : 0);
            
        }
    
        // 塔防商店物品
        for (int i = 0; i < BoughtTower.Count; i++)
        {
            PlayerPrefs.SetInt($"Tower_Bought_{i}", BoughtTower[i] ? 1 : 0);
        }
    }
    
    public static void Load()
    {
        money = PlayerPrefs.GetInt("Money", 0);
        // 一般商店物品
        for (int i = 0; i < Bought.Count; i++)
        {
            Bought[i] = PlayerPrefs.GetInt($"Shop_Bought_{i}", 0) == 1;
        }
    
        // 塔防商店物品
        for (int i = 0; i < BoughtTower.Count; i++)
        {
            BoughtTower[i] = PlayerPrefs.GetInt($"Tower_Bought_{i}", 0) == 1;
        }
        
        
        Config.bgmAudioVolume = PlayerPrefs.GetFloat("BGM_Volume", 0.6f);
        Config.sfxAudioVolume = PlayerPrefs.GetFloat("SFX_Volume", 0.8f);
    
        // 如果AudioMgr已經初始化，則立即應用音量設置
        if (Audio != null)
        {
            Audio.SetBgmVolume(Config.bgmAudioVolume);
            Audio.SetSfxVolume(Config.sfxAudioVolume);
        }
    }
    public static void ResetAll()
    {
        money = 100000;  // 重置金錢
    
        // 重置所有商品購買狀態
        for (int i = 0; i < Bought.Count; i++)
        {
            Bought[i] = false;
        }
        for (int i = 0; i < BoughtTower.Count; i++)
        {
            BoughtTower[i] = false;
        }
        qionglinPos = Vector3.zero;
        houshuiPos = Vector3.zero;
        liuPos = Vector3.zero;
        anPos = Vector3.zero;
        tahouPos = Vector3.zero;
        
        qionglinBtnInteractable = true;
        houshuiBtnInteractable = true;
        liuBtnInteractable = true;
        anBtnInteractable = true;
        tahouBtnInteractable = true;
        // 儲存重置後的狀態
        Save();
    }
    
}


