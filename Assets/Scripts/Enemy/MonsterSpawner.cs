using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject stormPrefab;          // 暴風的預製件
    public GameObject graveBirdPrefab;       // 墓坑鳥的預製件
    public GameObject headlessTroopPrefab;   // 無頭部隊的預製件
    
    public GameObject warningPanel;          // 警告面板

    private int totalWaves = 3;              // 總波數
    private int monstersPerWave = 5;         // 每波怪物數量
    private float minInterval = 15f;         // 最小生成間隔
    private float maxInterval = 25f;         // 最大生成間隔

    // 固定生成位置
    public Vector3[] spawnPositions;

    void Start()
    {
        // 隱藏警告面板
        warningPanel.SetActive(false);
        // 等待 10 秒後開始波次生成
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

            // 根據波數選擇怪物類型
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

            // 顯示警告面板
            warningPanel.SetActive(true);
            yield return new WaitForSeconds(3f);  // 顯示3秒
            warningPanel.SetActive(false);

            // 生成該波的怪物，使用固定位置
            for (int i = 0; i < monstersPerWave; i++)
            {
                if (monsterPrefab != null && spawnPositions.Length > 0)
                {
                    // 從固定位置陣列中選擇位置來生成怪物
                    Vector3 spawnPosition = spawnPositions[i % spawnPositions.Length];
                    Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
                }
                yield return new WaitForSeconds(3.5f); // 每隻怪物間隔3.5秒生成
            }

            // 等待下一波的隨機間隔時間
            float waveInterval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waveInterval);
        }
    }
}
