using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TowerPnlMgr : MonoBehaviour
{
    public InfoPanelHandler infoPanelHandler;
    public Button closebtn;
    public GameObject pauseMenu;
    public GameObject teachPnl;
    public AudioClip btnsfx;
    //private EscMgr escManager; 
    public UnityEvent<NpcType> OnNpcClickEvent;
    public UnityEvent<NpcType> OnNpcBtnCloseEvent;
    
    // 保存當前選中的NPC類型
    private NpcType currentNpcType;

    void Start()
    {
        // 初始隱藏資訊面板
        infoPanelHandler.gameObject.SetActive(false);

        closebtn.onClick.AddListener(CloseinfoPanel);

        //escManager = EscMgr.Instance;
    }

    public void CloseinfoPanel()
    {
        GameDB.Audio.PlaySfx(btnsfx);
        infoPanelHandler.gameObject.SetActive(false);
        Time.timeScale = 1;
        if (GameDB.Audio._bgmAudioSource != null)
        {
            GameDB.Audio._bgmAudioSource.ignoreListenerPause = false;
        }
        
        // 使用事件系統通知關閉按鈕被點擊，並傳遞當前NPC類型
        OnNpcBtnCloseEvent?.Invoke(currentNpcType);
    }

    void Update()
    {
        // 當滑鼠左鍵被點擊時
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // 依序檢查各種標籤
                if (hit.transform.gameObject.GetComponent<QionglinTag>() != null)
                {
                    OnNpcClick("qionglin");
                }
                else if (hit.transform.gameObject.GetComponent<HoushuiTag>() != null)
                {
                    OnNpcClick("houshui");
                }
                else if (hit.transform.gameObject.GetComponent<LiuTag>() != null)
                {
                    OnNpcClick("liu");
                }
                else if (hit.transform.gameObject.GetComponent<AnTag>() != null)
                {
                    OnNpcClick("an");
                }
                else if (hit.transform.gameObject.GetComponent<TahouTag>() != null)
                {
                    OnNpcClick("tahou");
                }
            }
        }
    }

    public void OnNpcClick(string npcType)
    {
        Debug.Log($"Click {npcType}");
        if (pauseMenu.activeSelf || infoPanelHandler.isActiveAndEnabled || teachPnl.activeSelf)
        {
            return;
        }

        Time.timeScale = 0;
        float currentVolume = GameDB.Audio._bgmAudioSource.volume;
        if (GameDB.Audio._bgmAudioSource != null)
        {
            if (!GameDB.Audio._bgmAudioSource.isPlaying)
            {
                GameDB.Audio._bgmAudioSource.Play();
            }

            // 保持原有音量，不降低
            GameDB.Audio._bgmAudioSource.volume = currentVolume;
        }

        switch (npcType)
        {
            case "qionglin":
                currentNpcType = NpcType.QiongLin;
                OnNpcClickEvent?.Invoke(NpcType.QiongLin);
                break;
            case "houshui":
                currentNpcType = NpcType.HouShui;
                OnNpcClickEvent?.Invoke(NpcType.HouShui);
                break;
            case "liu":
                currentNpcType = NpcType.Liu;
                OnNpcClickEvent?.Invoke(NpcType.Liu);
                break;
            case "an":
                currentNpcType = NpcType.An;
                OnNpcClickEvent?.Invoke(NpcType.An);
                break;
            case "tahou":
                currentNpcType = NpcType.TaHou;
                OnNpcClickEvent?.Invoke(NpcType.TaHou);
                break;
        }
    }
}