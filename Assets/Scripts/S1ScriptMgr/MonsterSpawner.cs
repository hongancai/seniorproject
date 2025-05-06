using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject stormPrefab;          
    public GameObject leftBirdPrefab;      
    public GameObject rightBirdPrefab;
    public GameObject leftSoldierPrefab;
    public GameObject rightSoldierPrefab;
    public GameObject leftFishmanPrefab;
    public GameObject rightFishmanPrefab;
    public GameObject leftGhostPrefab;
    public GameObject rightGhostPrefab;
    
    public WaveWarningSystem warningSystem;
    
    public GameObject shopObject;
    public GameObject parkEntrance;
    //public GameObject nextLevelEntrance;
    public GameObject winPnl;
    
    public int totalWaves = 6;           // 改為六波
    public int monstersPerWave = 5;       // 這個將不再直接使用，改為根據波次動態設定
    public float spawnInterval = 7.0f;
    public float waveStartDelay = 15f;
    
    // 生成位置
    private readonly Vector3 leftSpawnPos = new Vector3(478.3f, 0.4522033f, 504.4f);
    private readonly Vector3 rightSpawnPos = new Vector3(522.83f, 0.4522033f, 493.23f);
    
    private int currentWaveMonsters = 0;    // 當前波次存活的怪物數量
    private int currentWave = 0;            // 當前波數
    
    // 新增事件
    public event Action OnWaveCompleted;    // 當一波完成時觸發
    public event Action OnWaveStarted;      // 當一波開始時觸發

    void Start()
    {
        // 從 GameDB 中讀取當前波次狀態
        LoadWaveStatus();
        InitializeLevel();
        StartCoroutine(DelayedStart());
    }

    private void LoadWaveStatus()
    {
        // 從 GameDB 加載波次狀態
        GameDB.Load();
    
        // 直接使用 GameDB 的當前波次
        currentWave = GameDB.currentWave;
    
        // 確保當前波次與 GameDB 同步
        if (currentWave <= 0)
        {
            currentWave = 1;
            GameDB.currentWave = 1;
        }
    }

    private void InitializeLevel()
    {
        winPnl.SetActive(false);
        shopObject.SetActive(true);
        parkEntrance.SetActive(true);
        
        // 如果從頭開始，則波次為 1
        if (currentWave == 0)
        {
            currentWave = 1;
            GameDB.currentWave = 1;
        }
        
        // 保存當前狀態
        GameDB.Save();
    }

    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(waveStartDelay);
        StartCoroutine(SpawnWaves());
    }

    private void DisableSceneObjects()
    {
        shopObject.SetActive(false);
        parkEntrance.SetActive(false);
    }

    private void EnableSceneObjects()
    {
        shopObject.SetActive(true);
        parkEntrance.SetActive(true);
    }

    private IEnumerator SpawnWaves()
    {
        // 從當前波次開始生成，使用 GameDB 中保存的值
        currentWave = GameDB.currentWave;

        for (int wave = currentWave; wave <= totalWaves; wave++)
        {
            currentWave = wave;

            // 更新 GameDB 中的當前波次
            GameDB.currentWave = currentWave;
            GameDB.Save(); // 保存當前狀態

            // 顯示警告
            warningSystem.ShowWarning();
            yield return new WaitForSeconds(3f); // 等待警告顯示完成

            // 關閉場景物件
            DisableSceneObjects();

            // 根據波次生成不同怪物
            switch (wave)
            {
                case 1: // 第一波：五隻暴風
                    currentWaveMonsters = 5;
                    yield return SpawnMonsters(new Dictionary<string, int>
                    {
                        { "leftStorm", 5 }
                    });
                    break;

                case 2: // 第二波：左右側各兩隻暴風、兩隻鳥
                    currentWaveMonsters = 8; // 2+2+2+2 = 8隻怪物
                    yield return SpawnMonsters(new Dictionary<string, int>
                    {
                        { "leftStorm", 2 },
                        { "rightStorm", 2 },
                        { "leftBird", 2 },
                        { "rightBird", 2 }
                    });
                    break;

                case 3: // 第三波：左右側各一隻暴風、兩隻鳥、兩個士兵
                    currentWaveMonsters = 10; // (1+2+2)左 + (1+2+2)右 = 10隻怪物
                    yield return SpawnMonsters(new Dictionary<string, int>
                    {
                        { "leftStorm", 1 },
                        { "rightStorm", 1 },
                        { "leftBird", 2 },
                        { "rightBird", 2 },
                        { "leftSoldier", 2 },
                        { "rightSoldier", 2 }
                    });
                    break;

                case 4: // 第四波：左右側各兩隻鳥、兩個士兵、三個漁人
                    currentWaveMonsters = 14; // (2+2+3)左 + (2+2+3)右 = 14隻怪物
                    yield return SpawnMonsters(new Dictionary<string, int>
                    {
                        { "leftBird", 2 },
                        { "rightBird", 2 },
                        { "leftSoldier", 2 },
                        { "rightSoldier", 2 },
                        { "leftFishman", 3 },
                        { "rightFishman", 3 }
                    });
                    break;

                case 5: // 第五波：左右側各兩個士兵、三個漁人、三隻鬼
                    currentWaveMonsters = 16; // (2+3+3)左 + (2+3+3)右 = 16隻怪物
                    yield return SpawnMonsters(new Dictionary<string, int>
                    {
                        { "leftSoldier", 2 },
                        { "rightSoldier", 2 },
                        { "leftFishman", 3 },
                        { "rightFishman", 3 },
                        { "leftGhost", 3 },
                        { "rightGhost", 3 }
                    });
                    break;

                case 6: // 第六波：共有兩個暴風、兩隻鳥、三個士兵、四個漁人、四個鬼
                    currentWaveMonsters = 17; // 2+2+3+4+6 = 17隻怪物
                    yield return SpawnMonsters(new Dictionary<string, int>
                    {
                        { "leftStorm", 1 },
                        { "rightStorm", 1 },
                        { "leftBird", 1 },
                        { "rightBird", 1 },
                        { "leftSoldier", 2 },
                        { "rightSoldier", 1 },
                        { "leftFishman", 2 },
                        { "rightFishman", 2 },
                        { "leftGhost", 3 },
                        { "rightGhost", 3 }
                    });
                    break;
            }

            // 等待當前波次的怪物被清空
            yield return new WaitUntil(() => currentWaveMonsters <= 0);

            // 更新 GameDB 的波次完成狀態
            UpdateGameDBWaveStatus(wave);

            // 保存狀態
            GameDB.Save();

            // 開啟場景物件
            EnableSceneObjects();

            // 觸發波次完成事件
            OnWaveCompleted?.Invoke();

            // WIN
            if (wave == totalWaves)
            {
                winPnl.SetActive(true);
            }
            else
            {
                yield return new WaitForSeconds(25f); // 波次間隔
            }

            if (winPnl.activeSelf)
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
            }
        }
    }

    // 更新 GameDB 的波次狀態
    private void UpdateGameDBWaveStatus(int wave)
    {
        switch (wave)
        {
            case 1:
                GameDB.wave1 = true;
                break;
            case 2:
                GameDB.wave2 = true;
                break;
            case 3:
                GameDB.wave3 = true;
                break;
            case 4:
                GameDB.wave4 = true;
                break;
            case 5:
                GameDB.wave5 = true;
                break;
            case 6:
                GameDB.wave6 = true;
                break;
        }
        
        // 更新 GameDB 中的當前波次
        GameDB.currentWave = wave;
    }

    // 新的怪物生成方法，根據字典來生成不同類型和數量的怪物
    private IEnumerator SpawnMonsters(Dictionary<string, int> monsterTypes)
    {
        foreach (var kvp in monsterTypes)
        {
            string monsterType = kvp.Key;
            int count = kvp.Value;
            
            for (int i = 0; i < count; i++)
            {
                SpawnMonster(monsterType);
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
    
    // 根據類型生成單個怪物
    private void SpawnMonster(string monsterType)
    {
        GameObject prefab = null;
        Vector3 spawnPos = Vector3.zero;
        
        switch (monsterType)
        {
            case "leftStorm":
                prefab = stormPrefab;
                spawnPos = leftSpawnPos;
                break;
            case "rightStorm":
                prefab = stormPrefab;
                spawnPos = rightSpawnPos;
                break;
            case "leftBird":
                prefab = leftBirdPrefab;
                spawnPos = rightSpawnPos;
                break;
            case "rightBird":
                prefab = rightBirdPrefab;
                spawnPos = leftSpawnPos;
                break;
            case "leftSoldier":
                prefab = leftSoldierPrefab;
                spawnPos = rightSpawnPos;
                break;
            case "rightSoldier":
                prefab = rightSoldierPrefab;
                spawnPos = leftSpawnPos;
                break;
            case "leftFishman":
                prefab = leftFishmanPrefab;
                spawnPos = rightSpawnPos;
                break;
            case "rightFishman":
                prefab = rightFishmanPrefab;
                spawnPos = leftSpawnPos;
                break;
            case "leftGhost":
                prefab = leftGhostPrefab;
                spawnPos = rightSpawnPos;
                break;
            case "rightGhost":
                prefab = rightGhostPrefab;
                spawnPos = leftSpawnPos;
                break;
        }
        
        if (prefab != null)
        {
            GameObject monster = Instantiate(prefab, spawnPos, Quaternion.identity);
            
            // 检查所有可能的健康处理器组件
            StormHpHandler stormHealth = monster.GetComponentInChildren<StormHpHandler>();
            if (stormHealth != null)
            {
                StartCoroutine(MonitorMonsterHealth(monster));
                return;
            }
            LBirdHpHandler lbirdHealth = monster.GetComponentInChildren<LBirdHpHandler>();
            if (lbirdHealth != null)
            {
                StartCoroutine(MonitorMonsterHealth(monster));
                return;
            }
            LSoldierHpHandler lsoldierHealth = monster.GetComponentInChildren<LSoldierHpHandler>();
            if (lsoldierHealth != null)
            {
                StartCoroutine(MonitorMonsterHealth(monster));
                return;
            }
            LFishmanHpHandler lfishmanHealth = monster.GetComponentInChildren<LFishmanHpHandler>();
            if (lfishmanHealth != null)
            {
                StartCoroutine(MonitorMonsterHealth(monster));
                return;
            }
            LGhostHpHandler lghostHealth = monster.GetComponentInChildren<LGhostHpHandler>();
            if (lghostHealth != null)
            {
                StartCoroutine(MonitorMonsterHealth(monster));
                return;
            }
            
            RBirdHpHandler rbirdHealth = monster.GetComponentInChildren<RBirdHpHandler>();
            if (rbirdHealth != null)
            {
                StartCoroutine(MonitorMonsterHealth(monster));
                return;
            }
            RSoldierHpHandler rsoldierHealth = monster.GetComponentInChildren<RSoldierHpHandler>();
            if (rsoldierHealth != null)
            {
                StartCoroutine(MonitorMonsterHealth(monster));
                return;
            }
            RFishmanHpHandler rfishmanHealth = monster.GetComponentInChildren<RFishmanHpHandler>();
            if (rfishmanHealth != null)
            {
                StartCoroutine(MonitorMonsterHealth(monster));
                return;
            }
            RGhostHpHandler rghostHealth = monster.GetComponentInChildren<RGhostHpHandler>();
            if (rghostHealth != null)
            {
                StartCoroutine(MonitorMonsterHealth(monster));
                return;
            }
        }
    }
    
    // 监控怪物健康状态的协程
    private IEnumerator MonitorMonsterHealth(GameObject monster)
    {
        bool isDead = false;
        
        while (monster != null && !isDead)
        {
            // 检查各种健康处理器
            StormHpHandler stormHealth = monster.GetComponentInChildren<StormHpHandler>();
            if (stormHealth != null && (stormHealth.IsDying() || stormHealth.GetHpPercentage() <= 0))
            {
                isDead = true;
            }
            
            RBirdHpHandler rbirdHealth = monster.GetComponentInChildren<RBirdHpHandler>();
            if (rbirdHealth != null && (rbirdHealth.IsDying() || rbirdHealth.GetHpPercentage() <= 0))
            {
                isDead = true;
            }
            
            // 為其他怪物類型添加類似的檢查
            LBirdHpHandler lbirdHealth = monster.GetComponentInChildren<LBirdHpHandler>();
            if (lbirdHealth != null && (lbirdHealth.IsDying() || lbirdHealth.GetHpPercentage() <= 0))
            {
                isDead = true;
            }
            
            LSoldierHpHandler lsoldierHealth = monster.GetComponentInChildren<LSoldierHpHandler>();
            if (lsoldierHealth != null && (lsoldierHealth.IsDying() || lsoldierHealth.GetHpPercentage() <= 0))
            {
                isDead = true;
            }
            
            RSoldierHpHandler rsoldierHealth = monster.GetComponentInChildren<RSoldierHpHandler>();
            if (rsoldierHealth != null && (rsoldierHealth.IsDying() || rsoldierHealth.GetHpPercentage() <= 0))
            {
                isDead = true;
            }
            
            LFishmanHpHandler lfishmanHealth = monster.GetComponentInChildren<LFishmanHpHandler>();
            if (lfishmanHealth != null && (lfishmanHealth.IsDying() || lfishmanHealth.GetHpPercentage() <= 0))
            {
                isDead = true;
            }
            
            RFishmanHpHandler rfishmanHealth = monster.GetComponentInChildren<RFishmanHpHandler>();
            if (rfishmanHealth != null && (rfishmanHealth.IsDying() || rfishmanHealth.GetHpPercentage() <= 0))
            {
                isDead = true;
            }
            
            LGhostHpHandler lghostHealth = monster.GetComponentInChildren<LGhostHpHandler>();
            if (lghostHealth != null && (lghostHealth.IsDying() || lghostHealth.GetHpPercentage() <= 0))
            {
                isDead = true;
            }
            
            RGhostHpHandler rghostHealth = monster.GetComponentInChildren<RGhostHpHandler>();
            if (rghostHealth != null && (rghostHealth.IsDying() || rghostHealth.GetHpPercentage() <= 0))
            {
                isDead = true;
            }
            
            if (isDead)
            {
                OnMonsterKilled();
                yield break;
            }
            
            yield return new WaitForSeconds(0.5f); // 定期检查
        }
        
        // 如果怪物對象被銷毀但我們沒有檢測到死亡，也算作擊殺
        if (monster == null)
        {
            OnMonsterKilled();
        }
    }

    private void OnMonsterKilled()
    {
        currentWaveMonsters--;
        
        // 檢查是否是該波次最後一個怪物被殺死
        if (currentWaveMonsters <= 0)
        {
            // 確保這裡也保存波次狀態
            GameDB.Save();
        }
    }
    
    // 獲取當前波數的公共方法
    public int GetCurrentWave()
    {
        return currentWave;
    }
}