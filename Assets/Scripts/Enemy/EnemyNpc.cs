using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNpc 
{
    public EnemyBuff enemybased;

    void Start()
    {
        enemybased = new EnemyBuff() { HP = 50, Atk = 10, Def = 2, Spd = 1.5f };
    }


    void Update()
    {

    }

    public  EnemyBuff GetFinalEnemyBuff()
    {
        EnemyBuff result = new EnemyBuff();
        
        result.HP = enemybased.HP;
        result.Atk = enemybased.Atk;
        result.Def = enemybased.Def;
        result.Spd = enemybased.Spd;
        return result;
    }
   

}
