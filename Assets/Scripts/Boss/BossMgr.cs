using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BossMgr : MonoBehaviour
{
    public AudioClip bossbgm;
    public Image blackScreen;
    public List<GameObject> buyedItems;
    public List<GameObject> buyTowers;
    
    void Start()
    {
        GameDB.Audio.PlayBgm(bossbgm);
        blackScreen.gameObject.SetActive(true);
        blackScreen.color = new Color(0, 0, 0, 1);

        // 開始淡出黑幕
        blackScreen.DOFade(0f, 2f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => { blackScreen.gameObject.SetActive(false); });
        GameDB.Load();
        UpdateDisplay();
    }

    
    void Update()
    {
        
    }
    private void UpdateDisplay()
    {
        GameDB.Load();
        
        // 根據 GameDB.Bought 的狀態更新物品顯示
        for (int i = 0; i < GameDB.Bought.Count; i++)
        {
            // 假設你有一個存放已購買物品的GameObject列表
            if (buyedItems != null && i < buyedItems.Count)
            {
                buyedItems[i].SetActive(GameDB.Bought[i]);
            }
        }

        for (int i = 0; i < GameDB.BoughtTower.Count; i++)
        {
            // 假設你有一個存放已購買物品的GameObject列表
            if (buyTowers != null && i < buyTowers.Count)
            {
                buyTowers[i].SetActive(GameDB.BoughtTower[i]);
            }
        }
    }
}
