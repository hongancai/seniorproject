using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Transform traget;
    public NavMeshAgent agent;
    public GameObject coinPrefab;
    
    private Animator animator;
    public int minCoins = 1;
    public int maxCoins = 3;
   
    public enum Storm
    {
        Find,
        Attack
    }
    void Start()
    {  
        agent = GetComponent<NavMeshAgent>();
        //GameDB.enemyHp = 50;
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        agent.SetDestination(traget.transform.position);
        animator.Play("move");
        gameObject.transform.eulerAngles = new Vector3(30, 0, 0);
        var vector3 = gameObject.transform.position;
        vector3.y = 0.061f; //強制更新高度
        gameObject.transform.position = vector3;
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

