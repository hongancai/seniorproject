using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Transform _traget;
    public NavMeshAgent _agent;
    public GameObject coinPrefab;
    public Slider enemy1health;
    private Animator animator;
    public int minCoins = 1;
    public int maxCoins = 3;
    //public int hitPoints = 3;
    
    void Start()
    {  
      _agent = GetComponent<NavMeshAgent>();
      GameDB.enemyHp = 10;
      animator = GetComponent<Animator>();
      UpdateEnemy1HealthSlider();
    }

    
    void Update()
    {
        _agent.SetDestination(_traget.transform.position);
        animator.Play("move");
        gameObject.transform.eulerAngles = new Vector3(30, 0, 0);
    }

    public void UpdateEnemy1HealthSlider()
    {
        enemy1health.value = GameDB.enemyHp;
    }
    public void TakeDamage()
    {
        GameDB.enemyHp = Mathf.Max(GameDB.enemyHp - 10, 0); // 確保血量不低於0
        UpdateEnemy1HealthSlider();

        if (GameDB.enemyHp <= 0)
        {
            DropCoins();
            Destroy(gameObject);
        }
    }
    private void DropCoins()
    {
        int coinCount = Random.Range(minCoins, maxCoins + 1); // 隨機掉落 1 到 3 個金幣
        for (int i = 0; i <= coinCount; i++)
        {
            // 在怪物位置生成金幣
            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            Vector3 spawnPosition = transform.position + offset;
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
