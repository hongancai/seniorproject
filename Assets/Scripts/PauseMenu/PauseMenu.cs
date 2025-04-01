using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{
    private InputMaster _inputMaster;
    public GameObject pausemenu;
    public GameObject settingUI;
    public bool isShow;
    
    public Button settingBtn;
    public Button closeBtn;
    public Button backmenuBtn;
    public Button conBtn;
    
    
    
    // 增加按鈕圖片切換
    public Sprite[] continueBtnSprites; // 0: 正常, 1: 懸停
    public Sprite[] settingBtnSprites;  // 0: 正常, 1: 懸停
    public Sprite[] backBtnSprites;     // 0: 正常, 1: 懸停

    public AudioClip btnsfx;
    
    // 控制器導航變數
    private int currentButtonIndex = 0;
    private Button[] menuButtons;
    private bool isInputLocked = false;
    private float inputCooldown = 0.2f;
    private float lastInputTime = 0f;

   
    private void OnEnable()
    {
        _inputMaster = new InputMaster();
        _inputMaster.Enable();

        // 綁定 PauseMenu 鍵
       // _inputMaster.Menu.PauseMenu.performed += context => TogglePauseMenu(); //已經模擬ESC建
       
       _inputMaster.Menu.Navigate.performed += context => NavigateMenu(context.ReadValue<Vector2>());
       _inputMaster.Menu.Confirm.performed += context => ConfirmSelection();
       _inputMaster.Menu.Back.performed += context => BackAction();
    }

    private void OnDisable()
    {
        // 取消綁定 PauseMenu 鍵
        _inputMaster.Menu.PauseMenu.performed -= context => TogglePauseMenu();
        _inputMaster.Menu.Navigate.performed -= context => NavigateMenu(context.ReadValue<Vector2>());
        _inputMaster.Menu.Confirm.performed -= context => ConfirmSelection();
        _inputMaster.Menu.Back.performed -= context => BackAction();
        _inputMaster.Disable();
    }
    void Start()
    {
        backmenuBtn.onClick.AddListener(BackMenuClick);
        closeBtn.onClick.AddListener(ClosesettingClick);
        settingBtn.onClick.AddListener(OnsettingClick);
        conBtn.onClick.AddListener(ConBtnClick);
        
        // 設置按鈕陣列用於導航
        menuButtons = new Button[] { conBtn, settingBtn, backmenuBtn };
        
        // 添加懸停事件
        SetupButtonHoverEvents();
        
        settingUI.SetActive(false);
        pausemenu.SetActive(false);
        pausemenu.SetActive(isShow);
    }
    
    private void SetupButtonHoverEvents()
    {
        // 繼續按鈕懸停事件
        EventTrigger conTrigger = conBtn.gameObject.GetComponent<EventTrigger>() ?? conBtn.gameObject.AddComponent<EventTrigger>();
        SetupHoverEvents(conTrigger, conBtn.GetComponent<Image>(), continueBtnSprites);
        
        // 設定按鈕懸停事件
        EventTrigger settingTrigger = settingBtn.gameObject.GetComponent<EventTrigger>() ?? settingBtn.gameObject.AddComponent<EventTrigger>();
        SetupHoverEvents(settingTrigger, settingBtn.GetComponent<Image>(), settingBtnSprites);
        
        // 返回按鈕懸停事件
        EventTrigger backTrigger = backmenuBtn.gameObject.GetComponent<EventTrigger>() ?? backmenuBtn.gameObject.AddComponent<EventTrigger>();
        SetupHoverEvents(backTrigger, backmenuBtn.GetComponent<Image>(), backBtnSprites);
    }
    
    private void SetupHoverEvents(EventTrigger trigger, Image buttonImage, Sprite[] sprites)
    {
        // 添加懸停進入事件
        EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
        pointerEnter.eventID = EventTriggerType.PointerEnter;
        pointerEnter.callback.AddListener((data) => {
            buttonImage.sprite = sprites[1]; // 懸停圖片
        });
        trigger.triggers.Add(pointerEnter);
        
        // 添加懸停退出事件
        EventTrigger.Entry pointerExit = new EventTrigger.Entry();
        pointerExit.eventID = EventTriggerType.PointerExit;
        pointerExit.callback.AddListener((data) => {
            buttonImage.sprite = sprites[0]; // 正常圖片
        });
        trigger.triggers.Add(pointerExit);
    }

    private void BackMenuClick()
    {
        SaveGameData();
        SceneManager.LoadScene("MainMenu");
    }
    private void SaveGameData()
    {
        // 儲存目前場景名稱
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        
        // 儲存遊戲資料
        GameDB.Save();
    }

    private void ClosesettingClick()
    {
        GameDB.Audio.PlaySfx(btnsfx);
        settingUI.SetActive(false);
    }

    private void ConBtnClick()
    {
        GameDB.Audio.PlaySfx(btnsfx);
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnsettingClick()
    {
        GameDB.Audio.PlaySfx(btnsfx);
        settingUI.SetActive(true);
        pausemenu.SetActive(true);
    }
    
    // 手把導航功能
    private void NavigateMenu(Vector2 direction)
    {
        if (!isShow || Time.unscaledTime - lastInputTime < inputCooldown)
            return;
            
        lastInputTime = Time.unscaledTime;
        
        // 重置所有按鈕圖片
        menuButtons[currentButtonIndex].GetComponent<Image>().sprite = 
            currentButtonIndex == 0 ? continueBtnSprites[0] : 
            currentButtonIndex == 1 ? settingBtnSprites[0] : 
            backBtnSprites[0];
        
        // 垂直導航
        if (direction.y > 0.5f) // 上
        {
            currentButtonIndex = (currentButtonIndex + menuButtons.Length - 1) % menuButtons.Length;
        }
        else if (direction.y < -0.5f) // 下
        {
            currentButtonIndex = (currentButtonIndex + 1) % menuButtons.Length;
        }
        
        // 視覺反饋 - 將當前選中的按鈕設為懸停狀態
        menuButtons[currentButtonIndex].GetComponent<Image>().sprite = 
            currentButtonIndex == 0 ? continueBtnSprites[1] : 
            currentButtonIndex == 1 ? settingBtnSprites[1] : 
            backBtnSprites[1];
        
        // 設置 EventSystem 當前選中的遊戲物件
        EventSystem.current.SetSelectedGameObject(menuButtons[currentButtonIndex].gameObject);
    }
    
    // A鍵確認選擇
    private void ConfirmSelection()
    {
        if (!isShow || isInputLocked)
            return;
            
        // 防止重複點擊
        StartCoroutine(LockInput());
        
        // 執行點擊事件
        menuButtons[currentButtonIndex].onClick.Invoke();
    }
    
    // B鍵返回動作
    private void BackAction()
    {
        if (isInputLocked)
            return;
            
        StartCoroutine(LockInput());
        
        if (settingUI.activeSelf)
        {
            // 如果設定面板打開，關閉它
            ClosesettingClick();
        }
        else if (isShow)
        {
            // 如果暫停菜單打開，關閉它
            TogglePauseMenu();
        }
    }
    
    // 防止重複點擊的協程
    private IEnumerator LockInput()
    {
        isInputLocked = true;
        yield return new WaitForSecondsRealtime(inputCooldown);
        isInputLocked = false;
    }

    void Update()
    {
        // 檢查 ESC 鍵是否被按下
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePauseMenu();
        }
    }
    
    // 切換暫停選單顯示
    void TogglePauseMenu()
    {
        isShow = !isShow;
        pausemenu.SetActive(isShow);

        // 暫停或繼續遊戲時間
        Time.timeScale = isShow ? 0f : 1f;
        
        // 如果打開暫停菜單，選中第一個按鈕
        if (isShow)
        {
            currentButtonIndex = 0;
            NavigateMenu(Vector2.zero); // 刷新導航狀態
        }
        
    }
}

