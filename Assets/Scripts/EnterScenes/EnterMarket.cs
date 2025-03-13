using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class EnterMarket : MonoBehaviour
{
    public AudioClip transsfx;
    public Image blackScreen;
    
    private bool bgmfadeout = false;
    void Start()
    {
        
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
        if (collision.gameObject.CompareTag("Player"))
        {
            DisablePlayerMovement(collision.gameObject);
            GameDB.Audio.PlaySfx(transsfx);
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
                SceneManager.LoadScene("Market");
            });
        }
    }

    private void DisablePlayerMovement(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }
    }
}
