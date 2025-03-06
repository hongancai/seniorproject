using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMgr : MonoBehaviour
{
    public AudioClip openshopsfx;
    public AudioClip btnsfx;
    public GameObject shopPnl;
    public GameObject pauseMenu;
    public Button closeshoBtn;

    void Start()
    {
        shopPnl.gameObject.SetActive(false);
        closeshoBtn.onClick.AddListener(OnBtnClose);
    }

    private void OnBtnClose()
    {
        Time.timeScale = 1f;
        shopPnl.gameObject.SetActive(false);
        GameDB.Audio.PlaySfx(btnsfx);
    }


    void Update()
    {
        if (shopPnl.activeSelf || pauseMenu.activeSelf)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<ShopTag>() != null)
                {
                    Time.timeScale = 0;
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
                    GameDB.Audio.PlaySfx(openshopsfx);
                    shopPnl.gameObject.SetActive(true);
                    Debug.Log("測試開商店");
                }
                
            }
        }
    }
    
}