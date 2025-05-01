using System.Collections;
using System;
using UnityEngine;

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
    public GameObject nextLevelEntrance;
    
    public int totalWaves = 4;           
    public int monstersPerWave = 5;         
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
        InitializeLevel();
        StartCoroutine(DelayedStart());
    }

    private void InitializeLevel()
    {
        nextLevelEntrance.SetActive(false);
        shopObject.SetActive(true);
        parkEntrance.SetActive(true);
        currentWave = 1;  // 初始化為第一波
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
        for (int wave = 1; wave <= totalWaves; wave++)
        {
            currentWave = wave;
            
            // 觸發波次開始事件
            OnWaveStarted?.Invoke();
            
            // 顯示警告
            warningSystem.ShowWarning();
            yield return new WaitForSeconds(3f);  // 等待警告顯示完成
            
            // 關閉場景物件
            DisableSceneObjects();
            
            currentWaveMonsters = monstersPerWave;

            // 根據波次生成不同怪物
            switch (wave)
            {
                case 1:  // 第一波：只在左側生成Storm
                    yield return SpawnWaveMonsters(stormPrefab, null);
                    break;
                    
                case 2:  // 第二波：左右側都生成Storm
                    yield return SpawnWaveMonsters(stormPrefab, stormPrefab);
                    break;
                    
                case 3:  // 第三波：左右側生成不同的Bird
                    yield return SpawnWaveMonsters(rightBirdPrefab, leftBirdPrefab);
                    break;
                      
                case 4:  // 第四波：左右側生成不同的Soldier
                    yield return SpawnWaveMonsters(rightSoldierPrefab, leftSoldierPrefab);
                    break;
            }

            // 等待當前波次的怪物被清空
            yield return new WaitUntil(() => currentWaveMonsters <= 0);
            
            // 開啟場景物件
            EnableSceneObjects();
            
            // 觸發波次完成事件
            OnWaveCompleted?.Invoke();
            
            // 最後一波結束後開啟下一關入口
            if (wave == totalWaves)
            {
                nextLevelEntrance.SetActive(true);
            }
            else
            {
                yield return new WaitForSeconds(25f);  // 波次間隔
            }
        }
    }

    private IEnumerator SpawnWaveMonsters(GameObject leftPrefab, GameObject rightPrefab)
    {
        for (int i = 0; i < monstersPerWave; i++)
        {
            // 生成左側怪物
            if (leftPrefab != null)
            {
                GameObject monster = Instantiate(leftPrefab, leftSpawnPos, Quaternion.identity);
               // MonsterHealth health = monster.GetComponent<MonsterHealth>();
                //if (health != null)
                {
                   // health.OnMonsterDeath += OnMonsterKilled;
                }
            }
            
            // 生成右側怪物
            if (rightPrefab != null)
            {
                GameObject monster = Instantiate(rightPrefab, rightSpawnPos, Quaternion.identity);
                //MonsterHealth health = monster.GetComponent<MonsterHealth>();
                //if (health != null)
                {
                    //health.OnMonsterDeath += OnMonsterKilled;
                }
            }
            
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void OnMonsterKilled()
    {
        currentWaveMonsters--;
    }
    
    // 獲取當前波數的公共方法
    public int GetCurrentWave()
    {
        return currentWave;
    }
}