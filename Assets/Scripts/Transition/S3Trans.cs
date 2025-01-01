using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S3Trans : MonoBehaviour
{
    public static S3Trans Instance;
    public Image fadeImage; // 拖入全屏的 Image
    public float fadeDuration = 1f; // 淡入淡出的持續時間
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保持跨場景存在
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FadeIn(); // 當場景加載時執行淡入
    }

    public void FadeIn()
    {
        fadeImage.color = new Color(0, 0, 0, 1); // 設定初始為黑色
        fadeImage.DOFade(0, fadeDuration).SetEase(Ease.InOutQuad); // 淡入動畫
    }

    public void FadeOutAndLoadScene()
    {
        fadeImage.color = new Color(0, 0, 0, 0); // 確保初始為透明
        fadeImage.DOFade(1, fadeDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            SceneManager.LoadScene("S3"); // 加載新場景
        });
    }
}
