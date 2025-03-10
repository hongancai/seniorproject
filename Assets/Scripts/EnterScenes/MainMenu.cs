using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public AudioClip mainmenubgm;
    public AudioClip startsfx;
    public AudioClip btnsfx;
    public Button btnsetting;
    public Button btnsettingclose;
    public Button btnstart;
    //public Button btncon;
    public Button btnexit;
    public GameObject pauseMenu;
    public GameObject settingMenu;
    public Image blackScreen;

    // 添加輸入系統資產引用
    public InputActionAsset inputActions;

    // 添加按鈕正常與懸停圖片
    [Header("按鈕圖片設定")]
    public Sprite btnStartNormal;
    public Sprite btnStartHover;
    //public Sprite btnConNormal;
    //public Sprite btnConHover;
    public Sprite btnExitNormal;
    public Sprite btnExitHover;

    // 目前選中的按鈕
    private Button currentSelectedButton;
    private bool isUsingGamepad = false;
    private PlayerInput playerInput;
    private InputAction navigateAction;
    private InputAction submitAction;
    private InputAction cancelAction; // 新增取消動作

    private bool bgmfadeout = false;
    private bool isStarting = false;

    void Start()
    {
        GameDB.Audio.PlayBgm(mainmenubgm);
        settingMenu.SetActive(false);
        btnsettingclose.onClick.AddListener(OnSettingClickfalse);
        btnsetting.onClick.AddListener(OnSettingClick);
        btnstart.onClick.AddListener(OnStartClick);
        //btncon.onClick.AddListener(OnConClick);
        btnexit.onClick.AddListener(OnExitClick);
        blackScreen.gameObject.SetActive(false);
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); // 確保主選單顯示時暫停介面是關閉的
        }

        // 初始化InputSystem
        SetupInputSystem();

        // 為按鈕添加滑鼠懸停和離開事件
        SetupButtonEvents();

        // 設置初始選中按鈕
        EventSystem.current.SetSelectedGameObject(btnstart.gameObject);
        UpdateButtonHighlight(btnstart);
    }

    private void SetupInputSystem()
    {
        // 檢查是否已指定輸入動作資產
        if (inputActions == null)
        {
            Debug.LogError("請在Inspector中指定Input Master資產！");
            return;
        }

        // 建立PlayerInput元件
        playerInput = gameObject.AddComponent<PlayerInput>();
        playerInput.actions = inputActions;
        playerInput.defaultActionMap = "UI"; // 確保您的Input Master中有名為"UI"的Action Map

        // 獲取導航和確認動作
        navigateAction = playerInput.actions.FindAction("Navigate");
        submitAction = playerInput.actions.FindAction("Submit");
        cancelAction = playerInput.actions.FindAction("Cancel"); // 獲取取消動作

        // 檢查動作是否存在
        if (navigateAction == null)
            Debug.LogError("找不到Navigate動作，請確認Input Master中存在此動作");
        if (submitAction == null)
            Debug.LogError("找不到Submit動作，請確認Input Master中存在此動作");
        if (cancelAction == null)
            Debug.LogError("找不到Cancel動作，請確認Input Master中存在此動作");

        // 註冊事件
        if (navigateAction != null)
            navigateAction.performed += OnNavigate;
        if (submitAction != null)
            submitAction.performed += OnSubmit;
        if (cancelAction != null)
            cancelAction.performed += OnCancel; 

        // 檢測控制方式變更
        playerInput.onControlsChanged += OnControlsChanged;
    }

    private void OnNavigate(InputAction.CallbackContext context)
    {
        // 當有導航輸入時，標記正在使用手把
        isUsingGamepad = true;
    }

    private void OnSubmit(InputAction.CallbackContext context)
    {
        // 模擬點擊當前選中的按鈕
        if (currentSelectedButton != null)
        {
            currentSelectedButton.onClick.Invoke();
        }
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
            return;
        // 如果設定選單正在顯示，關閉它
        if (settingMenu.activeSelf)
        {
            OnSettingClickfalse();
            context.ReadValue<float>();
        }
    }

    private void OnControlsChanged(PlayerInput input)
    {
        // 當控制方式變更時更新標誌
        isUsingGamepad = input.currentControlScheme == "Gamepad";
    }

    private void SetupButtonEvents()
    {
        // 為開始遊戲按鈕添加事件
        AddPointerEvents(btnstart, btnStartNormal, btnStartHover);
        
        // 為繼續遊戲按鈕添加事件 (如果啟用的話)
        //if (btncon != null)
        //    AddPointerEvents(btncon, btnConNormal, btnConHover);
        
        // 為離開遊戲按鈕添加事件
        AddPointerEvents(btnexit, btnExitNormal, btnExitHover);

        // 設定按鈕不需要換圖，所以不添加圖片切換事件
    }

    private void AddPointerEvents(Button button, Sprite normalSprite, Sprite hoverSprite)
    {
        // 獲取按鈕的事件觸發器
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        
        if (trigger == null)
            trigger = button.gameObject.AddComponent<EventTrigger>();
        
        // 清除已有事件
        trigger.triggers.Clear();
        
        // 添加滑鼠懸停事件
        EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
        pointerEnter.eventID = EventTriggerType.PointerEnter;
        pointerEnter.callback.AddListener((data) => {
            button.image.sprite = hoverSprite;
            UpdateButtonHighlight(button);
            isUsingGamepad = false;
        });
        trigger.triggers.Add(pointerEnter);
        
        // 添加滑鼠離開事件
        EventTrigger.Entry pointerExit = new EventTrigger.Entry();
        pointerExit.eventID = EventTriggerType.PointerExit;
        pointerExit.callback.AddListener((data) => {
            if (!isUsingGamepad || currentSelectedButton != button)
                button.image.sprite = normalSprite;
        });
        trigger.triggers.Add(pointerExit);

        // 添加選取事件
        EventTrigger.Entry select = new EventTrigger.Entry();
        select.eventID = EventTriggerType.Select;
        select.callback.AddListener((data) => {
            UpdateButtonHighlight(button);
        });
        trigger.triggers.Add(select);

        // 添加取消選取事件
        EventTrigger.Entry deselect = new EventTrigger.Entry();
        deselect.eventID = EventTriggerType.Deselect;
        deselect.callback.AddListener((data) => {
            if (!isUsingGamepad || button != currentSelectedButton)
                button.image.sprite = normalSprite;
        });
        trigger.triggers.Add(deselect);

        // 設置初始圖片
        button.image.sprite = normalSprite;
    }

    private void UpdateButtonHighlight(Button button)
    {
        // 更新當前選中的按鈕
        currentSelectedButton = button;
        
        // 更新所有按鈕狀態
        UpdateButtonState(btnstart, btnStartNormal, btnStartHover);
        //if (btncon != null)
        //    UpdateButtonState(btncon, btnConNormal, btnConHover);
        UpdateButtonState(btnexit, btnExitNormal, btnExitHover);
    }

    private void UpdateButtonState(Button button, Sprite normalSprite, Sprite hoverSprite)
    {
        // 如果是當前選中的按鈕，使用懸停圖片，否則使用正常圖片
        button.image.sprite = (button == currentSelectedButton) ? hoverSprite : normalSprite;
    }

    void Update()
    {
        if (bgmfadeout == true)
        {
            GameDB.Audio._bgmAudioSource.volume -= Time.deltaTime;
        }

        // 檢查是否有當前選中的UI元素，如果沒有則設置為btnstart
        if (EventSystem.current.currentSelectedGameObject == null && isUsingGamepad)
        {
            EventSystem.current.SetSelectedGameObject(btnstart.gameObject);
            UpdateButtonHighlight(btnstart);
        }
        
        // 如果打開了設定選單，確保有一個按鈕被選中
        if (settingMenu.activeSelf && EventSystem.current.currentSelectedGameObject == null && isUsingGamepad)
        {
            EventSystem.current.SetSelectedGameObject(btnsettingclose.gameObject);
        }
        if (settingMenu.activeSelf && Gamepad.current != null && Gamepad.current.bButton.wasPressedThisFrame)
        {
            OnSettingClickfalse();
        }
    }
    
    private void OnSettingClickfalse()
    {
        GameDB.Audio.PlaySfx(btnsfx);
        settingMenu.SetActive(false);
        
        // 當關閉設定後，重新選擇主選單中的按鈕
        EventSystem.current.SetSelectedGameObject(btnstart.gameObject);
        UpdateButtonHighlight(btnstart);
    }

    private void OnSettingClick()
    {
        isStarting = true;
        GameDB.Audio.PlaySfx(btnsfx);
        settingMenu.SetActive(true);
        
        // 當開啟設定後，選擇設定關閉按鈕
        if (isUsingGamepad)
        {
            EventSystem.current.SetSelectedGameObject(btnsettingclose.gameObject);
        }
    }
    
    private void OnStartClick()
    {
        if (isStarting )
        {
            return;
        }

        isStarting = true;
        GameDB.Audio.PlaySfx(startsfx);
        blackScreen.color = new Color(0,0,0,0);
        blackScreen.gameObject.SetActive(true);
        
        // 建立序列動畫
        Sequence sequence = DOTween.Sequence();
        
        // 黑幕從透明慢慢變成不透明（淡入）
        sequence.Append(blackScreen.DOColor(Color.black, 3f).SetEase(Ease.InOutSine));
        sequence.AppendInterval(2f);
        bgmfadeout = true;
        // 淡入完成後切換場景
        sequence.OnComplete(() => 
        {
            SceneManager.LoadScene("S1");
        });
        
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); // 在開始遊戲時關閉暫停介面
        }
        Time.timeScale = 1f; // 確保遊戲時間正常運行
    }
    
    //private void OnConClick()
    //{
        //GameDB.Audio.PlaySfx(startsfx);
        //GameDB.Load();
    //}

    private void OnExitClick()
    {
        isStarting = true;
        GameDB.Audio.PlaySfx(btnsfx);
        Application.Quit();
        Debug.Log("Exit");
    }

    private void OnDestroy()
    {
        // 取消註冊事件
        if (navigateAction != null)
            navigateAction.performed -= OnNavigate;
        
        if (submitAction != null)
            submitAction.performed -= OnSubmit;
            
        if (cancelAction != null)
            cancelAction.performed -= OnCancel;
    }
}