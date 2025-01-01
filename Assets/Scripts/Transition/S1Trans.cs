using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S1Trans : MonoBehaviour
{
    public Image fadeImage; // 拖入全屏的 Image
    public float fadeDuration = 1f; // 淡入淡出的持續時間
     
    void Start()
    {
        FadeIn();
    }
    
    void Update()
    {
        
    }
    public void FadeIn()
    {
        fadeImage.color = new Color(0, 0, 0, 1); // 確保 Image 初始為黑色（完全不透明）
        fadeImage.DOFade(0, fadeDuration).SetEase(Ease.InOutQuad); // 淡入
    }
    public void FadeOutAndLoadScene()
    {
        fadeImage.color = new Color(0, 0, 0, 0); // 確保 Image 初始為透明
        fadeImage.DOFade(1, fadeDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            SceneManager.LoadScene("S1"); // 淡出完成後加載新場景
        });
    }
}
