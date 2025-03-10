using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    

   
    private void OnEnable()
    {
        _inputMaster = new InputMaster();
        _inputMaster.Enable();

        // 綁定 PauseMenu 鍵
       // _inputMaster.Menu.PauseMenu.performed += context => TogglePauseMenu(); //已經模擬ESC建
    }

    private void OnDisable()
    {
        // 取消綁定 PauseMenu 鍵
        _inputMaster.Menu.PauseMenu.performed -= context => TogglePauseMenu();
        _inputMaster.Disable();
    }
    void Start()
    {
        backmenuBtn.onClick.AddListener(BackMenuClick);
        closeBtn.onClick.AddListener(ClosesettingClick);
        settingBtn.onClick.AddListener(OnsettingClick);
        conBtn.onClick.AddListener(ConBtnClick);
        settingUI.SetActive(false);
        pausemenu.SetActive(false);
        pausemenu.SetActive(isShow);
    }

    private void BackMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ClosesettingClick()
    {
        settingUI.SetActive(false);
    }

    private void ConBtnClick()
    {
        pausemenu.SetActive(false);
        Time.timeScale =  1f;
    }

    private void OnsettingClick()
    {
        settingUI.SetActive(true);
    }

    void Update()
    {
        // 檢查 ESC 鍵是否被按下
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePauseMenu();
        }
    }
    // 切換暫停選單顯示
    void TogglePauseMenu()
    {
        Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        
        isShow = !isShow;
        pausemenu.SetActive(isShow);

        // 暫停或繼續遊戲時間
        Time.timeScale = isShow ? 0f : 1f;
    }
}

