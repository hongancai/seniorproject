using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPnlMgr : MonoBehaviour
{ 
    public InfoPanelHandler infoPanelHandler;
    public Button closebtn;
    public GameObject pauseMenu;
    public GameObject teachPnl;
    
    //private EscMgr escManager; 
    
    void Start()
    {
        // 初始隱藏資訊面板
        infoPanelHandler.gameObject.SetActive(false);
        
        closebtn.onClick.AddListener(CloseinfoPanel);
        
        //escManager = EscMgr.Instance;
    }

    public void CloseinfoPanel()
    {
        infoPanelHandler.gameObject.SetActive(false);
        Time.timeScale = 1;
        if (GameDB.Audio._bgmAudioSource != null)
        {
            GameDB.Audio._bgmAudioSource.ignoreListenerPause = false;
        }
        //if (escManager != null)
        {
            //escManager.UnregisterPanel(EscMgr.ESCPanelState.TowerPanel);
        }
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
                infoPanelHandler.Setup(GameDB.qionglin, "qionglin");
                break;
            case "houshui":
                infoPanelHandler.Setup(GameDB.houshui, "houshui");
                break;
            case "liu":
                infoPanelHandler.Setup(GameDB.liu, "liu");
                break;
            case "an":
                infoPanelHandler.Setup(GameDB.an, "an");
                break;
            case "tahou":
                infoPanelHandler.Setup(GameDB.tahou, "tahou");
                break;
        }
       
        //if (escManager != null)
        {
            //escManager.OpenTowerPanel();
        }
    }
}
