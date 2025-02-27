using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeController : MonoBehaviour
{
   
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;
    
    
    private bool saveOnValueChanged = true;

    private void Start()
    {
        // 確保AudioMgr已經被初始化
        if (GameDB.Audio == null)
        {
            Debug.LogError("AudioMgr not initialized! Please make sure AudioMgr exists in the scene.");
            return;
        }

        // 初始化滑桿值為當前音量
        InitializeSliders();
        
        // 添加滑桿的事件監聽
        bgmVolumeSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    private void InitializeSliders()
    {
        // 從GameDB獲取當前設置的音量
        bgmVolumeSlider.value = GameDB.Config.bgmAudioVolume;
        sfxVolumeSlider.value = GameDB.Config.sfxAudioVolume;
    }

    public void OnBGMVolumeChanged(float volume)
    {
        // 設置BGM音量
        GameDB.Audio.SetBgmVolume(volume);
        
        // 如果設置為保存更改，則調用Save方法
        if (saveOnValueChanged)
        {
            SaveAudioSettings();
        }
    }

    public void OnSFXVolumeChanged(float volume)
    {
        // 設置SFX音量
        GameDB.Audio.SetSfxVolume(volume);
        
        // 播放測試音效以便用戶立即聽到效果
        if (volume > 0)
        {
            GameDB.Audio.PlaySubmit();
        }
        
        // 如果設置為保存更改，則調用Save方法
        if (saveOnValueChanged)
        {
            SaveAudioSettings();
        }
    }

    public void SaveAudioSettings()
    {
        // 保存音量設置
        PlayerPrefs.SetFloat("BGM_Volume", GameDB.Config.bgmAudioVolume);
        PlayerPrefs.SetFloat("SFX_Volume", GameDB.Config.sfxAudioVolume);
        PlayerPrefs.Save();
    }

    public void LoadAudioSettings()
    {
        // 從PlayerPrefs讀取音量設置
        GameDB.Config.bgmAudioVolume = PlayerPrefs.GetFloat("BGM_Volume", 0.6f);
        GameDB.Config.sfxAudioVolume = PlayerPrefs.GetFloat("SFX_Volume", 0.8f);
        
        // 更新滑桿和音量
        InitializeSliders();
        GameDB.Audio.SetBgmVolume(GameDB.Config.bgmAudioVolume);
        GameDB.Audio.SetSfxVolume(GameDB.Config.sfxAudioVolume);
    }
}
