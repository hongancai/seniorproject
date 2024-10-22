using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    
    void Start()
    {
        GameDB.playerDef = 10;
    }

    
    void Update()
    {
        
    }
    public void TakeDamage(int enemyAttack)
    {
        int actualDamage = CalculateDamage(enemyAttack); // 計算實際傷害
        GameDB.playerHp -= actualDamage; // 扣除玩家血量

        // 如果玩家血量小於等於0，觸發死亡
        if (GameDB.playerHp <= 0)
        {
            FindObjectOfType<PlayerHealth>().Die(); // 找到 PlayerHealth 腳本並執行 Die 方法
        }
        else
        {
            FindObjectOfType<PlayerHealth>().UpdateHealthSlider(); // 更新血量條顯示
        }
    }
    private int CalculateDamage(int enemyAttack)
    {
        int damageReduction = GameDB.playerDef; // 玩家防禦力
        int actualDamage = enemyAttack - damageReduction; // 計算實際傷害值

        // 如果實際傷害小於或等於0，則扣1滴血
        if (actualDamage <= 0)
        {
            actualDamage = 1;
        }

        return actualDamage;
    }
}

