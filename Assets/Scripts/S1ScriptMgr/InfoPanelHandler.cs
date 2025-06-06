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
    
    public GridHighlightManager gridManager;
    
    private Npc currentNpc;
    private string currentNpcType;
    
    
    public GameObject warningPnl;
    public AudioClip upgradesfx;
    public AudioClip btnsfx;

    public Button btnCloseWarning;
    public Button btnUpgrade;
    public Button btnReplace;
    public Button btnRebirth;
    private Npc npc;
    public UnityEvent OnPanelClosingEvent;

    void Start()
    {
        btnRebirth.gameObject.SetActive(false);
        btnRebirth.onClick.AddListener(OnReBirthClick);
        btnUpgrade.onClick.AddListener(OnUpgradeClick);
        btnReplace.onClick.AddListener(OnReplaceClick);
        btnCloseWarning.onClick.AddListener(OnCloseWarningClick);
        warningPnl.gameObject.SetActive(false);
    }
    private void OnUpgradeClick()
    {
        if (GameDB.money >= 100)
        {
            if (currentNpc != null && currentNpc.Lv < 5)
            {
                GameDB.money -= 100;
                GameDB.UpgradeTower(currentNpc);
                GameDB.Audio.PlaySfx(upgradesfx);
                if (currentNpc.Lv >= 5)
                {
                    DisableUpgradeButton(currentNpcType);
                    btnUpgrade.interactable = false;
                }
                // 更新介面顯示
                _isDirty = true;
            }
        }
        else
        {
            warningPnl.SetActive(true);
            Debug.Log("你沒100塊升級!!!!!!!");
        }
    }
    private void DisableUpgradeButton(string npcType)
    {
        switch (npcType)
        {
            case "qionglin":
                GameDB.qionglinUpgradeBtnInteractable = false;
                break;
            case "houshui":
                GameDB.houshuiUpgradeBtnInteractable = false;
                break;
            case "liu":
                GameDB.liuUpgradeBtnInteractable = false;
                break;
            case "an":
                GameDB.anUpgradeBtnInteractable = false;
                break;
            case "tahou":
                GameDB.tahouUpgradeBtnInteractable = false;
                break;
        }
    
        // 儲存變更
        GameDB.Save();
    }
    
    private void OnReplaceClick()
    {
        if (GameDB.money > 50)
        {
            GameDB.money -= 50;
            OnPanelClosingEvent?.Invoke(); //通知準備關閉面板
            gameObject.SetActive(false); //關閉面板
            Time.timeScale = 1;
            if (gridManager != null)
            {
                gridManager.ShowAllValidAreas();
            }
        }
        else
        {
            warningPnl.SetActive(true);
            Debug.Log("你沒50塊!!!!!!!!!");
        }
    }
    private void OnReBirthClick()
    {
        if (GameDB.money > 200)
        {
            GameDB.money -= 200;
        }
        else
        {
            warningPnl.SetActive(true);
            Debug.Log("你沒200塊!!!!!!!!!");
        }
    }
    private void OnCloseWarningClick()
    {
        warningPnl.SetActive(false);
        GameDB.Audio.PlaySfx(btnsfx);
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
        // 檢查當前角色的升級按鈕狀態
        CheckUpgradeButtonInteractable(npcType);

        UpdateImageDisplay(npcType);
        _isDirty = true; // 強迫用update 更新畫面
    }
    private void CheckUpgradeButtonInteractable(string npcType)
    {
        bool interactable = true;
    
        // 檢查角色是否已達最高等級
        if (currentNpc.Lv >= 5)
        {
            interactable = false;
        }
        else
        {
            // 從GameDB檢查該類型角色的按鈕互動狀態
            switch (npcType)
            {
                case "qionglin":
                    interactable = GameDB.qionglinUpgradeBtnInteractable;
                    break;
                case "houshui":
                    interactable = GameDB.houshuiUpgradeBtnInteractable;
                    break;
                case "liu":
                    interactable = GameDB.liuUpgradeBtnInteractable;
                    break;
                case "an":
                    interactable = GameDB.anUpgradeBtnInteractable;
                    break;
                case "tahou":
                    interactable = GameDB.tahouUpgradeBtnInteractable;
                    break;
            }
        }
    
        // 設置升級按鈕的互動狀態
        btnUpgrade.interactable = interactable;
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