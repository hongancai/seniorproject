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
    
    void Start()
    {
        // 初始隱藏資訊面板
        infoPanelHandler.gameObject.SetActive(false);
        
        closebtn.onClick.AddListener(CloseinfoPanel);
    }

    private void CloseinfoPanel()
    {
        infoPanelHandler.gameObject.SetActive(false);
    }

    void Update()
    {
        // 當滑鼠左鍵被點擊時
        if (Input.GetMouseButtonDown(0))
        {
            if (pauseMenu.activeSelf || infoPanelHandler.isActiveAndEnabled || teachPnl.activeSelf)
            {
                return;
            }
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
    }
}
