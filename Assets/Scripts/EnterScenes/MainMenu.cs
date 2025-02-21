using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

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

    private bool bgmfadeout = false;
    void Start()
    {
        GameDB.Audio.PlayBgm(mainmenubgm);
        settingMenu.SetActive(false);
        btnsettingclose.onClick.AddListener(OnSettingClickfalse);
        btnsetting.onClick.AddListener(OnSettingClick);
        btnstart.onClick.AddListener(OnStartClick);
        //btnc0n.onClick.AddListener(OnConClick);
        btnexit.onClick.AddListener(OnExitClick);
        blackScreen.gameObject.SetActive(false);
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); // 確保主選單顯示時暫停介面是關閉的
        }
    }
    
    private void OnSettingClickfalse()
    {
        GameDB.Audio.PlaySfx(btnsfx);
        settingMenu.SetActive(false);
    }

    private void OnSettingClick()
    {
        GameDB.Audio.PlaySfx(btnsfx);
        settingMenu.SetActive(true);
    }
    private void OnStartClick()
    {
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
        GameDB.Audio.PlaySfx(btnsfx);
        Application.Quit();
        Debug.Log("Exit");
    }

    void Update()
    {
        if (bgmfadeout == true)
        {
            GameDB.Audio._bgmAudioSource.volume -= Time.deltaTime;
        }
    }
   
}