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
    //PlayerPrefs
    public static void Save()
    {
       
    }
    
    public static void Load()
    {
       
    }
    
}
