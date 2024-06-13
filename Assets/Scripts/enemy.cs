using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //取得物件的Agent元件
    }

    void Update()
    {
        agent.SetDestination(target.position); // 設定目標位置
    }
}