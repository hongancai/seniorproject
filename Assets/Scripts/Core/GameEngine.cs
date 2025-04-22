using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    static GameEngine()
    {
        Instance = new GameEngine();
    }

    public static GameEngine Instance;
    

    public void Initialization()
    {
        Debug.Log("Game Engine Initialization ... ");

        if (GameDB.Audio == null)
        {

            // 從resource load 抓 設定
            AudioData audioData = Resources.Load("AudioData") as AudioData;

            // 新增 Audio 引擎
            GameObject temp = Instantiate(new GameObject());
            AudioMgr audioMgr = temp.AddComponent<AudioMgr>();
            audioMgr.gameObject.name = "AudioManager";
            
            // 設定引擎 並啟動
            audioMgr.audioData = audioData;
        }
    }
}
