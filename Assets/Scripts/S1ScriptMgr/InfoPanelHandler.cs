using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class InfoPanelHandler : MonoBehaviour
{
    private bool _isDirty = false;
    public TextMeshProUGUI Lv; 
    public TextMeshProUGUI Atk; 
    public TextMeshProUGUI Def;
    public TextMeshProUGUI SpeedRate;
    
    public RawImage imageDisplay1;  // 第一張圖
    public RawImage imageDisplay2;  // 第二張圖
    public RawImage imageDisplay3;  // 第三張圖
    
    public Texture2D[] npc1Images = new Texture2D[3]; // 按鈕 1 開啟的 3 張圖片
    public Texture2D[] npc2Images = new Texture2D[3]; // 按鈕 2 開啟的 3 張圖片
    public Texture2D[] npc3Images = new Texture2D[3]; // 按鈕 3 開啟的 3 張圖片
    public Texture2D[] npc4Images = new Texture2D[3]; // 按鈕 3 開啟的 3 張圖片
    public Texture2D[] npc35Images = new Texture2D[3]; // 按鈕 3 開啟的 3 張圖片

    public Button upgradebtn;
    public GameObject warningPnl;
    //public AudioClip upgradesfx;

    private Npc npc; 
    void Start()
    {
        upgradebtn.onClick.AddListener(OnUpgradeClick);
        warningPnl.gameObject.SetActive(false);
    }

    private void OnUpgradeClick()
    {
        int upgradeCost = 200;
        if (GameDB.money > upgradeCost)
        {
            if (GameDB.qionglin.Lv < 5)
            {
                GameDB.money -= upgradeCost;
                GameDB.UpgradeTower(GameDB.qionglin);
                //GameDB.Audio.PlaySfx(upgradesfx);
            }
            else
            {
                upgradebtn.gameObject.SetActive(false);
            }
            {
                warningPnl.gameObject.SetActive(true);
                Debug.Log("你不夠200塊");
            }

         GameDB.Save();
        }
    }

    void Update()
    {
        if (_isDirty)
        {
            //
            Lv.text = "Lv." + npc.Lv.ToString("D2"); //D2 ==>  1 => 01

            Buff buff = npc.GetFinalBuff();  //加權 過後的結果
            
            SpeedRate.text = "SpeedRate: " + buff.HP;
            
            Atk.text = "ATK: " + buff.Atk;
                
            Def.text = "DEF: " + buff.Def;
            
            
            
            _isDirty = false;
        }
    }

    public void Setup(Npc source)
    {
        this.npc = source;
        this.gameObject.SetActive(true); //把自己打開
        
        _isDirty = true; // 強迫用update 更新畫面
    }
}