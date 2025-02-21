using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class S3Mgr : MonoBehaviour
{
    public GameObject[] lion;
    public GameObject[] shopPanels; 
    public AudioClip s3bgm;
    public Image blackScreen;
    void Start()
    {
        GameDB.Audio.PlayBgm(s3bgm);
        // 初始化，隱藏所有購買面板
        foreach (GameObject panel in shopPanels)
        {
            panel.SetActive(false);
        }
        blackScreen.gameObject.SetActive(true);
        blackScreen.color = new Color(0, 0, 0, 1);
       
        // 開始淡出黑幕
        blackScreen.DOFade(0f, 2f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => 
            {
                blackScreen.gameObject.SetActive(false);
            });
    }
    void Update()
    {
        if (GameDB.Audio._bgmAudioSource.volume <=0.6f)
        {
            GameDB.Audio._bgmAudioSource.volume += Time.deltaTime;
        }   
    }
    public void ShowPanel(int index)
    {
        // 隱藏所有面板
        foreach (GameObject panel in shopPanels)
        {
            panel.SetActive(false);
        }

        // 顯示指定的面板
        if (index >= 0 && index < shopPanels.Length)
        {
            shopPanels[index].SetActive(true);
        }
    }

    public void CloseAllPanels()
    {
        // 隱藏所有面板
        foreach (GameObject panel in shopPanels)
        {
            panel.SetActive(false);
        }
    }
}