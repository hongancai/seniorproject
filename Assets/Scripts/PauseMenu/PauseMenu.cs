using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    private InputMaster _inputMaster;
    public GameObject pausemenu;
    public bool isShow;
    
    private void OnEnable()
    {
        _inputMaster = new InputMaster();
        _inputMaster.Enable();

        // 綁定 PauseMenu 鍵
        _inputMaster.Menu.PauseMenu.performed += context => TogglePauseMenu();
    }

    private void OnDisable()
    {
        // 取消綁定 PauseMenu 鍵
        _inputMaster.Menu.PauseMenu.performed -= context => TogglePauseMenu();
        _inputMaster.Disable();
    }
    void Start()
    {
        pausemenu.SetActive(false);
        pausemenu.SetActive(isShow);
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
        isShow = !isShow;
        pausemenu.SetActive(isShow);

        // 暫停或繼續遊戲時間
        Time.timeScale = isShow ? 0f : 1f;
    }
}

