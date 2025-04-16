using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EscMgr : MonoBehaviour
{
    public enum ESCPanelState
    {
        None,
        LionPanel,      // 風獅爺面板 (S3)
        TowerPanel,     // 風獅爺面板 (S1和Boss場景)
        ShopPanel,      // 商店面板 (S2)
        ProductInfo,    // 商品資訊面板 (S2)
        TutorialPanel,  // 教學面板 (所有場景)
        PausePanel,     // 暫停面板 (所有場景)
        SettingPanel    // 設定面板 (在暫停面板內)
    }

    // 使用堆疊來管理面板開啟順序
    private Stack<ESCPanelState> panelStateStack = new Stack<ESCPanelState>();

    // 當前活動面板狀態
    public ESCPanelState CurrentState => panelStateStack.Count > 0 ? panelStateStack.Peek() : ESCPanelState.None;

    // 面板引用
    
    public GameObject[] lionPanels;  // 風獅爺面板 (S3)
    public GameObject shopPanel;     // 商店面板 (S2)
    public GameObject[] productPanels; // 商品資訊面板 (S2)
    public GameObject tutorialPanel; // 教學面板 (所有場景)
    public GameObject pausePanel;    // 暫停面板 (所有場景)
    public GameObject settingPanel;  // 設定面板 (在暫停面板內)

    // 存放各面板控制器的引用
    private PauseMenu pauseMenuController;
    private TowerPnlMgr towerPanelManager; // 新增：S1和Boss場景的風獅爺面板管理器

    // 記錄當前活動中的面板索引
    private int currentActiveLionPanelIndex = -1;
    private int currentActiveProductPanelIndex = -1;

    // 各場景的管理器引用
    private S3Mgr s3Manager;

    // 單例模式
    public static EscMgr Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 清除所有面板狀態
        ClearAllPanelStates();
        
        // 重置活動面板索引
        currentActiveLionPanelIndex = -1;
        currentActiveProductPanelIndex = -1;
        
        // 尋找當前場景的管理器
        s3Manager = FindObjectOfType<S3Mgr>();
        towerPanelManager = FindObjectOfType<TowerPnlMgr>(); // 新增：尋找S1和Boss場景的面板管理器
        
        // 尋找暫停選單控制器
        pauseMenuController = FindObjectOfType<PauseMenu>();
        
        // 確保所有面板在場景載入時都是關閉的
        InitializePanels();
        
        Debug.Log($"Scene loaded: {scene.name}, Panel stack cleared.");
    }

    private void InitializePanels()
    {
        // 關閉所有風獅爺面板
        if (lionPanels != null)
        {
            foreach (var panel in lionPanels)
            {
                if (panel != null) panel.SetActive(false);
            }
        }
        
        // 關閉商店面板
        if (shopPanel != null) shopPanel.SetActive(false);
        
        // 關閉所有商品資訊面板
        if (productPanels != null)
        {
            foreach (var panel in productPanels)
            {
                if (panel != null) panel.SetActive(false);
            }
        }
        
        // 關閉教學面板
        if (tutorialPanel != null) tutorialPanel.SetActive(false);
        
        // 關閉暫停面板和設定面板
        if (pausePanel != null) pausePanel.SetActive(false);
        if (settingPanel != null) settingPanel.SetActive(false);
        
        // 確保S1和Boss場景的風獅爺面板關閉
        if (towerPanelManager != null && towerPanelManager.infoPanelHandler != null)
        {
            towerPanelManager.infoPanelHandler.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            HandleEscKey();
        }
    }

    // 處理 ESC 按鍵邏輯
    public void HandleEscKey()
    {
        // 如果沒有任何面板開啟，則開啟暫停面板（但只在沒有其他面板時）
        if (panelStateStack.Count == 0)
        {
            // 檢查是否可以開啟暫停面板
            if (CanOpenPausePanel())
            {
                OpenPausePanel();
                return;
            }
            else
            {
                // 如果不能開啟暫停面板，直接返回
                return;
            }
        }

        // 如果堆疊中有面板，則根據最上層的面板類型進行處理
        ESCPanelState currentState = panelStateStack.Peek();

        switch (currentState)
        {
            case ESCPanelState.SettingPanel:
                CloseSettingPanel();
                break;

            case ESCPanelState.PausePanel:
                if (settingPanel != null && settingPanel.activeSelf)
                {
                    CloseSettingPanel();
                }
                else
                {
                    ClosePausePanel();
                }
                break;

            case ESCPanelState.ProductInfo:
                CloseProductInfoPanel(currentActiveProductPanelIndex);
                break;

            case ESCPanelState.ShopPanel:
                // 檢查是否有商品資訊面板開啟
                bool anyProductPanelActive = false;
                if (productPanels != null)
                {
                    for (int i = 0; i < productPanels.Length; i++)
                    {
                        if (productPanels[i] != null && productPanels[i].activeSelf)
                        {
                            anyProductPanelActive = true;
                            CloseProductInfoPanel(i);
                            break;
                        }
                    }
                }
                
                if (!anyProductPanelActive)
                {
                    CloseShopPanel();
                }
                break;

            case ESCPanelState.LionPanel:
                CloseLionPanel(currentActiveLionPanelIndex);
                break;
                
            case ESCPanelState.TowerPanel: // 新增：處理S1和Boss場景的風獅爺面板
                CloseTowerPanel();
                break;

            case ESCPanelState.TutorialPanel:
                CloseTutorialPanel();
                break;
        }
    }

    // 檢查是否可以開啟暫停面板
    private bool CanOpenPausePanel()
    {
        // 根據需求，檢查場景中是否有其他面板開啟
        // 如果有其他邏輯需要排除暫停面板開啟的情況，可以在這裡添加
        return panelStateStack.Count == 0;
    }

    // 註冊面板開啟
    public void RegisterPanel(ESCPanelState state)
    {
        panelStateStack.Push(state);
        Debug.Log($"Panel registered: {state}. Stack count: {panelStateStack.Count}");
    }

    // 取消註冊面板
    public void UnregisterPanel(ESCPanelState state)
    {
        if (panelStateStack.Count > 0 && panelStateStack.Peek() == state)
        {
            panelStateStack.Pop();
            Debug.Log($"Panel unregistered: {state}. Stack count: {panelStateStack.Count}");
        }
        else
        {
            Debug.LogWarning($"Tried to unregister {state} but it's not at the top of the stack!");
        }
    }

    // 風獅爺面板相關方法
    public void OpenLionPanel(int index)
    {
        if (lionPanels != null && index >= 0 && index < lionPanels.Length)
        {
            lionPanels[index].SetActive(true);
            currentActiveLionPanelIndex = index;
            RegisterPanel(ESCPanelState.LionPanel);
        }
    }

    public void CloseLionPanel(int index)
    {
        // S3 場景特殊處理
        if (s3Manager != null)
        {
            //s3Manager.CloseCurrentPanel();
            UnregisterPanel(ESCPanelState.LionPanel);
            currentActiveLionPanelIndex = -1;
            return;
        }

        // 關閉特定的風獅爺面板
        if (lionPanels != null && index >= 0 && index < lionPanels.Length)
        {
            lionPanels[index].SetActive(false);
            UnregisterPanel(ESCPanelState.LionPanel);
            currentActiveLionPanelIndex = -1;
        }
    }

    public void CloseLionPanels()
    {
        CloseLionPanel(currentActiveLionPanelIndex);
    }
    
    // S1和Boss場景風獅爺面板相關方法 (新增)
    public void OpenTowerPanel()
    {
        if (towerPanelManager != null && towerPanelManager.infoPanelHandler != null)
        {
            // 這個方法是註冊，讓ESC可以關閉，但不是直接開啟
            // 實際開啟由TowerPnlMgr處理
            RegisterPanel(ESCPanelState.TowerPanel);
        }
    }

    public void CloseTowerPanel()
    {
        if (towerPanelManager != null && towerPanelManager.infoPanelHandler != null)
        {
            towerPanelManager.CloseinfoPanel();
            UnregisterPanel(ESCPanelState.TowerPanel);
        }
    }
    
    // 商店面板相關方法
    public void OpenShopPanel()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(true);
            RegisterPanel(ESCPanelState.ShopPanel);
        }
    }

    public void CloseShopPanel()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
            UnregisterPanel(ESCPanelState.ShopPanel);
        }
    }

    // 商品資訊面板相關方法
    public void OpenProductInfoPanel(int index)
    {
        if (productPanels != null && index >= 0 && index < productPanels.Length)
        {
            productPanels[index].SetActive(true);
            currentActiveProductPanelIndex = index;
            RegisterPanel(ESCPanelState.ProductInfo);
        }
    }

    public void CloseProductInfoPanel(int index)
    {
        if (productPanels != null && index >= 0 && index < productPanels.Length)
        {
            productPanels[index].SetActive(false);
            UnregisterPanel(ESCPanelState.ProductInfo);
            currentActiveProductPanelIndex = -1;
        }
    }

    public void CloseProductInfoPanels()
    {
        CloseProductInfoPanel(currentActiveProductPanelIndex);
    }

    // 教學面板相關方法
    public void OpenTutorialPanel()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
            RegisterPanel(ESCPanelState.TutorialPanel);
        }
    }

    public void CloseTutorialPanel()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
            UnregisterPanel(ESCPanelState.TutorialPanel);
        }
    }

    // 暫停面板相關方法
    public void OpenPausePanel()
    {
        if (pausePanel != null && CanOpenPausePanel())
        {
            pausePanel.SetActive(true);
            RegisterPanel(ESCPanelState.PausePanel);
            // 暫停遊戲（如果需要）
            Time.timeScale = 0f;
        }
    }

    public void ClosePausePanel()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
            UnregisterPanel(ESCPanelState.PausePanel);
            // 恢復遊戲時間
            Time.timeScale = 1f;
        }
    }

    // 設定面板相關方法
    public void OpenSettingPanel()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(true);
            RegisterPanel(ESCPanelState.SettingPanel);
        }
    }

    public void CloseSettingPanel()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(false);
            UnregisterPanel(ESCPanelState.SettingPanel);
        }
    }

    // 場景轉換時清除面板狀態
    public void ClearAllPanelStates()
    {
        panelStateStack.Clear();
        Time.timeScale = 1f; // 確保時間恢復到正常
        Debug.Log("All panel states cleared");
    }

    // 檢查面板陣列中是否有激活的面板
    private bool AnyPanelActive(GameObject[] panels)
    {
        if (panels == null) return false;
        
        foreach (var panel in panels)
        {
            if (panel != null && panel.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
    
    // 獲取當前面板堆疊的字符串表示（用於調試）
    public string GetPanelStackString()
    {
        if (panelStateStack.Count == 0) return "Empty";
        
        string result = "";
        foreach (var state in panelStateStack)
        {
            result += state.ToString() + " -> ";
        }
        return result.TrimEnd(' ', '-', '>');
    }
}