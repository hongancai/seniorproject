using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BirdEnemy : MonoBehaviour
{
    public Transform traget;
    public NavMeshAgent agent;
    public GameObject coinPrefab;
    private Animator animator;
    public int minCoins = 1;
    public int maxCoins = 3;
    
    
    void Start()
    {  
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
    }

    
    void Update()
    {
        agent.SetDestination(traget.transform.position);
        
        gameObject.transform.eulerAngles = new Vector3(30, 0, 0);
    }
    
    
    private void DropCoins()
    {
        int coinCount = Random.Range(minCoins, maxCoins + 1); // 隨機掉落 1 到 3 個金幣
        for (int i = 0; i < coinCount; i++)
        {
            // 在怪物位置生成金幣
            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            Vector3 spawnPosition = transform.position + offset;
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
