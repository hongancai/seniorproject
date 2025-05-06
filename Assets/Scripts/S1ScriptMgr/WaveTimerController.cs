using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveTimerController : MonoBehaviour
{
    [Header("UI 元素")]
    public Image timerFillImage;           // 倒數計時器的填充圖片
    public TextMeshProUGUI waveNumberText; // 波數文字 (使用 TextMeshPro)
    
    [Header("計時器設定")]
    public float timeBetweenWaves = 25f;  // 波次之間的時間間隔，與 MonsterSpawner 的波次間隔相同
    
    private MonsterSpawner monsterSpawner; // 怪物生成器的引用
    private int currentWave = 0;           // 當前波數
    private bool isTimerActive = false;    // 計時器是否激活
    
    void Awake()
    {
        // 獲取場景中的 MonsterSpawner 組件
        monsterSpawner = FindObjectOfType<MonsterSpawner>();
        
        // 確保找到了 MonsterSpawner
        if (monsterSpawner == null)
        {
            Debug.LogError("找不到 MonsterSpawner 組件！");
        }
    }
    
    void Start()
    {
        // 從 GameDB 中讀取當前波次
        GameDB.Load();
        currentWave = GameDB.currentWave;
        
        // 如果是新遊戲，預設為第1波
        if (currentWave <= 0)
        {
            currentWave = 1;
        }
        
        // 初始化 UI
        UpdateWaveText();
        
        if (timerFillImage != null)
        {
            timerFillImage.fillAmount = 1f;
        }
        
        // 監聽 MonsterSpawner 完成一波的事件
        if (monsterSpawner != null)
        {
            monsterSpawner.OnWaveCompleted += OnWaveCompleted;
            monsterSpawner.OnWaveStarted += OnWaveStarted;
        }
        
        // 初始延遲開始計時
        StartCoroutine(InitialDelayTimer());
    }
    
    // 初始延遲計時器
    private IEnumerator InitialDelayTimer()
    {
        float initialDelay = monsterSpawner.waveStartDelay;
        float timeRemaining = initialDelay;
        
        isTimerActive = true;
        
        while (timeRemaining > 0 && isTimerActive)
        {
            // 更新填充圖片
            if (timerFillImage != null)
            {
                timerFillImage.fillAmount = timeRemaining / initialDelay;
            }
            
            timeRemaining -= Time.deltaTime;
            yield return null;
        }
        
        // 確保填充為 0
        if (timerFillImage != null && isTimerActive)
        {
            timerFillImage.fillAmount = 0f;
        }
        
        isTimerActive = false;
    }
    
    // 當一波怪物被清空時調用
    private void OnWaveCompleted()
    {
        // 同步波次狀態
        currentWave = monsterSpawner.GetCurrentWave();
        
        // 保存當前狀態
        GameDB.currentWave = currentWave;
        GameDB.Save();
        
        // 如果不是最後一波，開始計時下一波
        if (currentWave < monsterSpawner.totalWaves)
        {
            StartWaveTimer();
        }
    }
    
    // 當新的一波開始時調用
    private void OnWaveStarted()
    {
        // 更新當前波次 (與 MonsterSpawner 保持同步)
        currentWave = monsterSpawner.GetCurrentWave();
        UpdateWaveText();
        
        // 保存當前狀態
        GameDB.currentWave = currentWave;
        GameDB.Save();
        
        // 停止計時器
        isTimerActive = false;
    }
    
    // 開始波次間的計時器
    private void StartWaveTimer()
    {
        // 為下一波更新波數
        currentWave++;
        UpdateWaveText();
    
        // 關鍵修改：更新 GameDB 中的下一波數值
        // 這確保了即使切換場景，回來後也會顯示正確的波數
        GameDB.currentWave = currentWave;
        GameDB.Save(); // 立即保存更新的波數
    
        // 重設填充圖片並開始倒數
        if (timerFillImage != null)
        {
            timerFillImage.fillAmount = 1f;
        }
    
        StartCoroutine(CountdownTimer());
    }
    
    // 倒數計時器協程
    private IEnumerator CountdownTimer()
    {
        float timeRemaining = timeBetweenWaves;
        
        isTimerActive = true;
        
        while (timeRemaining > 0 && isTimerActive)
        {
            // 更新填充圖片
            if (timerFillImage != null)
            {
                timerFillImage.fillAmount = timeRemaining / timeBetweenWaves;
            }
            
            timeRemaining -= Time.deltaTime;
            yield return null;
        }
        
        // 確保填充為 0
        if (timerFillImage != null && isTimerActive)
        {
            timerFillImage.fillAmount = 0f;
        }
        
        isTimerActive = false;
    }
    
    // 更新波數文字
    private void UpdateWaveText()
    {
        if (waveNumberText != null)
        {
            waveNumberText.text = currentWave.ToString();
        }
    }
    
    void OnDestroy()
    {
        // 移除事件監聽
        if (monsterSpawner != null)
        {
            monsterSpawner.OnWaveCompleted -= OnWaveCompleted;
            monsterSpawner.OnWaveStarted -= OnWaveStarted;
        }
    }
}