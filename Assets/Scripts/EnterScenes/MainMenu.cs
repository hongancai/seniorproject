using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip mainmenubgm;
    public Button btnstart;
    public Button btnexit;
    public GameObject pauseMenu;
    public GameObject settingMenu;
    void Start()
    {
        
        btnstart.onClick.AddListener(OnStartClick);
        btnexit.onClick.AddListener(OnExitClick);
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); // 確保主選單顯示時暫停介面是關閉的
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnStartClick()
    {
        
        SceneManager.LoadScene("S1");
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); // 在開始遊戲時關閉暫停介面
        }

        Time.timeScale = 1f; // 確保遊戲時間正常運行
        SceneManager.LoadScene("S1");
    }
    private void OnExitClick()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}