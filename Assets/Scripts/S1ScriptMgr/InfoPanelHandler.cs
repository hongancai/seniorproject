using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Serialization;
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
    
    public Texture2D[] npc1Images = new Texture2D[3]; 
    public Texture2D[] npc2Images = new Texture2D[3]; 
    public Texture2D[] npc3Images = new Texture2D[3]; 
    public Texture2D[] npc4Images = new Texture2D[3]; 
    public Texture2D[] npc5Images = new Texture2D[3]; 
    
    private Npc currentNpc;
    private string currentNpcType;
    
    //public Button upgradebtn;
    //public GameObject warningPnl;
    //public AudioClip upgradesfx;


    public Button btnUpgrade;
    public Button btnReplace;
    private Npc npc; 

    public UnityEvent OnPanelClosingEvent;

    void Start()
    {
        btnUpgrade.onClick.AddListener(OnUpgradeClick);
        btnReplace.onClick.AddListener(OnReplaceClick);
        //warningPnl.gameObject.SetActive(false);
    }
    private void OnUpgradeClick()
    {
        int upgradeCost = 100;
        if (GameDB.money > upgradeCost)
        {
            if (GameDB.qionglin.Lv < 5)
            {
                GameDB.money -= upgradeCost;
                //GameDB.UpgradeTower(GameDB.qionglin);
                //GameDB.Audio.PlaySfx(upgradesfx);
            }
            else
            {
                //upgradebtn.gameObject.SetActive(false);
            }
            {
                //warningPnl.gameObject.SetActive(true);
                Debug.Log("你不夠200塊");
            }

            GameDB.Save();
        }
    }

    private void OnReplaceClick()
    {
        
        OnPanelClosingEvent?.Invoke(); //通知準備關閉面板
        gameObject.SetActive(false); //關閉面板
    }
        
    void Update()
    {
        if (_isDirty)
        {
            if (currentNpc != null)
            {
                //Text
                Lv.text = currentNpc.Lv.ToString("D2"); //D2 ==>  1 => 01

                Buff buff = currentNpc.GetFinalBuff();  //加權 過後的結果
            
                SpeedRate.text = buff.SpeedRate.ToString("F2");
                Atk.text = "" + buff.Atk;
                Def.text = "" + buff.Def;
            }
            _isDirty = false;
        }
    }

    public void Setup(Npc source, string npcType)
    {
        this.npc = source;
        this.currentNpc = source;
        this.currentNpcType = npcType;
        this.gameObject.SetActive(true); //把自己打開
        UpdateImageDisplay(npcType);
        _isDirty = true; // 強迫用update 更新畫面
    }
    private void UpdateImageDisplay(string npcType)
    {
        Texture2D[] selectedImages = GetImageArrayForNpc(npcType);
        
        if (selectedImages != null)
        {
            imageDisplay1.texture = selectedImages[0];
            imageDisplay2.texture = selectedImages[1];
            imageDisplay3.texture = selectedImages[2];
        }
    }

    private Texture2D[] GetImageArrayForNpc(string npcType)
    {
        switch (npcType)
        {
            case "qionglin": return npc1Images;
            case "houshui": return npc2Images;
            case "liu": return npc3Images;
            case "an": return npc4Images;
            case "tahou": return npc5Images;
            default: return null;
        }
    }
}