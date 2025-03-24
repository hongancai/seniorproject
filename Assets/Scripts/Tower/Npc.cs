using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc 
{
    public int Lv;
    public Buff based;  //基礎內容, (依角色給) 
    public List<Buff> skillBuff;


    public Npc()
    {
        based = new Buff() { HP=100,Atk=20,Def=5,SpeedRate = 1.0f};
        skillBuff = new List<Buff>();
    }
    
    
    public Buff GetFinalBuff()
    {
        Buff result = new Buff();

        //自己的基礎數值
        result.HP = based.HP;
        result.Atk = based.Atk;
        result.Def = based.Def;
        result.SpeedRate = based.SpeedRate;
        
        //道具的數值
        for (int i = 0; i < skillBuff.Count; i++)
        {
            result.HP += skillBuff[i].HP;
            result.Atk += skillBuff[i].Atk;
            result.Def += skillBuff[i].Def;
            result.SpeedRate *= skillBuff[i].SpeedRate;
        }
        
        return result;
    }
    
    
    
}
