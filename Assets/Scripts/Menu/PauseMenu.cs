using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private InputActionReference menuInputActionReference; // 用於 Xbox 控制器的 Menu 鍵
    public GameObject pausemenu;
    public bool isShow;

    private void OnEnable()
    {
        menuInputActionReference.action.started += MenuPressed;
    }

    private void OnDisable()
    {
        menuInputActionReference.action.started -= MenuPressed;
    }

    void Start()
    {
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

    // 當 Menu 鍵被按下時觸發
    private void MenuPressed(InputAction.CallbackContext context)
    {
        TogglePauseMenu();
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

