using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip mainmenubgm;
    public Button btnsetting;
    public Button btnsettingclose;
    public Button btnstart;
    public Button btnexit;
    public GameObject pauseMenu;
    public GameObject settingMenu;
    void Start()
    {
        GameDB.Audio.PlayBgm(mainmenubgm);
        settingMenu.SetActive(false);
        btnsettingclose.onClick.AddListener(OnSettingClickfalse);
        btnsetting.onClick.AddListener(OnSettingClick);
        btnstart.onClick.AddListener(OnStartClick);
        btnexit.onClick.AddListener(OnExitClick);
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); // 確保主選單顯示時暫停介面是關閉的
        }
    }

    private void OnSettingClickfalse()
    {
        settingMenu.SetActive(false);
    }

    private void OnSettingClick()
    {
        settingMenu.SetActive(true);
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

    void Update()
    {
        
    }
   
}