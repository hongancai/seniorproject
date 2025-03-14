using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class S3Mgr : MonoBehaviour
{
    public List<GameObject> tower;
    public List<GameObject> towerPnl; 
    public AudioClip s3bgm;
    public AudioClip closesfx;
    public AudioClip buysfx;
    public Image blackScreen;
    public List<Button> closegoodsPnlBtn;
    public GameObject warningPnl;
    public Button closeWarnBtn;
    public Button buy02;
    public Button buy03;
    public Button buy04;
    public Button buy05;
    
    public GameObject activePanel = null;
    public GameObject pauseMenu;
    private bool isPanelOpen = false;
    private bool isClosingPanel = false; 
    void Start()
    {
        GameDB.Load();
        UpdateButtonVisibility();
        GameDB.Audio.PlayBgm(s3bgm);
        for (int i = 0; i < closegoodsPnlBtn.Count; i++)
        {
            int index = i; // 創建一個區域變數來儲存當前索引
            closegoodsPnlBtn[i].onClick.AddListener(() => ClosePanel(index));
        }
        warningPnl.gameObject.SetActive(false);
        closeWarnBtn.onClick.AddListener(OncCloseWarning);
        buy02.onClick.AddListener(OnBtnBuyItem2);
        buy03.onClick.AddListener(OnBtnBuyItem3);
        buy04.onClick.AddListener(OnBtnBuyItem4);
        buy05.onClick.AddListener(OnBtnBuyItem5);
        blackScreen.gameObject.SetActive(true);
        blackScreen.color = new Color(0, 0, 0, 1);
       
        // 開始淡出黑幕
        blackScreen.DOFade(0f, 2f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => 
            {
                blackScreen.gameObject.SetActive(false);
            });
    }
     private void OncCloseWarning()
    {
        warningPnl.gameObject.SetActive(false);
    }
    
    private void UpdateButtonVisibility()
    {
        // 確保GameDB.Bought有足夠的元素
        if (GameDB.BoughtTower.Count >= 4)
        {
            buy02.gameObject.SetActive(!GameDB.BoughtTower[0]);
            buy03.gameObject.SetActive(!GameDB.BoughtTower[1]);
            buy04.gameObject.SetActive(!GameDB.BoughtTower[2]);
            buy05.gameObject.SetActive(!GameDB.BoughtTower[3]);
        }
    }
    void Update()
    {
        // 當滑鼠左鍵點擊時
        if (Input.GetButtonDown("Fire1")&& !isPanelOpen)
        {
            if (pauseMenu.activeSelf)
            {
                return;
            }
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // 檢查點擊到的物件是否在tower列表中
                for (int i = 0; i < tower.Count; i++)
                {
                    if (hit.collider.gameObject == tower[i])
                    {
                        OpenPanel(i);
                        break;
                    }
                }
            }
        }
    }

    // 開啟面板
    private void OpenPanel(int index)
    {
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
        
        // 如果有其他面板開著，先關閉
        if (activePanel != null)
        {
            activePanel.SetActive(false);
        }

        // 開啟對應的面板
        towerPnl[index].SetActive(true);
        activePanel = towerPnl[index];
        
        isPanelOpen = true; 
    }

    // 關閉面板
    private void ClosePanel(int index)
    {
        GameDB.Audio.PlaySfx(closesfx);
        Time.timeScale = 1;
        if (GameDB.Audio._bgmAudioSource != null)
        {
            GameDB.Audio._bgmAudioSource.ignoreListenerPause = false;
        }
        towerPnl[index].SetActive(false);
        activePanel = null;
        isPanelOpen = false; 
    }
   

    private void OnBtnBuyItem2()
    {
        if (GameDB.money > 300 && !GameDB.BoughtTower[0])
        {
            buy02.gameObject.SetActive(false);
            GameDB.money -= 300;
            GameDB.Audio.PlaySfx(buysfx);
            GameDB.BoughtTower[0] = true;
            GameDB.Save();
            Debug.Log("你買了後水頭");
        }
        else
        {
            warningPnl.gameObject.SetActive(true);
            Debug.Log("你不夠300塊");
        }
    }

    private void OnBtnBuyItem3()
    {
        if (GameDB.money > 500 && !GameDB.BoughtTower[1])
        {
            buy03.gameObject.SetActive(false);
            GameDB.money -= 500;
            GameDB.BoughtTower[1] = true;
            GameDB.Audio.PlaySfx(buysfx);
            GameDB.Save();
            Debug.Log("你買了劉澳");
        }
        else
        {
            warningPnl.gameObject.SetActive(true);
            Debug.Log("你不夠300塊");
        }
    }

    private void OnBtnBuyItem4()
    {
        if (GameDB.money > 700 && !GameDB.BoughtTower[2])
        {
            buy04.gameObject.SetActive(false);
            GameDB.money -= 700;
            GameDB.BoughtTower[2] = true;
            GameDB.Audio.PlaySfx(buysfx);
            GameDB.Save();
            Debug.Log("你買了安崎");
        }
        else
        {
            warningPnl.gameObject.SetActive(true);
            Debug.Log("你不夠700塊");
        }
    }

    private void OnBtnBuyItem5()
    {
        if (GameDB.money > 1500 && !GameDB.BoughtTower[3])
        {
            buy05.gameObject.SetActive(false);
            GameDB.money -= 1500;
            GameDB.BoughtTower[3] = true;
            GameDB.Audio.PlaySfx(buysfx);
            GameDB.Save();
            Debug.Log("你買了塔后");
        }
        else
        {
            warningPnl.gameObject.SetActive(true);
            Debug.Log("你不夠1500塊");
        }
    }

    public void CloseCurrentPanel()
    {
        //if (activePanel == null || isClosingPanel) return;
        
        //isClosingPanel = true;
        //Time.timeScale = 1;
        //activePanel.SetActive(false);
        //activePanel = null;
        //isPanelOpen = false;
        
        // 添加延遲，防止立即觸發其他面板
        //StartCoroutine(ResetClosingFlag());
    }
    //private IEnumerator ResetClosingFlag()
    //{
        //yield return new WaitForSecondsRealtime(0.2f);
        //isClosingPanel = false;
    //}
}