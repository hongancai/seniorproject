using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform _traget;
    public NavMeshAgent _agent;
    //public int hitPoints = 3;
    
    void Start()
    {  
      _agent = GetComponent<NavMeshAgent>();
      GameDB.enemyHp = 3;
    }

    
    void Update()
    {
        _agent.SetDestination(_traget.transform.position);
        gameObject.transform.eulerAngles = new Vector3(30, 0, 0);
    }

    public void TakeDamage()
    {
        GameDB.enemyHp--;

        if (GameDB.enemyHp <= 0)
        {
            Destroy(gameObject);
            //
            
        }
    }
}
