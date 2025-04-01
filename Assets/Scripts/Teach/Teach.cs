using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teach : MonoBehaviour
{
    public AudioClip btnsfx;
    public GameObject teachPnl;
    public GameObject[] image;
    public Button rBtn; 
    public Button lBtn;
    public Button openTeachBtn;
    public Button closeTeachBtn;
    //public EscMgr escManager;
    
    void Start()
    {
        teachPnl.gameObject.SetActive(false);
        rBtn.onClick.AddListener(OnNextPageBtn);
        lBtn.onClick.AddListener(OnPreviousPageBtn);
        openTeachBtn.onClick.AddListener(OnOpenTeachtn);
        closeTeachBtn.onClick.AddListener(OnCloseTeachBtn);
    }
    
    public void OnOpenTeachtn()
    {
        //EscMgr.Instance.OpenTutorialPanel();
        GameDB.Audio.PlaySfx(btnsfx);
        teachPnl.gameObject.SetActive(true);
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
    }
    public void OnCloseTeachBtn()
    {
        //EscMgr.Instance.CloseTutorialPanel();
        GameDB.Audio.PlaySfx(btnsfx);
        teachPnl.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnPreviousPageBtn()
    {
        GameDB.Audio.PlaySfx(btnsfx);
        for (int i = 0; i < image.Length; i++)
        {
            if (image[i].activeSelf)
            {
                image[i].SetActive(false);
                image[i - 1].SetActive(true);
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
                image[i + 1].SetActive(true);
                return;
            }
        }
    }
    
    void Update()
    {
        if (image[3].activeSelf)
        {
            rBtn.gameObject.SetActive(false);
        }
        else
        {
            rBtn.gameObject.SetActive(true);
        }
        if (image[0].activeSelf)
        {
            lBtn.gameObject.SetActive(false);
        }
        else
        {
            lBtn.gameObject.SetActive(true);
        }
    }
}
