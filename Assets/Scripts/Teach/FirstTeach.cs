using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstTeach : MonoBehaviour
{
    public AudioClip btnsfx;
    public GameObject teachPnl;
    public GameObject[] image;
    public Button rBtn; 
    public Button lBtn;
    
    public Button closeTeachBtn;
    
    void Start()
    {
        // 設定按鈕監聽
        if (rBtn != null) rBtn.onClick.AddListener(OnNextPageBtn);
        if (lBtn != null) lBtn.onClick.AddListener(OnPreviousPageBtn);
        if (closeTeachBtn != null) closeTeachBtn.onClick.AddListener(OnCloseTeachBtn);
        
        // 檢查是否已經關閉過教學面板
        if (!GameDB.hasClosedTeachPanel)
        {
            // 如果未關閉過，立即顯示教學面板
            teachPnl.SetActive(true);
            Time.timeScale = 0f;
            float currentVolume = GameDB.Audio._bgmAudioSource.volume;
            if (GameDB.Audio._bgmAudioSource != null)
            {
                if (!GameDB.Audio._bgmAudioSource.isPlaying)
                {
                    GameDB.Audio._bgmAudioSource.Play();
                }
                // 保持原有音量，不降低
                GameDB.Audio._bgmAudioSource.volume = currentVolume;
            }
            // 確保只有第一頁是顯示的
            for (int i = 0; i < image.Length; i++)
            {
                image[i].SetActive(i == 0);
            }
        }
        else
        {
            // 如果已關閉過，隱藏教學面板
            teachPnl.SetActive(false);
        }
    }
    
    public void OnCloseTeachBtn()
    {
        GameDB.Audio.PlaySfx(btnsfx);
        teachPnl.SetActive(false);
        Time.timeScale = 1f;
        
        // 設置已經關閉教學面板的狀態並保存
        GameDB.hasClosedTeachPanel = true;
        GameDB.Save();
    }

    private void OnPreviousPageBtn()
    {
        GameDB.Audio.PlaySfx(btnsfx);
        for (int i = 0; i < image.Length; i++)
        {
            if (image[i].activeSelf)
            {
                image[i].SetActive(false);
                
                // 防止超出索引範圍
                if (i > 0)
                {
                    image[i - 1].SetActive(true);
                }
                return;
            }
        }
    }

    private void OnNextPageBtn()
    {
        GameDB.Audio.PlaySfx(btnsfx);
        for (int i = 0; i < image.Length; i++)
        {
            if (image[i].activeSelf)
            {
                image[i].SetActive(false);
                
                // 防止超出索引範圍
                if (i < image.Length - 1)
                {
                    image[i + 1].SetActive(true);
                }
                return;
            }
        }
    }
    
    void Update()
    {
        if (teachPnl != null && teachPnl.activeSelf)
        {
            // 最後一頁時隱藏右按鈕
            if (image.Length > 0 && image[image.Length - 1].activeSelf)
            {
                rBtn.gameObject.SetActive(false);
            }
            else
            {
                rBtn.gameObject.SetActive(true);
            }
            
            // 第一頁時隱藏左按鈕
            if (image.Length > 0 && image[0].activeSelf)
            {
                lBtn.gameObject.SetActive(false);
            }
            else
            {
                lBtn.gameObject.SetActive(true);
            }
        }
    }
}