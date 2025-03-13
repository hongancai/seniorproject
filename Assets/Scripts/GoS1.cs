using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoS1 : MonoBehaviour
{
    public AudioClip transsfx;
    public Image blackScreen;
    public string sourceScene;
    
    private bool bgmfadeout = false;
    void Start()
    {
        if (string.IsNullOrEmpty(sourceScene))
        {
            sourceScene = SceneManager.GetActiveScene().name;
        }
    }
    
    void Update()
    {
        if (bgmfadeout == true)
        {
            GameDB.Audio._bgmAudioSource.volume -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameDB.Audio.PlaySfx(transsfx);
        blackScreen.color = new Color(0,0,0,0);
        blackScreen.gameObject.SetActive(true);
        
        PlayerPrefs.SetString("LastScene", sourceScene);
        PlayerPrefs.Save();
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
    }
}