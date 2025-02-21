using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class S1Mgr : MonoBehaviour
{
    
    public AudioClip s1bgm;
    public Image blackScreen;
    public List<GameObject> buyedItems;
    
    void Start()
    {
        GameDB.Audio.PlayBgm(s1bgm);
        blackScreen.gameObject.SetActive(true);
        blackScreen.color = new Color(0, 0, 0, 1);
       
        // 開始淡出黑幕
        blackScreen.DOFade(0f, 2f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => 
            {
                blackScreen.gameObject.SetActive(false);
            });
        GameDB.Load();
        UpdateDisplay();
    }

    void Update()
    {
        if (GameDB.Audio._bgmAudioSource.volume <=0.6f)
        {
            GameDB.Audio._bgmAudioSource.volume += Time.deltaTime;
        }   
    }
    private void UpdateDisplay()
    {
        // 根據 GameDB.Bought 的狀態更新物品顯示
        for (int i = 0; i < GameDB.Bought.Count; i++)
        {
            // 假設你有一個存放已購買物品的GameObject列表
            if (buyedItems != null && i < buyedItems.Count)
            {
                buyedItems[i].SetActive(GameDB.Bought[i]);
            }
        }
    }
}
