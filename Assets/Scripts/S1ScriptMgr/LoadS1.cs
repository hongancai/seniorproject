using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadS1 : MonoBehaviour
{
    public Image blackScreen;
    void Start()
    {
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
        
    }
}
