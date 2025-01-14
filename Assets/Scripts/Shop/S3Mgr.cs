using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S3Mgr : MonoBehaviour
{
    public GameObject[] shopPanels; // 將所有購買面板拖到這個陣列中
    public AudioClip s3bgm;
    void Start()
    {
        // 初始化，隱藏所有購買面板
        foreach (GameObject panel in shopPanels)
        {
            panel.SetActive(false);
        }
        GameDB.Audio.PlayBgm(s3bgm);
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