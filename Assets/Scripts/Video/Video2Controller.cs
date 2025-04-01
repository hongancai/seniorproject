using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class Video2Controller : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    
    void Start()
    {
        // 確保有VideoPlayer組件
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
            if (videoPlayer == null)
            {
                videoPlayer = gameObject.AddComponent<VideoPlayer>();
            }
        }
        
        // 設置影片播放完成後的事件
        videoPlayer.loopPointReached += OnVideoEnd;
    }
    
    // 影片播放完成後的回調函數
    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("Boss");
    }
    
    private void OnDestroy()
    {
        // 清除事件監聽器，避免記憶體洩漏
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}
