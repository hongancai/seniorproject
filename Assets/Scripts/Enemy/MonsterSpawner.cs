using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject stormPrefab;          
    public GameObject graveBirdPrefab;      
    public GameObject headlessTroopPrefab;
    
    

    private int totalWaves = 3;           
    private int monstersPerWave = 5;         // 每波怪物數量
    private float minInterval = 15f;         // 最小生成間隔
    private float maxInterval = 25f;         // 最大生成間隔

    // 固定生成位置
    public Vector3[] spawnPositions;

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(10f);  // 等待 10 秒
        StartCoroutine(SpawnWaves());           // 開始波次生成
    }

    private IEnumerator SpawnWaves()
    {
        for (int wave = 1; wave <= totalWaves; wave++)
        {
            GameObject monsterPrefab = null;
            
            switch (wave)
            {
                case 1:
                    monsterPrefab = stormPrefab;
                    break;
                case 2:
                    monsterPrefab = graveBirdPrefab;
                    break;
                case 3:
                    monsterPrefab = headlessTroopPrefab;
                    break;
            }
            
            // 生成該波的怪物，使用固定位置
            for (int i = 0; i < monstersPerWave; i++)
            {
                if (monsterPrefab != null && spawnPositions.Length > 0)
                {
                    
                    Vector3 spawnPosition = spawnPositions[i % spawnPositions.Length];
                    Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
                }
                yield return new WaitForSeconds(3.5f); 
            }
            
            float waveInterval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waveInterval);
        }
    }
}
